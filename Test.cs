﻿namespace Laboratory
{
    class Test
    {
        private enum LogMessageType
        {
            Task,
            Input,
            ExpectedOutput,
            Ouput,
            OutputStatic
        }

        private static Dictionary<LogMessageType, string> LogMessages = new Dictionary<LogMessageType, string>()
        {
            { LogMessageType.Task, string.Empty },
            { LogMessageType.Input, string.Empty },
            { LogMessageType.ExpectedOutput, string.Empty },
            { LogMessageType.Ouput, string.Empty },
            { LogMessageType.OutputStatic, string.Empty },
        };

        private static void Log()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            
            Console.WriteLine("Test task: " + LogMessages[LogMessageType.Task]);
            Console.WriteLine("Input: " + LogMessages[LogMessageType.Input]);
            Console.WriteLine("Expected output: " + LogMessages[LogMessageType.ExpectedOutput]);

            bool outputAreEqual = LogMessages[LogMessageType.Ouput] == LogMessages[LogMessageType.ExpectedOutput];
            if (outputAreEqual) Console.ForegroundColor = ConsoleColor.Green;
            else Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"Output: {LogMessages[LogMessageType.Ouput]}");
            if (!string.IsNullOrEmpty(LogMessages[LogMessageType.OutputStatic]))
            {
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
            LogMessages[LogMessageType.Ouput] = set.ToString();
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
            LogMessages[LogMessageType.Ouput] = set.ToString();
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
            LogMessages[LogMessageType.Ouput] = set.ToString();
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
            LogMessages[LogMessageType.Ouput] = isSetContainsElement.ToString();
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
            LogMessages[LogMessageType.Ouput] = setA.ToString();
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
            LogMessages[LogMessageType.Ouput] = setA.ToString();
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
            LogMessages[LogMessageType.Ouput] = setA.ToString();
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
            LogMessages[LogMessageType.Ouput] = set.ToString();
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

            var evaluator = new SetEvaluator<int>(setsDict);
            var set = evaluator.Evaluate(expression);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression";
            LogMessages[LogMessageType.Input] = $"{expression},\nsetsDict = " + DictionaryToListOfSets(setsDict);
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 3, 5, 6, 7 }).ToString();
            LogMessages[LogMessageType.Ouput] = set.ToString();
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

            var evaluator = new SetEvaluator<int>(setsDict);
            var set = evaluator.Evaluate(expression);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression";
            LogMessages[LogMessageType.Input] = $"{expression},\nsetsDict = " + DictionaryToListOfSets(setsDict);
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 1, 2, 4, 5, 6, 8, 9, 10 }).ToString();
            LogMessages[LogMessageType.Ouput] = set.ToString();
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

            var evaluator = new SetEvaluator<int>(setsDict);
            var set = evaluator.Evaluate(expression);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression (medium dificulty)";
            LogMessages[LogMessageType.Input] = $"{expression},\nsetsDict = " + DictionaryToListOfSets(setsDict);
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 }).ToString();
            LogMessages[LogMessageType.Ouput] = set.ToString();
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

            var evaluator = new SetEvaluator<int>(setsDict);
            var set = evaluator.Evaluate(expression);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression (medium dificulty)";
            LogMessages[LogMessageType.Input] = $"{expression},\nsetsDict = " + DictionaryToListOfSets(setsDict);
            LogMessages[LogMessageType.ExpectedOutput] = new Set(new int[] { 1, 2, 3, 7, 8, 9, 11, 13, 14, 15, 16, 17, 18 }).ToString();
            LogMessages[LogMessageType.Ouput] = set.ToString();
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
        }
    }
}
