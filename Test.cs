namespace Laboratory
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

        static public void CreateSet()
        {
            var set = new Set<int>(new int[] { 1, 2, 2, 3 });

            LogMessages[LogMessageType.Task] = "Create a set from a list of elements, removing any duplicates";
            LogMessages[LogMessageType.Input] = "[1,2,2,3]";
            LogMessages[LogMessageType.ExpectedOutput] = "[1,2,3]";
            LogMessages[LogMessageType.Ouput] = set.ToString();
            Log();
        }
        static public void AddElement()
        {
            var set = new Set<int>(new int[] { 1, 2, 3 });
            int newElement = 4;
            set.AddElement(newElement);

            LogMessages[LogMessageType.Task] = "Add an element to the set if it's not already present";
            LogMessages[LogMessageType.Input] = "[1,2,3,4], 4";
            LogMessages[LogMessageType.ExpectedOutput] = "[1,2,3,4]";
            LogMessages[LogMessageType.Ouput] = set.ToString();
            Log();
        }
        static public void RemoveElement()
        {
            var set = new Set<int>(new int[] { 1, 2, 3, 4 });
            int elementToRemove = 4;
            set.RemoveElement(elementToRemove);

            LogMessages[LogMessageType.Task] = "Remove an element if it exists in the set";
            LogMessages[LogMessageType.Input] = "[1,2,3,4], 4";
            LogMessages[LogMessageType.ExpectedOutput] = "[1,2,3]";
            LogMessages[LogMessageType.Ouput] = set.ToString();
            Log();
        }
        static public void ContainsElement()
        {
            var set = new Set<int>(new int[] { 1, 2, 3 });
            int element = 4;

            LogMessages[LogMessageType.Task] = "Return a boolean indicating if an element is present in the set";
            LogMessages[LogMessageType.Input] = "[1,2,3], 4";
            LogMessages[LogMessageType.ExpectedOutput] = "False";
            LogMessages[LogMessageType.Ouput] = set.Contains(element).ToString();
            Log();
        }
        static public void Union()
        {
            var setA = new Set<int>(new int[] { 1, 2, 3 });
            var setB = new Set<int>(new int[] { 3, 4, 5 });
            var unionSet = setA.Union(setB);
            var staticUnionSet = Set<int>.Union(setA, setB);

            LogMessages[LogMessageType.Task] = "Return a new set that's the union of the two sets";
            LogMessages[LogMessageType.Input] = "[1,2,3], [3,4,5]";
            LogMessages[LogMessageType.ExpectedOutput] = "[1,2,3,4,5]";
            LogMessages[LogMessageType.Ouput] = unionSet.ToString();
            LogMessages[LogMessageType.OutputStatic] = staticUnionSet.ToString();
            Log();
        }
        static public void Intersection()
        {
            var setA = new Set<int>(new int[] { 1, 2, 3 });
            var setB = new Set<int>(new int[] { 3, 4, 5 });
            var intersectionSet = setA.Intersection(setB);
            var staticIntersectionSet = Set<int>.Intersection(setA, setB);

            LogMessages[LogMessageType.Task] = "Return a new set that's the intersection of the two sets";
            LogMessages[LogMessageType.Input] = "[1,2,3], [3,4,5]";
            LogMessages[LogMessageType.ExpectedOutput] = "[3]";
            LogMessages[LogMessageType.Ouput] = intersectionSet.ToString();
            LogMessages[LogMessageType.OutputStatic] = staticIntersectionSet.ToString();
            Log();
        }
        static public void Difference()
        {
            var setA = new Set<int>(new int[] { 1, 2, 3 });
            var setB = new Set<int>(new int[] { 3, 4, 5 });
            var differenceSet = setA.Difference(setB);
            var staticDifferenceSet = Set<int>.Difference(setA, setB);

            LogMessages[LogMessageType.Task] = "Return a set containing elements in setA but not in setB";
            LogMessages[LogMessageType.Input] = "[1,2,3], [3,4,5]";
            LogMessages[LogMessageType.ExpectedOutput] = "[1,2]";
            LogMessages[LogMessageType.Ouput] = differenceSet.ToString();
            LogMessages[LogMessageType.OutputStatic] = staticDifferenceSet.ToString();
            Log();
        }
        static public void Complement()
        {
            var set = new Set<int>(new int[] { 1, 2, 3 });
            var universalSet = new Set<int>(new int[] { 3, 4, 5 });
            var complementSet = set.Complement(universalSet);
            var staticComplementSet = Set<int>.Complement(set, universalSet);

            LogMessages[LogMessageType.Task] = "Return the complement of setA in relation to a universal set";
            LogMessages[LogMessageType.Input] = "[1,2,3], [1,2,3,4,5]";
            LogMessages[LogMessageType.ExpectedOutput] = "[4,5]";
            LogMessages[LogMessageType.Ouput] = complementSet.ToString();
            LogMessages[LogMessageType.OutputStatic] = staticComplementSet.ToString();
            Log();
        }
        static public void EvaluateEasy_1()
        {
            var input = "A intersection B union C";
            var sets = new Dictionary<string, Set<int>>();
            sets["A"] = new Set<int>(new int[] { 1, 2, 3 });
            sets["B"] = new Set<int>(new int[] { 3, 4, 5 });
            sets["C"] = new Set<int>(new int[] { 5, 6, 7 });

            var evaluator = new SetEvaluator<int>(sets);
            var set = evaluator.Evaluate(input);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression";
            LogMessages[LogMessageType.Input] = "\"A intersection B union C\",\nsetsDict = {\n'A': [1,2,3],\n'B': [3,4,5],\n'C': [5,6,7]\n}";
            LogMessages[LogMessageType.ExpectedOutput] = "[3,5,6,7]";
            LogMessages[LogMessageType.Ouput] = set.ToString();
            Log();
        }

        static public void EvaluateEasy_2()
        {
            var input = "A intersection B union C difference D complement E";
            var sets = new Dictionary<string, Set<int>>();
            sets["A"] = new Set<int>(new int[] { 1, 2, 3 });
            sets["B"] = new Set<int>(new int[] { 3, 4, 5 });
            sets["C"] = new Set<int>(new int[] { 5, 6, 7 });
            sets["D"] = new Set<int>(new int[] { 10, 4, 12, 5, 6 });
            sets["E"] = new Set<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            var evaluator = new SetEvaluator<int>(sets);
            var set = evaluator.Evaluate(input);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression";
            LogMessages[LogMessageType.Input] = "\"A intersection B union C difference D complement E\",\nsetsDict = {\n'A': [1,2,3],\n'B': [3,4,5],\n'C': [5,6,7]\n'D': [10,4,12,5,6]\n'E': [1,2,3,4,5,6,7,8,9,10]\n}";
            LogMessages[LogMessageType.ExpectedOutput] = "[1,2,4,5,6,8,9,10]";
            LogMessages[LogMessageType.Ouput] = set.ToString();
            Log();
        }

        static public void EvaluateMedium_1()
        {
            var input = "(A union B) intersection (C difference D) complement (E union F)";
            var sets = new Dictionary<string, Set<int>>();
            sets["A"] = new Set<int>(new int[] { 1, 2, 3, 4 });
            sets["B"] = new Set<int>(new int[] { 5, 8, 9, 6 });
            sets["C"] = new Set<int>(new int[] { 4, 6, 7, 10 });
            sets["D"] = new Set<int>(new int[] { 10, 4, 12, 5, 6 });
            sets["E"] = new Set<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            sets["F"] = new Set<int>(new int[] { 11, 12, 13, 14, 15, 16, 17, 18 });

            var evaluator = new SetEvaluator<int>(sets);
            var set = evaluator.Evaluate(input);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression (medium dificulty)";
            LogMessages[LogMessageType.Input] = "\"(A union B) intersection (C difference D) complement (E union F)\"\nsetsDict = {\n'A': [1,2,3,4],\n'B': [5,8,9,6],\n'C': [4,6,7,10],\n'D': [10,4,12,5,6],\n'E': [1,2,3,4,5,6,7,8,9,10],\n'F': [11,12,13,14,15,16,17,18]\n}";
            LogMessages[LogMessageType.ExpectedOutput] = "[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18]";
            LogMessages[LogMessageType.Ouput] = set.ToString();
            Log();
        }

        static public void EvaluateMedium_2()
        {
            var input = "(((A union B) intersection C) difference D) union (D complement (E union F))";
            var sets = new Dictionary<string, Set<int>>();
            sets["A"] = new Set<int>(new int[] { 1, 2, 3, 4 });
            sets["B"] = new Set<int>(new int[] { 5, 8, 9, 6 });
            sets["C"] = new Set<int>(new int[] { 4, 6, 7, 10 });
            sets["D"] = new Set<int>(new int[] { 10, 4, 12, 5, 6 });
            sets["E"] = new Set<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            sets["F"] = new Set<int>(new int[] { 11, 12, 13, 14, 15, 16, 17, 18 });

            var evaluator = new SetEvaluator<int>(sets);
            var set = evaluator.Evaluate(input);

            LogMessages[LogMessageType.Task] = "Given a string expression and a dictionary of sets, compute the result of the expression (medium dificulty)";
            LogMessages[LogMessageType.Input] = "\"(((A union B) intersection C) difference D) union (D complement (E union F))\"\nsetsDict = {\n'A': [1,2,3,4],\n'B': [5,8,9,6],\n'C': [4,6,7,10],\n'D': [10,4,12,5,6],\n'E': [1,2,3,4,5,6,7,8,9,10],\n'F': [11,12,13,14,15,16,17,18]\n}";
            LogMessages[LogMessageType.ExpectedOutput] = "[1,2,3,7,8,9,11,13,14,15,16,17,18]";
            LogMessages[LogMessageType.Ouput] = set.ToString();
            Log();
        }
    }
}
