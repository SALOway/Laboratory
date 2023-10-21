﻿namespace Laboratory
{
    class SetEvaluator
    {
        private static readonly Dictionary<string, int> predefinedSetOperators = new Dictionary<string, int>
        {
            { "(", 0 },
            { ")", -1 },
            { "union", 1 },
            { "intersection", 1 },
            { "complement", 1},
            { "difference", 1},
        };
        /// <summary>
        /// Convert string expression to list of tokens based on spaces and predefined operators
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>
        /// List of tokens from expression
        /// </returns>
        private static List<string> ConvertToTokens(string expression)
        {
            var tokens = new List<string>();
            var temp = "";
            for (int i = 0; i < expression.Length; i++)
            {
                var symbol = expression[i];
                if (symbol == ' ')
                {
                    if (!string.IsNullOrEmpty(temp))
                    {
                        tokens.Add(temp);
                        temp = "";
                    }
                    continue;
                }
                temp += symbol;

                // try find operator
                foreach (var op in predefinedSetOperators.Keys)
                {
                    bool outOfRange = (i + op.Length) > expression.Length;
                    if (outOfRange) continue;

                    var potentialOperator = expression.Substring(i, op.Length).ToLower();
                    var isActuallyOperator = potentialOperator.Equals(op);
                    if (isActuallyOperator)
                    {
                        var variable = temp[..^1];
                        if (!string.IsNullOrEmpty(variable)) tokens.Add(variable);
                        tokens.Add(potentialOperator);
                        temp = "";
                        i += op.Length - 1;
                        break;
                    }
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
            var operators = new Stack<string>();
            var values = new Stack<Set>();
            foreach (string token in tokens)
            {
                if (predefinedSetOperators.ContainsKey(token))
                {
                    if (token == "(")
                    {
                        operators.Push(token);
                        continue;
                    }
                    if (operators.Count > 0)
                    {
                        bool lastOperatorPrecedenceIsLower = predefinedSetOperators[operators.Peek()] < predefinedSetOperators[token];
                        if (!lastOperatorPrecedenceIsLower && values.Count > 1)
                        {
                            var operation = operators.Pop();
                            while (operation != "(" && values.Count > 1) // remove [operators.Count > 0]???
                            {
                                var b = values.Pop();
                                var a = values.Pop();
                                values.Push(Execute(operation, a, b));
                                if (operators.Count == 0) break;
                                operation = operators.Pop();
                            }
                            if (operation == "(" && token == ")")
                            {
                                continue;
                            }
                        }
                    }
                    operators.Push(token);
                }
                else
                {
                    try
                    {
                        values.Push(variables[token]);
                    }
                    catch (Exception)
                    {
                        throw new Exception($"Token [{token}] is not exist in given dictonary: [{string.Join(", ", variables.Keys)}]");
                    }
                }
            }

            while (operators.Count > 0)
            {
                var operation = operators.Pop();
                var b = values.Pop();
                var a = values.Pop();
                values.Push(Execute(operation, a, b));
            }

            return values.Pop();
        }
    }
}
