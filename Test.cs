namespace Laboratory
{
    static class Test
    {
        private enum LogMessageType
        {
            Task,
            Input,
            ExpectedOutput,
            Output,
            OutputStatic
        }

        private static Dictionary<LogMessageType, string> LogMessages = new Dictionary<LogMessageType, string>()
        {
            { LogMessageType.Task, string.Empty },
            { LogMessageType.Input, string.Empty },
            { LogMessageType.ExpectedOutput, string.Empty },
            { LogMessageType.Output, string.Empty },
            { LogMessageType.OutputStatic, string.Empty },
        };

        private static void Log()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            
            Console.WriteLine("Test task: " + LogMessages[LogMessageType.Task]);
            Console.WriteLine("Input: " + LogMessages[LogMessageType.Input]);
            Console.WriteLine("Expected output: " + LogMessages[LogMessageType.ExpectedOutput]);

            if (!string.IsNullOrEmpty(LogMessages[LogMessageType.Output]))
            {
                bool outputAreEqual = LogMessages[LogMessageType.Output] == LogMessages[LogMessageType.ExpectedOutput];
                Console.ForegroundColor = outputAreEqual ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"Output: {LogMessages[LogMessageType.Output]}");
                LogMessages[LogMessageType.Output] = string.Empty;
            }


            if (!string.IsNullOrEmpty(LogMessages[LogMessageType.OutputStatic]))
            {
                bool staticOutputAreEqual = LogMessages[LogMessageType.OutputStatic] == LogMessages[LogMessageType.ExpectedOutput];
                Console.ForegroundColor = staticOutputAreEqual ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"Output (static): " + LogMessages[LogMessageType.OutputStatic]);
                LogMessages[LogMessageType.OutputStatic] = string.Empty;
            }
            Console.ResetColor();
            Console.WriteLine();
        }
        private static string DictionaryToListOfSets(Dictionary<string, Set> sets) => "{\n" + string.Join("\n", sets.Select(kv => $"    {kv.Key}: {string.Join(", ", kv.Value)}")) + "\n}";

        static public void CreateSet()
        {
            var array = new int[] { 1, 2, 2, 3 };
            var set = new Set(array);

            LogMessages[LogMessageType.Task] = "Create a set from a list of elements, removing any duplicates";
            LogMessages[LogMessageType.Input] = $"[ {string.Join(", ", array)} ]";
            LogMessages[LogMessageType.ExpectedOutput] = "{ 1, 2, 3 }";
            LogMessages[LogMessageType.Output] = set.ToString();
            Log();
        }
        static public void AddElement()
        {
            var array = new int[] { 1, 2, 3 };
            int element = 4;
            var set = new Set(array);
            set.AddElement(element);

            LogMessages[LogMessageType.Task] = "Add an element to the set if it's not already present";
            LogMessages[LogMessageType.Input] = $"[ {string.Join(", ", array)} ], {element}";
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 1, 2, 3, 4 }).ToString();
            LogMessages[LogMessageType.Output] = set.ToString();
            Log();
        }
        static public void RemoveElement()
        {
            var array = new int[] { 1, 2, 3, 4 };
            int element = 4;
            var set = new Set(array);
            set.RemoveElement(element);

            LogMessages[LogMessageType.Task] = "Remove an element if it exists in the set";
            LogMessages[LogMessageType.Input] = $"[ {string.Join(", ", array)} ], {element}";
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 1, 2, 3 }).ToString();
            LogMessages[LogMessageType.Output] = set.ToString();
            Log();
        }
        static public void ContainsElement()
        {
            var array = new int[] { 1, 2, 3 };
            int element = 4;
            var set = new Set(array);
            bool isSetContainsElement = set.Contains(element);

            LogMessages[LogMessageType.Task] = "Return a boolean indicating if an element is present in the set";
            LogMessages[LogMessageType.Input] = $"[ {string.Join(", ", array)} ], {element}";
            LogMessages[LogMessageType.ExpectedOutput] = false.ToString();
            LogMessages[LogMessageType.Output] = isSetContainsElement.ToString();
            Log();
        }
        static public void Union()
        {
            var arrayA = new int[] { 1, 2, 3 };
            var arrayB = new int[] { 3, 4, 5 };
            var setA = new Set(arrayA);
            var setB = new Set(arrayB);
            var staticUnionSet = Set.Union(setA, setB);
            setA.Union(setB);

            LogMessages[LogMessageType.Task] = "Return a new set that's the union of the two sets";
            LogMessages[LogMessageType.Input] = $"[ {string.Join(", ", arrayA)} ], [ {string.Join(", ", arrayB)} ]";
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 1, 2, 3, 4, 5 }).ToString();
            LogMessages[LogMessageType.Output] = setA.ToString();
            LogMessages[LogMessageType.OutputStatic] = staticUnionSet.ToString();
            Log();
        }
        static public void Intersection()
        {
            var arrayA = new int[] { 1, 2, 3 };
            var arrayB = new int[] { 3, 4, 5 };
            var setA = new Set(arrayA);
            var setB = new Set(arrayB);
            var staticIntersectionSet = Set.Intersection(setA, setB);
            setA.Intersection(setB);

            LogMessages[LogMessageType.Task] = "Return a new set that's the intersection of the two sets";
            LogMessages[LogMessageType.Input] = $"[ {string.Join(", ", arrayA)} ], [ {string.Join(", ", arrayB)} ]";
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 3 }).ToString();
            LogMessages[LogMessageType.Output] = setA.ToString();
            LogMessages[LogMessageType.OutputStatic] = staticIntersectionSet.ToString();
            Log();
        }
        static public void Difference()
        {
            var arrayA = new int[] { 1, 2, 3 };
            var arrayB = new int[] { 3, 4, 5 };
            var setA = new Set(arrayA);
            var setB = new Set(arrayB);
            var staticDifferenceSet = Set.Difference(setA, setB);
            setA.Difference(setB);

            LogMessages[LogMessageType.Task] = "Return a set containing elements in setA but not in setB";
            LogMessages[LogMessageType.Input] = $"[ {string.Join(", ", arrayA)} ], [ {string.Join(", ", arrayB)} ]";
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 1, 2 }).ToString();
            LogMessages[LogMessageType.Output] = setA.ToString();
            LogMessages[LogMessageType.OutputStatic] = staticDifferenceSet.ToString();
            Log();
        }
        static public void Complement()
        {
            var arrayA = new int[] { 1, 2, 3 };
            var arrayB = new int[] { 1, 2, 3, 4, 5 };
            var set = new Set(arrayA);
            var universalSet = new Set(arrayB);
            var staticComplementSet = Set.Complement(set, universalSet);
            set.Complement(universalSet);

            LogMessages[LogMessageType.Task] = "Return the complement of setA in relation to a universal set";
            LogMessages[LogMessageType.Input] = $"[ {string.Join(", ", arrayA)} ], [ {string.Join(", ", arrayB)} ]";
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 4, 5 }).ToString();
            LogMessages[LogMessageType.Output] = set.ToString();
            LogMessages[LogMessageType.OutputStatic] = staticComplementSet.ToString();
            Log();
        }
        static public void EvaluateEasy_1()
        {
            var expression = "A intersection B union C";
            var setsDict = new Dictionary<string, Set>()
            {
                { "A", new Set(new [] { 1, 2, 3 }) },
                { "B", new Set(new [] { 3, 4, 5 }) },
                { "C", new Set(new [] { 5, 6, 7 }) }
            };
            var set = SetEvaluator.Evaluate(expression, setsDict);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression";
            LogMessages[LogMessageType.Input] = $"{expression},\nsetsDict = " + DictionaryToListOfSets(setsDict);
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 3, 5, 6, 7 }).ToString();
            LogMessages[LogMessageType.Output] = set.ToString();
            Log();
        }
        static public void EvaluateEasy_2()
        {
            var expression = "A intersection B union C difference D complement E";
            var setsDict = new Dictionary<string, Set>()
            {
                { "A", new Set(new [] { 1, 2, 3 }) },
                { "B", new Set(new [] { 3, 4, 5 }) },
                { "C", new Set(new [] { 5, 6, 7 }) },
                { "D", new Set(new [] { 10, 4, 12, 5, 6 }) },
                { "E", new Set(new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }) },
            };
            var set = SetEvaluator.Evaluate(expression, setsDict);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression";
            LogMessages[LogMessageType.Input] = $"{expression},\nsetsDict = " + DictionaryToListOfSets(setsDict);
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 1, 2, 4, 5, 6, 8, 9, 10 }).ToString();
            LogMessages[LogMessageType.Output] = set.ToString();
            Log();
        }
        static public void EvaluateMedium_1()
        {
            var expression = "(A union B) intersection (C difference D) complement (E union F)";
            var setsDict = new Dictionary<string, Set>()
            {
                { "A", new Set(new [] { 1, 2, 3, 4 }) },
                { "B", new Set(new [] { 5, 8, 9, 6 }) },
                { "C", new Set(new [] { 4, 6, 7, 10 }) },
                { "D", new Set(new [] { 10, 4, 12, 5, 6 }) },
                { "E", new Set(new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }) },
                { "F", new Set(new [] { 11, 12, 13, 14, 15, 16, 17, 18 }) },
            };
            var set = SetEvaluator.Evaluate(expression, setsDict);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression (medium dificulty)";
            LogMessages[LogMessageType.Input] = $"{expression},\nsetsDict = " + DictionaryToListOfSets(setsDict);
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 }).ToString();
            LogMessages[LogMessageType.Output] = set.ToString();
            Log();
        }
        static public void EvaluateMedium_2()
        {
            var expression = "(((A union B) intersection C) difference D) union (D complement (E union F))";
            var setsDict = new Dictionary<string, Set>()
            {
                { "A", new Set(new [] { 1, 2, 3, 4 }) },
                { "B", new Set(new [] { 5, 8, 9, 6 }) },
                { "C", new Set(new [] { 4, 6, 7, 10 }) },
                { "D", new Set(new [] { 10, 4, 12, 5, 6 }) },
                { "E", new Set(new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }) },
                { "F", new Set(new [] { 11, 12, 13, 14, 15, 16, 17, 18 }) },
            };
            var set = SetEvaluator.Evaluate(expression, setsDict);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression (medium dificulty)";
            LogMessages[LogMessageType.Input] = $"{expression},\nsetsDict = " + DictionaryToListOfSets(setsDict);
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 1, 2, 3, 7, 8, 9, 11, 13, 14, 15, 16, 17, 18 }).ToString();
            LogMessages[LogMessageType.Output] = set.ToString();
            Log();
        }
        static public void CartesianProduct()
        {
            var arrayA = new int[] { 1, 2 };
            var arrayB = new char[] { 'a', 'b' };
            var setA = new Set(arrayA);
            var setB = new Set(arrayB);
            var cartesianSet = Set.CartesianProduct(setA, setB);
            setA.CartesianProduct(setB);

            LogMessages[LogMessageType.Task] = "Given two sets, compute the cartesian product";
            LogMessages[LogMessageType.Input] = $"[ {string.Join(", ", arrayA)} ], [ {string.Join(", ", arrayB)} ]";
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new (int, char)[] { (1, 'a'), (1, 'b'), (2, 'a'), (2, 'b') }).ToString();
            LogMessages[LogMessageType.OutputStatic] = cartesianSet.ToString();
            LogMessages[LogMessageType.Output] = setA.ToString();
            Log();
        }
        static public void IsRelationValid()
        {
            var relation = new (int, char)[] { (1, 'a'), (2, 'b') };
            var arrayA = new int[] { 1, 2 };
            var arrayB = new char[] { 'a', 'b' };
            var relationSet = new Set(relation);
            var setA = new Set(arrayA);
            var setB = new Set(arrayB);
            var isRelationValidStatic = Set.IsRelationValid(relationSet, setA, setB);
            var isRelationValid = setA.IsRelationValid(relationSet, setB);

            LogMessages[LogMessageType.Task] = "Validate if a given relation is valid for the Cartesian product of two sets";
            LogMessages[LogMessageType.Input] = $"{relationSet} , [ {string.Join(", ", arrayA)} ], [ {string.Join(", ", arrayB)} ]";
            LogMessages[LogMessageType.ExpectedOutput] = true.ToString();
            LogMessages[LogMessageType.OutputStatic] = isRelationValidStatic.ToString();
            LogMessages[LogMessageType.Output] = isRelationValid.ToString();
            Log();
        }
        static public void FindRelations()
        {
            var relationFunction = (int a, int b) => a % b == 0;
            var array = new int[] { 1, 2, 3, 4, 6 };
            var set = new Set(array);
            var relationsSetStatic = Set.FindRelations(set, relationFunction);
            var relationsSet = set.FindRelations(relationFunction);

            LogMessages[LogMessageType.Task] = "Find all the relations for a given set based on a relation function";
            LogMessages[LogMessageType.Input] = $"[ {string.Join(", ", array)} ], {nameof(relationFunction)}";
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new (int, int)[] { (2, 1), (3, 1), (4, 1), (4, 2), (6, 1), (6, 2), (6, 3) }).ToString();
            LogMessages[LogMessageType.OutputStatic] = relationsSetStatic.ToString();
            LogMessages[LogMessageType.Output] = relationsSet.ToString();
            Log();
        }
        static public void FilteredCartesianProduct()
        {
            var filterFunction = (int a, int b) => a < b;
            var arrayA = new int[] { 1, 2, 3 };
            var arrayB = new int[] { 3, 4, 5 };
            var setA = new Set(arrayA);
            var setB = new Set(arrayB);
            var cartesianSet = Set.CartesianProduct(setA, setB, filterFunction);
            setA.CartesianProduct(setB, filterFunction);

            LogMessages[LogMessageType.Task] = "Given two sets and filter function, compute the cartesian product which only includes pairs that satisfy the filter function";
            LogMessages[LogMessageType.Input] = $"filter => {{ a < b }}, [ {string.Join(", ", arrayA)} ], [ {string.Join(", ", arrayB)} ]";
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new (int, int)[] { (1, 3), (1, 4), (1, 5), (2, 3), (2, 4), (2, 5), (3, 4), (3, 5) }).ToString();
            LogMessages[LogMessageType.OutputStatic] = cartesianSet.ToString();
            LogMessages[LogMessageType.Output] = setA.ToString();
            Log();
        }

        public static void RunAll()
        {
            CreateSet();
            AddElement();
            RemoveElement();
            ContainsElement();
            Union();
            Intersection();
            Difference();
            Complement();
            EvaluateEasy_1();
            EvaluateEasy_2();
            EvaluateMedium_1();
            EvaluateMedium_2();
            CartesianProduct();
            IsRelationValid();
            FindRelations();
            FilteredCartesianProduct();
        }
    }
}
