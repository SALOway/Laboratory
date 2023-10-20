namespace Laboratory
{
    class SetEvaluator
    {
        private static readonly Dictionary<string, int> setOperators = new Dictionary<string, int>
        {
            { "(", 1},
            { ")", 1},
            { "union", 0},
            { "complement", 0},
            { "difference", 0},
            { "intersection", 0},
        };
        /// <summary>
        /// Convert string expression to list of tokens
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>
        /// List of tokens from expression
        /// </returns>
        private static List<string> ConvertToTokens(string expression)
        {
            var tokens = new List<string>();
            string temp = "";
            foreach (var symbol in expression)
            {
                if (symbol == '(' || symbol == ')')
                {
                    // extract '(' and ')' symbols  and push temp if it is not empty
                    if (!string.IsNullOrEmpty(temp))
                    {
                        tokens.Add(temp);
                        temp = "";
                    }
                    tokens.Add(symbol.ToString());
                }
                else if (symbol == ' ')
                {
                    // push temp
                    if (!string.IsNullOrEmpty(temp))
                    {
                        tokens.Add(temp);
                        temp = "";
                    }
                }
                else
                {
                    temp += symbol;
                }
            }
            if (!string.IsNullOrEmpty(temp)) tokens.Add(temp);
            return tokens;
        }
        private static Set Execute(string operation, Set leftOperand, Set rightOperand)
        {
            var set = new Set();
            switch (operation)
            {
                case "union":
                    set = Set.Union(leftOperand, rightOperand);
                    break;
                case "intersection":
                    set = Set.Intersection(leftOperand, rightOperand);
                    break;
                case "difference":
                    set = Set.Difference(leftOperand, rightOperand);
                    break;
                case "complement":
                    set = Set.Complement(leftOperand, rightOperand);
                    break;
            }
            return set;
        }
        public static Set Evaluate(string input, Dictionary<string, Set> variables)
        {
            if (variables.Count < 2) return Set.Empty;
            var tokens = ConvertToTokens(input);
            var operations = new Stack<string>();
            var values = new Stack<Set>();
            foreach (string token in tokens)
            {
                if (setOperators.ContainsKey(token))
                {
                    if (token == ")")
                    {
                        var operation = operations.Pop();
                        while (operation != "(" && operations.Count > 0 && values.Count > 1)
                        {
                            var b = values.Pop();
                            var a = values.Pop();
                            values.Push(Execute(operation, a, b));
                            operation = operations.Pop();
                        }
                        continue;
                    }

                    bool procedenceIsLowerOrEqual = false;
                    string lastOperator;
                    if (operations.TryPeek(out lastOperator))
                    {
                        procedenceIsLowerOrEqual = setOperators[token] <= setOperators[lastOperator];
                    }

                    if (procedenceIsLowerOrEqual)
                    {
                        if (operations.Count > 0)
                        {
                            var operation = operations.Pop();
                            while (operation != "(" && values.Count > 1)
                            {
                                var b = values.Pop();
                                var a = values.Pop();
                                values.Push(Execute(operation, a, b));
                            }
                            if (operation == "(")
                            {
                                operations.Push(operation);
                            }
                        }
                    }
                    operations.Push(token);
                }
                else
                {
                    if (variables.TryGetValue(token, out Set set))
                    {
                        values.Push(set);
                    }
                    else
                    {
                        return new Set();
                    }
                }
            }

            while (operations.Count > 0)
            {
                var operation = operations.Pop();
                var b = values.Pop();
                var a = values.Pop();
                values.Push(Execute(operation, a, b));
            }

            return values.Pop();
        }
    }
}
