namespace Laboratory
{
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
        static public float Abs(float x) => x > 0 ? x : -x;
        static public int Abs(int x) => x > 0 ? x : -x;

        static public bool IsEven(double x) => x % 2 == 0;
        static public bool IsEven(float x) => x % 2 == 0;
        static public bool IsEven(int x) => x % 2 == 0;

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
            double previousY = yValues[0];
            if (xValues[0] == 0)
            {
                yIntercepts.AddElement(yValues[0]);
            }
            if (yValues[0] == 0)
            {
                xIntercepts.AddElement(xValues[0]);
            }

            for (var i = 1; i < xValues.Count; i++)
            {
                if (xValues[i] == 0)
                {
                    yIntercepts.AddElement(yValues[i]);
                }
                if (yValues[i] == 0)
                {
                    xIntercepts.AddElement(xValues[i]);
                }

                if ((i + 1) >= xValues.Count)
                {
                    continue;
                }

                if (yValues[i] > previousY && yValues[i] > yValues[i + 1])
                {
                    grapfInfo.UpdateMaxima(yValues[i]);
                }
                if (yValues[i] < previousY && yValues[i] < yValues[i + 1])
                {
                    grapfInfo.UpdateMinima(yValues[i]);
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
                str += '\n' + "Maxima: " + "None";
            else
                str += '\n' + "Maxima: " + Maxima.ToString();
            if (Minima == null)
                str += '\n' + "Minima: " + "None";
            else
                str += '\n' + "Minima: " + Minima.ToString();
            return str;
        }
    }
}
