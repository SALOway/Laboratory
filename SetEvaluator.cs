namespace Laboratory
{
    class SetEvaluator<T>
    {
        bool _debug = false;
        private List<string> _tokens = new List<string>();
        private Stack<string> _operations = new Stack<string>();
        private Stack<Set> _values = new Stack<Set>();
        private readonly Dictionary<string, Set> _sets;
        private static readonly Dictionary<string, int> setOperators = new Dictionary<string, int>
        {
            { "(", 1},
            { ")", 1},
            { "union", 0},
            { "complement", 0},
            { "difference", 0},
            { "intersection", 0},
        };
        public SetEvaluator(Dictionary<string, Set> sets, bool debug = false)
        {
            _sets = sets;
            _debug = debug;
        }
        /// <summary>
        /// Convert string expression to list of tokens
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>
        /// List of tokens from expression
        /// </returns>
        List<string> ConvertToTokens(string expression)
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
                        _tokens.Add(temp);
                        temp = "";
                    }
                    _tokens.Add(symbol.ToString());
                }
                else if (symbol == ' ')
                {
                    // push temp
                    if (!string.IsNullOrEmpty(temp))
                    {
                        _tokens.Add(temp);
                        temp = "";
                    }
                }
                else
                {
                    temp += symbol;
                }
            }
            if (!string.IsNullOrEmpty(temp)) _tokens.Add(temp);
            return _tokens;
        }
        private Set Execute(string operation, Set leftOperand, Set rightOperand)
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
        public Set Evaluate(string input)
        {
            if (_sets.Count < 2) return new Set();
            _tokens = ConvertToTokens(input);
            _operations.Clear();
            _values.Clear();
            foreach (string token in _tokens)
            {
                if (_debug)
                {
                    Console.WriteLine("Current token: " + token);
                    Console.WriteLine("Current operations: " + "{" + string.Join(", ", _operations) + "}");
                    Console.WriteLine("Current values: " + "{" + string.Join(", ", _values) + "}");
                    Console.WriteLine();
                }
                if (setOperators.ContainsKey(token))
                {
                    if (token == ")")
                    {
                        var operation = _operations.Pop();
                        while (operation != "(" && _operations.Count > 0 && _values.Count > 1)
                        {
                            var b = _values.Pop();
                            var a = _values.Pop();
                            _values.Push(Execute(operation, a, b));
                            operation = _operations.Pop();
                        }
                        continue;
                    }

                    bool procedenceIsLowerOrEqual = false;
                    string lastOperator;
                    if (_operations.TryPeek(out lastOperator))
                    {
                        procedenceIsLowerOrEqual = setOperators[token] <= setOperators[lastOperator];
                    }

                    if (procedenceIsLowerOrEqual)
                    {
                        if (_operations.Count > 0)
                        {
                            var operation = _operations.Pop();
                            while (operation != "(" && _values.Count > 1)
                            {
                                var b = _values.Pop();
                                var a = _values.Pop();
                                _values.Push(Execute(operation, a, b));
                            }
                            if (operation == "(")
                            {
                                _operations.Push(operation);
                            }
                        }
                    }
                    _operations.Push(token);
                }
                else
                {
                    if (_sets.TryGetValue(token, out Set set))
                    {
                        _values.Push(set);
                    }
                    else
                    {
                        return new Set();
                    }
                }
            }

            while (_operations.Count > 0)
            {
                var operation = _operations.Pop();
                var b = _values.Pop();
                var a = _values.Pop();
                _values.Push(Execute(operation, a, b));
            }

            return _values.Pop();
        }
    }
}
