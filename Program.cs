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
/*
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
/*
Lab #3: "Advanced Propositional Logic and Computational Logic"
    1.  Complex Logical Expressions Evaluation
        •	Input:
            •	Expression: "(A AND B) OR (NOT C)"
            •	Values: {"A": true, "B": false, "C": true}
        •	Output: false
    Task 2: Automated Truth Table Generation
        •	Input:
                •	Expression: "(A AND B) OR C"
        •	Output:
        •	Truth Table:
                         A     | B     | C     | (A AND B) OR C
                         --------------------------------------
                         true  | true  | true  | true
                         true  | true  | false | true
                         true  | false | true  | true
                         true  | false | false | false
                         false | true  | true  | true
                         false | true  | false | false
                         false | false | true  | true
                         false | false | false | false
*/
/*
Lab #4: "???"
    Task 1: Parity Checker
    Objective: Write a program to determine the parity of a given number.
        •	Input:
            •	Integer: 25
        •	Output:
            •	Parity: Odd
    Instructions: Implement a function that accepts an integer and returns its parity as "Even" or "Odd". Explain the logic and algorithm used in your implementation.
    ________________________________________
    Task 2: Prime Number Checker
    Objective: Implement a program that checks if a given number is prime.
        •	Input:
            •	Integer: 17
        •	Output:
            •	Prime Status: Prime
    Instructions: Develop a function that takes an integer input and returns whether it is "Prime" or "Not Prime". Provide an explanation of the method used to determine the primality.
    ________________________________________
    Task 3: GCD Calculator
    Objective: Create a program to calculate the Greatest Common Divisor (GCD) of two numbers.
        •	Input:
            •	Two integers: 48, 18
        •	Output:
            •	GCD: 6
    Instructions: Implement a function that calculates the GCD of two integers. Explain the algorithm and steps involved in the calculation.
    ________________________________________
    Medium Tasks (75-89 points)
    Task 4: Prime Factorization
    Objective: Write a program to find the prime factorization of a given number.
        •	Input:
            •	Integer: 56
        •	Output:
            •	Prime Factors: 2^3 * 7
    Instructions: Implement a function to find the prime factors and their powers. The program should provide the result in the format "p1^e1 * p2^e2 * ...".
    ________________________________________
    Task 5: LCM Calculator
    Objective: Create a program to compute the Least Common Multiple (LCM) of two integers.
        •	Input:
            •	Two integers: 15, 20
        •	Output:
            •	LCM: 60
    Instructions: Develop a function to calculate the LCM, and explain the method used for the calculation and how it relates to the GCD.
    ________________________________________
    Task 6: Direct Proof Implementation
    Objective: Write a program that demonstrates a direct proof method for a given mathematical statement.
        •	Input:
            •	Statement: “If a number is even, then it is divisible by 2.”
        •	Output:
            •	Proof steps and validation
    Instructions: Implement an algorithm that validates the statement through direct proof. Explain each step and the logical reasoning behind it.
    ________________________________________
    Hard Tasks (90-100 points)
    Task 7: Advanced Number Theory Function
    Objective: Implement a program that performs a complex operation involving number theory concepts, like Euler’s Totient Function.
        •	Input:
            •	Integer: 12
        •	Output:
            •	Euler’s Totient Value: 4
    Instructions: Implement Euler’s Totient Function and provide an explanation of the algorithm and steps in the computation.
    ________________________________________
    Task 8: Comprehensive Proof by Induction
    Objective: Develop a program that can prove mathematical statements using the principle of mathematical induction.
        •	Input:
            •	Statement: “The sum of the first n odd numbers is n^2 for all positive integers n.”
        •	Output:
            •	Proof steps and validation
    Instructions: The program should facilitate proof by induction, demonstrating base case verification, induction hypothesis, and induction step.
    ________________________________________
    Task 9: Advanced Function Evaluation
    Objective: Implement a program to evaluate complex mathematical functions with considerations to their domains and ranges.
        •	Input:
            •	Function: f(x) = x^2 + 2x + 1
            •	Value: x = 3
        •	Output:
            •	Evaluated Result: 16
    Instructions: The program should evaluate the function, consider its domain and range, and provide an explanation of the evaluation process and results.
 */
/*
Lab #5: "???"
    Easy Tasks (60-74 points)
        Task 1: Function Evaluator
        Objective: Write a program to evaluate a simple linear function f(x) = mx + b.
            •	Input:
                •	m (slope): 2
                •	b (y-intercept): 3
                •	x (value): 4
            •	Output:
                •	f(x): 11
        Instructions: Implement a function that accepts three parameters m, b, and x and returns the value of f(x). 

        Task 2: Domain and Range Identifier
        Objective: Implement a program that identifies the domain and range of a given set of ordered pairs.
            •	Input:
                •	Set of ordered pairs: {(1, 2), (3, 6), (4, 8)}
            •	Output:
                •	Domain: {1, 3, 4}
                •	Range: {2, 6, 8}
        Instructions: Develop a function that takes a list of ordered pairs and returns two sets: one for the domain and one for the range.

        Task 3: Even/Odd Function Identifier
        Objective: Create a program that determines if a given function is even, odd, or neither based on its graph.
            •	Input:
                •	Graphical function data (as a list of points): [(-1,1), (0,0), (1,1)]
            •	Output:
                •	Function Type: Even
        Instructions: Implement a function that processes a list of points representing a function's graph and determines if the function is even or odd. 
    
    Medium Tasks (75-89 points)
        Task 4: Injective Function Validator
        Objective: Create a program that checks if a function is injective based on a set of input-output pairs.
            •	Input:
                •	Set of input-output pairs: {(2, 4), (3, 6), (4, 8)}
            •	Output:
                •	Injective Status: True
        Instructions: Develop a function that takes a list of input-output pairs and determines if the function is injective.

        Task 5: Surjective Function Checker
        Objective: Implement a program that checks if a function is surjective from its domain to a given codomain.
            •	Input:
                •	Function pairs: {(1, 2), (2, 3), (3, 4)}
                •	Codomain: {2, 3, 4}
            •	Output:
                •	Surjective Status: True
        Instructions: Implement a function that validates if a given function is surjective.

    Hard Tasks (90-100 points)
        Task 6: Function Combination Tool
        Objective: Implement a program that combines two functions f(x) and g(x) by addition, subtraction, multiplication, or division.
            •	Input:
                •	f(x) = x^2
                •	g(x) = 2x + 1
                •	Operation: Addition
                •	Value: x = 3
            •	Output:
                •	Result: 18
        Instructions: Develop a program that takes two functions, an operation, and a value, then returns the result of the combination.

        Task 7: Graph Information Extractor
        Objective: Write a program to extract and report key features from a function’s graph such as intercepts, maxima, and minima.
            •	Input:
                •	Graphical function data (as a list of points): [(-2, -4), (-1, -1), (0, 0), (1, 1), (2, 4)]
            •	Output:
                •	x-intercepts: {0}
                •	y-intercepts: {0}
                •	Maxima: None
                •	Minima: None
        Instructions: Implement a function that analyzes a list of points to determine the graph's intercepts, maxima, and minima. Discuss the method for finding these features in a discrete set of points.
 */


namespace Laboratory
{
    class Program
    {
        static void Main(string[] args)
        {
            var points = new (double, double)[] { (-2, -4), (-1, -1), (0, 2), (1, 1), (2, 4) };

            var result = Math.GraphInformationExtractor(points);

            Console.WriteLine(result);
        }
    }
}

