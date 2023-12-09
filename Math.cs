using System.ComponentModel;
using System.Linq;

namespace Laboratory
{
    namespace DiscreteMath
    {
        public partial class FunctionSet
        {
            private List<List<object>> _domains;
            private List<object> _codomain;
            public List<HashSet<object>> Domains => GetDomains();
            public HashSet<object> Codomain => GetCodomain();
            public NegotiationMap Negotiator { get; private set; }
            public ParityProperties Parity => GetParityProperties();
            public MappingProperties Mapping => GetMappingProperties();
            public FunctionSet(int numberOfParameters, NegotiationMap? negotiationMap = null)
            {
                if (numberOfParameters > 0)
                {
                    _codomain = new List<object>();
                    _domains = new List<List<object>>();
                    for (int i = 0; i < numberOfParameters; i++)
                    {
                        _domains.Add(new List<object>());
                    }
                    Negotiator = negotiationMap is not null ? negotiationMap : new NegotiationMap();
                }
                else
                {
                    throw new ArgumentException("Number of parameters should be greater than zero.");
                }
            }
            public FunctionSet(HashSet<ParametersValuePair> pairs, NegotiationMap? negotiationMap = null)
            {
                if (pairs.Count <= 0)
                {
                    throw new ArgumentException("Number of pairs should be greater than zero.");
                }
                else if (pairs.Any(pair => pair.Parameters.Length != pairs.First().Parameters.Length))
                {
                    throw new ArgumentException("Number of pair's parameters of each pair should be equal.");
                }

                _codomain = new List<object>();
                _domains = new List<List<object>>();
                var numberOfParameters = pairs.First().Parameters.Length;
                for (int i = 0; i < numberOfParameters; i++)
                {
                    _domains.Add(new List<object>());
                }
                foreach (var pair in pairs)
                {
                    _codomain.Add(pair.Value);
                    for (int i = 0; i < pair.Parameters.Length; i++)
                    {
                        _domains[i].Add(pair.Parameters[i]);
                    }
                }
                Negotiator = negotiationMap is not null ? negotiationMap : new NegotiationMap();
            }

            #region Domains Methods
            public List<HashSet<object>> GetDomains()
            {
                return new List<HashSet<object>>(from domain in _domains select new HashSet<object>(domain));
            }
            public void SetDomains(List<List<object>> newDomains)
            {
                if (newDomains.Any(domain => domain.Count != _codomain.Count) is false)
                {
                    _domains = newDomains;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("domain.Count", "Number of parameters of each domain should be equal to number of codomain values.");
                }
            }
            public void AddDomain(List<object> newDomain)
            {
                if (newDomain.Count == _codomain.Count)
                {
                    _domains.Add(newDomain);
                }
                else
                {
                    throw new ArgumentException($"Number of parameters in new domain should be {_codomain.Count}.");
                }
            }
            public bool RemoveDomain(int index)
            {
                if (index < 0 || index > _codomain.Count) return false;
                _domains.RemoveAt(index);
                return true;
            }
            #endregion

            #region Codomain Methods
            public HashSet<object> GetCodomain()
            {
                return new HashSet<object>(_codomain);
            }
            public void SetCodomain(List<object> newCodomain)
            {
                if (newCodomain.Count == _domains[0].Count)
                {
                    _codomain = newCodomain;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("domain.Count", "Number of parameters of each domain should be equal to number of codomain values.");
                }
            }
            #endregion

