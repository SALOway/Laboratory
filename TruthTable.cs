namespace Laboratory
{
    class TruthTable
    {
        public string Expression { get; private set; }
        public string[] VariableNames { get; private set; }
        private Dictionary<string, bool>[] _data;

        public TruthTable(string expression, string[] variableNames)
        {
            Expression = expression;
            VariableNames = variableNames;
            _data = CreateTruthTable();
        }

        private Dictionary<string, bool>[] GenerateValuesTable()
        {
            var n = VariableNames.Length;
            var totalRows = 1 << n;
            var variablesRows = new Dictionary<string, bool>[totalRows];
            for (var i = 0; i < totalRows; i++)
            {
                variablesRows[i] = new Dictionary<string, bool>();
            }
            for (var i = 1; i <= n; i++)
            {
                var exponent = n - i;
                for (var j = 0; j < totalRows;)
                {
                    for (var k = 0; k < (1 << exponent); j++, k++)
                    {
                        variablesRows[j][VariableNames[i - 1]] = true;
                    }
                    for (var k = 1 << exponent; k < (1 << exponent + 1); j++, k++)
                    {
                        variablesRows[j][VariableNames[i - 1]] = false;
                    }
                }
            }
            return variablesRows;
        }

        private Dictionary<string, bool>[] CreateTruthTable()
        {
            var tableRows = GenerateValuesTable();
            foreach (var tableRow in tableRows)
            {
                var boolean = LogicalEvaluator.Evaluate(Expression, tableRow);
                tableRow.Add(Expression, boolean);
            }
            return tableRows;
        }

        public void Print()
        {
            string tableHead = string.Join(" | ", _data[0].Keys.Select(variableName => $"{variableName,-5}"));
            Console.WriteLine(new string('-', tableHead.Length));
            Console.WriteLine(tableHead);
            Console.WriteLine(new string('-', tableHead.Length));
            foreach (var rowData in _data)
            {
                Console.WriteLine(string.Join(" | ", rowData.Values.Select(value => $"{value,-5}")));
            }
            Console.WriteLine(new string('-', tableHead.Length));
        }
    }
}
