/*
Lab #1: "Basic Set Operations"
Objective: Implement foundational set operations without using pre-built libraries or classes.
Requirements:
•	Sets can contain integers, strings, or any data type.
•	Do not use built-in set classes or their methods. Examples:
    •	C#: Avoid HashSet<T>
    •	Python: Do not use set()
    •	Java: Avoid HashSet<E>
    •	JavaScript: Do not use Set
    •	And so on…

Tasks:
    1.	Set Creation and Manipulation (Score: 60-74)
        •	createSet(elements): Creates a set from a list of elements, removing any duplicates.
            •	Input: [1,2,2,3]
            •	Output: [1,2,3]
        •	addElement(set, element): Adds an element to the set if it's not already present.
            •	Input: ([1,2,3], 4)
            •	Output: [1,2,3,4]
        •	removeElement(set, element): Removes an element if it exists in the set.
            •	Input: ([1,2,3,4], 4)
            •	Output: [1,2,3]
        •	containsElement(set, element): Returns a boolean indicating if an element is present in the set.
            •	Input: ([1,2,3], 4)
            •	Output: False
    2.	Advanced Set Operations (Score: 75-89)
        •	union(setA, setB): Returns a new set that's the union of the two sets.
            •	Input: ([1,2,3], [3,4,5])
            •	Output: [1,2,3,4,5]
        •	intersection(setA, setB): Returns a new set that's the intersection of the two sets.
            •	Input: ([1,2,3], [3,4,5])
            •	Output: [3]
        •	difference(setA, setB): Returns a set containing elements in setA but not in setB.
            •	Input: ([1,2,3], [3,4,5])
            •	Output: [1,2]
        •	complement(setA, universalSet): Returns the complement of setA in relation to a universal set.
            •	Input: ([1,2,3], [1,2,3,4,5])
            •	Output: [4,5]
    3.	Expression Evaluator (Score: 90-100)
        •	evaluateExpression(expression, setsDict): Given a string expression and a dictionary of sets, compute the result of the expression.
        •	Input:
            •	Expression: "A intersection B union C"
            •	setsDict = {'A': [1,2,3], 'B': [3,4,5], 'C': [5,6,7]}
        •	Output: [3,5,6,7]
 */

// Ех... Тут би паттерн Команда

namespace Laboratory_1
{
    internal class Program
    {
        static private void CreateSet()
        {
            Console.WriteLine("Task: Create a set from a list of elements, removing any duplicates");
            Console.WriteLine("Input: [1,2,2,3]");
            Console.WriteLine("Expected output: [1,2,3]\n");

            Set<int> set = new Set<int>(new int[] { 1, 2, 2, 3 });
            
            Console.WriteLine($"Output: {set}\n\n");
        }
        static private void AddElement()
        {
            Console.WriteLine("Task: Add an element to the set if it's not already present");
            Console.WriteLine("Input: [1,2,3], 4");
            Console.WriteLine("Expected output: [1,2,3,4]\n");

            Set<int> set = new Set<int>(new int[] { 1, 2, 3 });
            int newElement = 4;
            set.AddElement(newElement);

            Console.WriteLine($"Output: {set}\n\n");
        }
        static private void RemoveElement()
        {
            Console.WriteLine("Task: Remove an element if it exists in the set");
            Console.WriteLine("Input: [1,2,3,4], 4");
            Console.WriteLine("Expected output: [1,2,3]\n");

            Set<int> set = new Set<int>(new int[] { 1, 2, 3, 4 });
            int elementToRemove = 4;
            set.RemoveElement(elementToRemove);

            Console.WriteLine($"Output: {set}\n\n");
        }
        static private void ContainsElement()
        {
            Console.WriteLine("Task: Return a boolean indicating if an element is present in the set");
            Console.WriteLine("Input: [1,2,3], 4");
            Console.WriteLine("Expected output: False\n");

            Set<int> set = new Set<int>(new int[] { 1, 2, 3 });
            int elementToFind = 4;

            Console.WriteLine($"Output: {set.Contains(elementToFind)}\n\n");
        }
        static private void Union()
        {
            Console.WriteLine("Task: Return a new set that's the union of the two sets");
            Console.WriteLine("Input: [1,2,3], [3,4,5]");
            Console.WriteLine("Expected output: [1,2,3,4,5]\n");

            Set<int> setA = new Set<int>(new int[] { 1, 2, 3 });
            Set<int> setB = new Set<int>(new int[] { 3, 4, 5 });
            Set<int> union = setA.Union(setB);

            Console.WriteLine($"Output: {union}\n\n");
        }
        static private void Intersection()
        {
            Console.WriteLine("Task: Return a new set that's the intersection of the two sets");
            Console.WriteLine("Input: [1,2,3], [3,4,5]");
            Console.WriteLine("Expected output: [3]\n");

            Set<int> setA = new Set<int>(new int[] { 1, 2, 3 });
            Set<int> setB = new Set<int>(new int[] { 3, 4, 5 });
            Set<int> intersection = setA.Intersection(setB);

            Console.WriteLine($"Output: {intersection}\n\n");
        }
        static private void Difference()
        {
            Console.WriteLine("Task: Return a new set that's the intersection of the two sets");
            Console.WriteLine("Input: [1,2,3], [3,4,5]");
            Console.WriteLine("Expected output: [1,2]\n");

            Set<int> setA = new Set<int>(new int[] { 1, 2, 3 });
            Set<int> setB = new Set<int>(new int[] { 3, 4, 5 });
            Set<int> difference = setA.Difference(setB);

            Console.WriteLine($"Output: {difference}\n\n");
        }
        static private void Complement()
        {
            Console.WriteLine("Task: Return the complement of setA in relation to a universal set");
            Console.WriteLine("Input: [1,2,3], [1,2,3,4,5]");
            Console.WriteLine("Expected output: [4,5]\n");

            Set<int> setA = new Set<int>(new int[] { 1, 2, 3 });
            Set<int> setB = new Set<int>(new int[] { 3, 4, 5 });
            Set<int> complement = setA.Complement(setB);

            Console.WriteLine($"Output: {complement}\n\n");
        }

        static private void Evaluate()
        {

        }
        // Eval expression
        // Separate by the space
        // Find and extract keywords
        // Now, first list is for set names and the second one is for set operations

        static void Main(string[] args)
        {
            CreateSet();
            AddElement();
            RemoveElement();
            ContainsElement();
            Union();
            Intersection();
            Difference();
            Complement();
        }
    }
}