            public void Add(object value, params object[] parameters)
            {
                if (parameters.Length == _domains.Count)
                {
                    for (int i = 0; i < _domains.Count; i++)
                    {
                        _domains[i].Add(parameters[i]);
                    }
                    _codomain.Add(value);
                }
                else
                {
                    throw new ArgumentException($"Number of parameters should be {_domains.Count}.");
                }
            }
            public bool RemoveAt(int index)
            {
                if (index >= 0 && index < _codomain.Count)
                {
                    _domains.ForEach(domain => domain.RemoveAt(index));
                    _codomain.RemoveAt(index);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public List<ParametersValuePair> GetAllPairs()
            {
                var pairs = new List<ParametersValuePair>();
                for (int i = 0; i < _codomain.Count; i++)
                {
                    var value = _codomain[i];
                    var parameters = (from domain in _domains select domain[i]).ToArray();
                    pairs.Add(new ParametersValuePair(value, parameters));
                }
                return pairs;
            }
            public ParityProperties GetParityProperties(int domainIndex = 0)
            {
                if (domainIndex >= 0 && domainIndex < _domains.Count)
                {
                    bool isEven = true;
                    bool isOdd = true;
                    var domain = _domains[domainIndex];
                    for (int i = 0; i < domain.Count; i++)
                    {
                        // Check if the function is Even
                        // Pair: if value(codomain[i]) for parameter(domain[i]) then it should be the same for negotiatied parameter(-domain[i])
                        // (x, y) => (-x, y)
                        Negotiator.TryGetNegotiation(domain[i], out object? negativeX);
                        if (negativeX is null || !domain.Contains(negativeX))
                        {
                            isEven = false;
                            isOdd = false;
                            break;
                        }
                        for (int j = 0; j < domain.Count; j++)
                        {
                            if (domain[j].Equals(negativeX) && _codomain[j].Equals(_codomain[i]))
                            {
                                isEven = false;
                                break;
                            }
                        }

                        // Check if the function is Odd
                        // Odd: if for parameter(domain[i]) the value(codomain[i]) then for negotiatied parameter(-domain[i]) the value should be also negotiatied(-codomain[i])
                        // (x, y) => (-x, -y)
                        Negotiator.TryGetNegotiation(_codomain[i], out object? negativeY);
                        if (negativeY is null || !_codomain.Contains(negativeY)) // That isn't good
                        {
                            isOdd = false;
                            break;
                        }
                        for (int j = 0; j < _codomain.Count; j++)
                        {
                            if (domain[j].Equals(negativeX) && !_codomain[j].Equals(negativeY))
                            {
                                isOdd = false;
                                break;
                            }
                        }
                    }
                    return new ParityProperties(isEven, isOdd, domainIndex);
                }
                else
                {
                    throw new IndexOutOfRangeException($"Domain at index {domainIndex} doesn't exist");
                }
            }
            public bool IsInjective(int domainIndex = 0)
            {
                if (domainIndex >= 0 && domainIndex < _domains.Count)
                {
                    var isInjective = true;
                    var domain = _domains[domainIndex];
                    var map = new Dictionary<object, object>();
                    for (int i = 0; i < domain.Count; i++)
                    {
                        var x = domain[i];
                        var y = _codomain[i];

                        if (map.ContainsKey(x))
                        {
                            isInjective = false;
                            break;
                        }
                        else
                        {
                            if (map.ContainsValue(y))
                            {
                                isInjective = false;
                                break;
                            }
                            map.Add(x, y);
                        }
                    }
                    return isInjective;
                }
                else
                {
                    throw new IndexOutOfRangeException($"Domain at index {domainIndex} doesn't exist");
                }
            }
            public bool IsSurjective(int domainIndex = 0)
            {
                if (domainIndex >= 0 && domainIndex < _domains.Count)
                {
                    var isSurjective = true;
                    var domain = _domains[domainIndex];
                    var map = new Dictionary<object, object>();
                    for (int i = 0; i < domain.Count; i++)
                    {
                        var x = domain[i];
                        var y = _codomain[i];

                        if (map.ContainsKey(x))
                        {
                            return false;
                        }
                        else
                        {
                            if (map.Values.Contains(y))
                            {
                                isSurjective = false;
                                break;
                            }
                            map.Add(x, y);
                        }
                    }
                    return isSurjective;
                }
                else
                {
                    throw new IndexOutOfRangeException($"Domain at index {domainIndex} doesn't exist");
                }
            }
            // do not work
            public bool IsSurjective(List<object> codomain, int domainIndex = 0)
            {
                if (domainIndex < 0 && domainIndex >= _domains.Count)
                {
                    throw new IndexOutOfRangeException($"Domain at index {domainIndex} doesn't exist");
                }
                else if (codomain.Count != _domains[0].Count)
                {
                    throw new ArgumentException($"Number of codomain's elements should be equal to number of domain's elements");
                }

                var isSurjective = true;
                var domain = _domains[domainIndex];
                var map = new Dictionary<object, List<object>>();
                for (int i = 0; i < domain.Count; i++)
                {
                    var x = domain[i];
                    var y = _codomain[i];

                    if (map.ContainsKey(x))
                    {
                        map[x].Add(y);
                    }
                    else
                    {
                        map.Add(x, new List<object>() { y });
                    }
                }
                var range = map.Values.SelectMany(list => list).ToList();
                foreach (var value in codomain)
                {
                    if (!range.Contains(value))
                    {
                        isSurjective = false;
                        break;
                    }
                }
                return isSurjective;
            }
            public MappingProperties GetMappingProperties(int domainIndex = 0)
            {
                return new MappingProperties(IsInjective(domainIndex), IsSurjective(domainIndex), domainIndex);
            }
        }

        // All utils classes and structs
        public partial class FunctionSet
        {
            public class NegotiationMap
            {
                List<object> _values = new();
                List<object> _negotiations = new();
                public int Count => _values.Count;
                public NegotiationMap()
                {
                    _values = new List<object>();
                    _negotiations = new List<object>();
                }
                public NegotiationMap(IEnumerable<object> values, IEnumerable<object> negotiations)
                {
                    _values = values.ToList();
                    _negotiations = negotiations.ToList();
                }
                public void Add(object value, object negotiation)
                {
                    if (!_values.Contains(value) && !_negotiations.Contains(negotiation))
                    {
                        _values.Add(value); _negotiations.Add(negotiation);
                    }
                    else
                    {
                        throw new ArgumentException("Map should consist only from unique value-negotiation pairs");
                    }
                }
                public bool Remove(object element)
                {
                    if (_values.Contains(element))
                    {
                        _values.Remove(element);
                        return true;
                    }
                    else if (_negotiations.Contains(element))
                    {
                        _negotiations.Remove(element);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                public bool RemoveAt(int index)
                {
                    if (index >= 0 && index < Count)
                    {
                        _values.RemoveAt(index);
                        _negotiations.RemoveAt(index);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                private bool TryGetDefaultNegotiation(object value, out object? negotiation)
                {
                    if (value is bool) { negotiation = !(bool)value; return true; }
                    if (value is int) { negotiation = -(int)value; return true; }
                    if (value is float) { negotiation = -(float)value; return true; }
                    if (value is double) { negotiation = -(double)value; return true; }
                    if (value is decimal) { negotiation = -(decimal)value; return true; }
                    if (value is long) { negotiation = -(long)value; return true; }
                    if (value is sbyte) { negotiation = -(sbyte)value; return true; }
                    if (value is short) { negotiation = -(short)value; return true; }
                    if (value is nint) { negotiation = -(nint)value; return true; }
                    negotiation = null; return false;
                }
                public bool TryGetNegotiation(object element, out object? negotiation)
                {
                    if (_values.Contains(element))
                    {
                        negotiation = _negotiations[_values.IndexOf(element)];
                        return true;
                    }
                    else if (_negotiations.Contains(element))
                    {
                        negotiation = _values[_negotiations.IndexOf(element)];
                        return true;
                    }
                    else
                    {
                        return TryGetDefaultNegotiation(element, out negotiation);
                    }
                }
            }
            public struct ParametersValuePair
            {
                public object[] Parameters { get; private set; }
                public object Value { get; private set; }
                public ParametersValuePair(object value, params object[] parameters)
                {
                    Parameters = parameters;
                    Value = value;
                }

                public override string ToString() => $"({string.Join(", ", Parameters)}, {Value})";
            }
            public struct ParityProperties
            {
                public bool IsEven { get; private set; }
                public bool IsOdd { get; private set; }
                public int DomainIndex { get; private set; }
                public ParityProperties(bool isEven, bool isOdd, int domainIndex)
                {
                    IsEven = isEven;
                    IsOdd = isOdd;
                    DomainIndex = domainIndex;
                }
                public override string ToString() => IsEven ? "Even" : (IsOdd ? "Odd" : "Neither");
            }
            public struct MappingProperties
            {
                public bool IsInjective { get; private set; }
                public bool IsSurjective { get; private set; }
                public int DomainIndex { get; private set; }
                public MappingProperties(bool isInjective, bool isSurjective, int domainIndex)
                {
                    IsInjective = isInjective;
                    IsSurjective = isSurjective;
                    DomainIndex = domainIndex;
                }
                public override string ToString()
                {
                    if (IsInjective && IsSurjective)
                    {
                        return "Bijective";
                    }
                    else
                    {
                        return IsInjective ? "Injective" : (IsSurjective ? "Surjective" : "Neither");
                    }
                }
            }
        }

        public class Graph
        {
            private Dictionary<object, List<object>> _graph = new();
            public int VerticesCount { get =>  _graph.Count; }

            public Graph(HashSet<object> vertices, HashSet<ValueTuple<object, object>> edges) 
            {
                foreach (var vertex in vertices)
                {
                    _graph[vertex] = new List<object>();
                }
                foreach (var edge in edges)
                {
                    AddEdge(edge.Item1, edge.Item2);
                }
                foreach (var vertex in _graph.Keys)
                {
                    Console.WriteLine($"{vertex}: {{ {string.Join(", ", _graph[vertex])} }}");
                }
            }

            public void AddEdge(object origin, object destination)
            {
                AddVertex(origin);
                _graph[origin].Add(destination);

                // For the undirected graph - add the reverse edge as well
                // AddVertex(destination);
                // graph[destination].Add(source);
            }
            public void AddVertex(object vertex)
            {
                if (!_graph.ContainsKey(vertex))
                {
                    _graph[vertex] = new List<object>();
                }
            }

            #region Task 1
            // Degree Calculator: Write a program that calculates the degree of each vertex in a graph
            public Dictionary<object, int> GetVerticesDegrees()
            {
                var degrees = new Dictionary<object, int>();
                foreach (var vertex in _graph.Keys)
                {
                    var degree = _graph[vertex].Count; // Out-degree

                    foreach (var otherVertex in _graph.Keys)
                    {
                        if (otherVertex.Equals(vertex))
                        {
                            continue;
                        }
                        else if (_graph[otherVertex].Contains(vertex))
                        {
                            degree++; // In-degree
                        }
                    }
                    degrees[vertex] = degree;
                }
                return degrees;
            }
            #endregion

            #region Task 2
            // Adjacency Matrix Builder: Implement a program to create the adjacency matrix of a graph
            public int[][] GetAdjacencyMatrix()
            {
                int[][] matrix = new int[VerticesCount][];

                for (int i = 0; i < VerticesCount; i++)
                {
                    matrix[i] = Enumerable.Repeat(0, VerticesCount).ToArray();
                }

                var vertices = new List<object>(_graph.Keys);
                for (int i = 0; i < VerticesCount; i++)
                {
                    for (int j = 0; j < VerticesCount; j++)
                    {
                        object vertex1 = vertices[i];
                        object vertex2 = vertices[j];

                        if (_graph[vertex1].Contains(vertex2))
                        {
                            matrix[i][j] += 1;
                            matrix[j][i] += 1;
                        }
                    }
                }
                return matrix;
            }
            #endregion

            #region Task 3
            // Path Finder: Create a program to find a path between two vertices in a graph
            public Path FindPath(object startVertex, object endVertex)
            {
                #region Input Validation
                if (startVertex is null || !_graph.ContainsKey(startVertex))
                {
                    throw new ArgumentException($"Given start vertex doesn't exist in a graph");
                }
                if (endVertex is null || !_graph.ContainsKey(endVertex))
                {
                    throw new ArgumentException($"Given end vertex doesn't exist in a graph");
                }
                #endregion

                if (VerticesCount != 0)
                {
                    var visited = new HashSet<object>();
                    var stack = new Stack<object>();

                    stack.Push(startVertex);

                    while (stack.Count > 0)
                    {
                        object currentNode = stack.Pop();
                        if (!visited.Contains(currentNode))
                        {
                            Console.WriteLine(currentNode);
                            visited.Add(currentNode);
                            foreach (object neighbor in _graph[currentNode])
                            {
                                if (!visited.Contains(neighbor))
                                {
                                    stack.Push(neighbor);
                                }
                            }
                        }
                    }
                }

                // No path found
                return new Path();
            }

            #endregion
            public override string ToString()
            {
                var result = "";
                result += $"Graph Vertices: {{ {string.Join(", ", _graph.Keys)} }}\n";
                var edges = _graph.SelectMany(pair => pair.Value.Select(neighbor => (pair.Key, neighbor)));
                result += $"Graph Edges: {{ {string.Join(", ", edges.Select(edge => $"{edge}"))} }}";
                return result;
            }

            public class Path
            {
                private Queue<object> _verticesQueue;

                public Path()
                {
                    _verticesQueue = new Queue<object>();
                }
                public Path(Queue<object> verticesQueue)
                {
                    _verticesQueue = verticesQueue;
                }

                public void AddNext(object vertex)
                {
                    _verticesQueue.Enqueue(vertex);
                }

                public void RemovePrevious()
                {
                    _verticesQueue.Dequeue();
                }

                public override string ToString()
                {
                    var result = "";
                    if (_verticesQueue.Count > 1)
                    {
                        result = string.Join(" ➔ ", _verticesQueue.ToArray());
                    }
                    else
                    {
                        result = "NaP"; // Not a Path
                    }
                    return result;
                }
            }
        }
    }
    static class Math
    {
        public enum Operation
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }
        static public double Abs(double x) => x > 0 ? x : -x;
        static public int Abs(int x) => x > 0 ? x : -x;
        static public double Min(params double[] numbers)
        {
            if (numbers.Length == 0) return double.NaN;
            var min = numbers[0];
            foreach (var number in numbers)
            {
                if (number < min)
                {
                    min = number;
                }
            }
            return min;
        }
        static public double Max(params double[] numbers)
        {
            if (numbers.Length == 0) return double.NaN;
            var max = numbers[0];
            foreach (var number in numbers)
            {
                if (number > max)
                {
                    max = number;
                }
            }
            return max;
        }
        static public bool IsEven(double x) => x % 2 == 0;
        static public bool IsPrime(int x)
        {
            if (x == 0) return false;
            x = Abs(x);
            if (x == 2 || x == 3 || x == 5) return true;
            if (x % 2 == 0 || x % 3 == 0 || x % 5 == 0) return false;
            return true;
        }
        static public int GCD(int a, int b) => b == 0 ? a : GCD(b, a % b);
        static public int LCM(int a, int b) => a * b / GCD(a, b);
        static public HashSet<object> Union(params HashSet<object>[] sets)
        {
            #region Input Validation
            if (sets.Length < 1)
            {
                throw new NullReferenceException("The parameters array must have at least one element");
            }
            #endregion
            
            var result = new HashSet<object>(sets[0]);
            for (int i = 1; i < sets.Length; i++)
            {
                result.UnionWith(sets[i]);
            }
            return result;
        }
        static public HashSet<object> Intersection(params HashSet<object>[] sets)
        {
            #region Input Validation
            if (sets.Length < 1)
            {
                throw new NullReferenceException("The parameters array must have at least one element");
            }
            #endregion
            var result = new HashSet<object>(sets[0]);
            for (int i = 1; i < sets.Length; i++)
            {
                result.IntersectWith(sets[i]);
            }
            return result;
        }
        static public HashSet<object> Difference(params HashSet<object>[] sets)
        {
            #region Input Validation
            if (sets.Length < 1)
            {
                throw new NullReferenceException("The parameters array must have at least one element");
            }
            #endregion
            var result = new HashSet<object>(sets[0]);
            for (int i = 1; i < sets.Length; i++)
            {
                result.ExceptWith(sets[i]);
            }
            return result;
        }
        static public HashSet<object> Complement(HashSet<object> set, HashSet<object> universalSet)
        {
            var complementarySet = new HashSet<object>(universalSet);
            complementarySet.ExceptWith(set);
            return complementarySet;
        }
        public static HashSet<object> CartesianProduct(HashSet<object> first, HashSet<object> second)
        {
            var cartesianProduct = new HashSet<object>(from x in first
                                                       from y in second
                                                       select Tuple.Create(x, y));
            return cartesianProduct;
        }
        public static HashSet<object> CartesianProduct<T1, T2>(HashSet<object> first, HashSet<object> second, Func<T1, T2, bool> FilterFunction)
        {
            var filteredCartesianProduct = new HashSet<object>(from x in first.OfType<T1>()
                                                               from y in second.OfType<T2>()
                                                               where FilterFunction(x, y)
                                                               select Tuple.Create(x, y));
            return filteredCartesianProduct;
        }
        public static HashSet<object> FindRelations<T1, T2>(HashSet<object> set, Func<T1, T2, bool> relationFunction)
        {
            var result = new HashSet<object>(from x in set.OfType<T1>()
                                             from y in set.OfType<T2>()
                                             where !x.Equals(y) && relationFunction(x, y)
                                             select Tuple.Create(x, y));
            return result;
        }
        public static bool IsRelationValid(List<ValueTuple<object, object>> relation, HashSet<object> first, HashSet<object> second)
        {
            HashSet<object> cartesianProduct = CartesianProduct(first, second);
            return relation.All(pair => cartesianProduct.Contains(pair));
        }
        static public bool IsRelationReflexive(HashSet<object> set, HashSet<ValueTuple<object, object>> relationSet)
        {
            return !set.Any(element => !relationSet.Contains((element, element)));
        }
        static public bool IsRelationSymmertic(HashSet<ValueTuple<object, object>> relationSet)
        {
            return !relationSet.Any(relation => !relationSet.Contains((relation.Item2, relation.Item1)));
        }
        static public bool IsRelationTransitive(HashSet<ValueTuple<object, object>> relationSet)
        {
            foreach (var pair1 in relationSet)
            {
                foreach (var pair2 in relationSet)
                {
                    // pair1 = (1, 2)
                    // pair2 = (2, 1)
                    if (pair1.Item2.Equals(pair2.Item1) && !pair2.Item2.Equals(pair1.Item1))
                    {
                        ValueTuple<object, object> intermediatePair = (pair1.Item1, pair2.Item2);
                        if (!relationSet.Contains(intermediatePair))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        static public bool IsRelationEquivalent(HashSet<object> set, HashSet<ValueTuple<object, object>> relationSet)
        {
            return IsRelationReflexive(set, relationSet) && IsRelationSymmertic(relationSet) && IsRelationTransitive(relationSet);
        }
        static public HashSet<ValueTuple<object, object>> ReverseRelation(HashSet<ValueTuple<object, object>> relationSet)
        {
            var reverse = new HashSet<ValueTuple<object, object>>(from pair in relationSet
                                                                  select ValueTuple.Create(pair.Item2, pair.Item1));
            return reverse;
        }
        static public bool SetsAreEqual(HashSet<object> set, params HashSet<object>[] otherSets)
        {
            var areEqual = true;
            foreach (var otherSet in otherSets)
            {
                areEqual &= set.SetEquals(otherSet);
            }
            return areEqual;
        }

        static public string FindPrimeFactors(int number)
        {
            var factors = new Dictionary<int, int>();
            for (int divisor = 2; divisor <= number; divisor++)
            {
                while (number % divisor == 0)
                {
                    if (factors.ContainsKey(divisor))
                    {
                        factors[divisor]++;
                    }
                    else
                    {
                        factors.Add(divisor, 1);
                    }
                    number /= divisor;
                }
            }
            var result = new List<string>();
            foreach (var factor in factors)
            {
                if (factor.Value == 1)
                {
                    result.Add($"{factor.Key}");
                }
                else
                {
                    result.Add($"{factor.Key}^{factor.Value}");
                }
            }
            return string.Join( " * ", result);
        }

        static public void DirectProof(int number)
        {
            Console.WriteLine("Statement: If a number is even, then it is divisible by 2");

            Console.WriteLine("\nProof Steps:");

            // Step 1: Assume the hypothesis
            Console.WriteLine($"Step 1: Assume that the number {number} is an even number");

            // Step 2: Define what it means for a number to be even
            Console.WriteLine("Step 2: By definition, a number is even if it can be expressed as 2 * k for some integer k");

            // Step 3: Express the number as 2 * k
            int k = number / 2;
            if (k % 1 == 0)
            {
                Console.WriteLine($"Step 3: We can write {number} as {2} * {k}, where {k} is an integer number");

                // Step 4: Show that the number is divisible by 2
                Console.WriteLine("Step 4: Since we have expressed the number as 2 * k, it is divisible by 2");

                // Step 5: Conclude that the statement is valid
                Console.WriteLine($"Step 5: Therefore, since number {number} is divisible by 2, number is even");

                Console.WriteLine($"Step 6: The statement is valid");
            }
            else
            {
                Console.WriteLine($"Step 3: We can't write {number} as 2 * k for some integer k");

                // Step 4: Show that the number is divisible by 2
                Console.WriteLine("Step 4: Since we can't express the number as 2 * k, it's not divisible by 2.");

                // Step 5: Conclude that the statement is valid
                Console.WriteLine($"Step 5: Therefore, since number {number} isn't divisible by 2, number isn't even");

                Console.WriteLine($"Step 6: The statement is invalid");
            }
        }

        static public void EulerTotientFunction(int n)
        {
            Console.WriteLine($"Calculating Euler's Totient Value for {n}");
            int result = n;
            for (int p = 2; p * p <= n; p++)
            {
                if (n % p == 0)
                {
                    Console.WriteLine($"Found prime factor: {p}");

                    while (n % p == 0)
                    {
                        n /= p;
                    }
                    Console.WriteLine($"Removed all {p} factors, n is now {n}");
                    result -= result / p;
                }
            }
            if (n > 1)
            {
                Console.WriteLine($"Found prime factor: {n}");
                result -= result / n;
            }
            Console.WriteLine($"Euler's Totient Value for {n}: {result}");
        }

        static public double LinearFunction(double value, double yIntercept = 0, double slope = 1)
        {
            return value * slope + yIntercept;
        }

        static public double FunctionCombiner(Func<double, double> F, Func<double, double> G, Operation operation, double x)
        {
            return operation switch
            {
                Operation.Addition => F(x) + G(x),
                Operation.Subtraction => F(x) - G(x),
                Operation.Multiplication => F(x) * G(x),
                Operation.Division => F(x) / G(x),
                _ => throw new NotImplementedException()
            };
        }

        static public double FindYIntercept(double x1, double y1, double x2, double y2)
        {
            double slope = (y2 - y1) / (x2 - x1);
            double yIntercept = y1 - slope * x1;
            return yIntercept;
        }
        static public double FindXIntercept(double x1, double y1, double x2, double y2)
        {
            double slope = (y2 - y1) / (x2 - x1);
            double xIntercept = x1 - y1 / slope;
            return xIntercept;
        }
        static public GraphInformation GraphInformationExtractor(IEnumerable<ValueTuple<double, double>> points)
        {
            if (points.Count() < 1)
            {
                return new GraphInformation(Set.Empty, Set.Empty);
            }

            var xValues = new List<double>();
            var yValues = new List<double>();
            foreach (var point in points)
            {
                var x = point.Item1;
                var y = point.Item2;
                xValues.Add(x);
                yValues.Add(y);
            }

            var xIntercepts = Set.Empty;
            var yIntercepts = Set.Empty;
            var grapfInfo = new GraphInformation(xIntercepts, yIntercepts);

            for (var i = 1; i < xValues.Count; i++)
            {
                var currentX = xValues[i];
                var currentY = yValues[i];
                var previousX = xValues[i - 1];
                var previousY = yValues[i - 1];

                var doInterceptY = Max(currentX, previousX) >= 0 && Min(currentX, previousX) <= 0;
                if (doInterceptY)
                {
                    var yIntercept = FindYIntercept(previousX, previousY, currentX, currentY);
                    yIntercepts.AddElement(yIntercept);
                }

                bool doInterceptX = Max(currentY, previousY) >= 0 && Min(currentY, previousY) <= 0;
                if (doInterceptX)
                {
                    var xIntercept = FindXIntercept(previousX, previousY, currentX, currentY);
                    xIntercepts.AddElement(xIntercept);
                }

                if ((i + 1) >= xValues.Count)
                {
                    continue;
                }

                var nextY = yValues[i + 1];
                if (currentY > previousY && currentY > nextY)
                {
                    grapfInfo.UpdateMaxima(currentY);
                }
                else if (currentY < previousY && currentY < nextY)
                {
                    grapfInfo.UpdateMinima(currentY);
                }
            }
            return grapfInfo;
        }
    }

    struct GraphInformation
    {
        public Set XIntercepts { get; private set; }
        public Set YIntercepts { get; private set; }
        public double? Maxima { get; private set; }
        public double? Minima { get; private set; }
        public GraphInformation(Set xIntercepts, Set yIntercepts, double? maxima = null, double? minima = null)
        {
            XIntercepts = xIntercepts;
            YIntercepts = yIntercepts;
            Maxima = maxima;
            Minima = minima;
        }

        public void UpdateMaxima(double maxima)
        {
            if (maxima < Maxima)
            {
                return;
            }
            Maxima = maxima;
        }

        public void UpdateMinima(double minima)
        {
            if (minima > Minima)
            {
                return;
            }
            Minima = minima;
        }

        public override string ToString()
        {
            string str = string.Empty;
            if (XIntercepts != null)
                str += "X-Intercepts: " + XIntercepts.ToString() + '\n';
            if (YIntercepts != null)
                str += "Y-Intercepts: " + YIntercepts.ToString();
            if (Maxima == null)
                str += '\n' + "Maxima y-value: " + "None";
            else
                str += '\n' + "Maxima y-value: " + Maxima.ToString();
            if (Minima == null)
                str += '\n' + "Minima y-value: " + "None";
            else
                str += '\n' + "Minima y-value: " + Minima.ToString();
            return str;
        }
    }
}
