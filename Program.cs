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


Lab #2: "Cartesian Products and Relations"
Objective: Understand and implement the Cartesian product of sets and related operations.
    1.	Basic Cartesian Product (Score: 60-74)
        •	cartesianProduct(setA, setB): Generates the Cartesian product of two sets.
            •	Input: ([1,2], ['a','b'])
            •	Output: [(1,'a'), (1,'b'), (2,'a'), (2,'b')]
    2.	Relation Testing and Advanced Operations (Score: 75-89)
        •	isRelationValid(relation, setA, setB): Validates if a given relation (list of ordered pairs) is valid for the Cartesian product of two sets.
            •	Input: ([(1,'a'), (2,'b')], [1,2], ['a','b'])
            •	Output: True
        •	findRelations(setA, relationFunc): Finds all the relations for a given set based on a relation function.
            •	Example function: "All numbers divisible by another number in the set".
            •	Input: ([1,2,3,4,6], isDivisible)
            •	Output: [(2,1), (3,1), (4,1), (4,2), (6,1), (6,2), (6,3)]
    3.	Advanced Cartesian Product with Filters (Score: 90-100)
        •	filteredCartesianProduct(setA, setB, filterFunc): Generates the Cartesian product, but only includes pairs that satisfy the filter function.
            •	Example function: "Only pairs where a number from setA is less than a number from setB".
            •	Input: ([1,2,3], [3,4,5], filterFunction)
            •	Output: [(1,3), (1,4), (1,5), (2,3), (2,4), (2,5), (3,4), (3,5)]
 */

namespace Laboratory
{
    class Program
    {
        public static bool IsDivisible(int a, int b)
        {
            return a % b == 0;
        }
        public static bool IsLess(decimal a, decimal b) 
        {
            return a < b;
        }
        static void Main(string[] args)
        {
            Test.RunAll();
        }
    }

}

