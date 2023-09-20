namespace Laboratory_1
{
    class Set<T>
    {
        private List<T> _elements = new List<T>();
        public IReadOnlyList<T> Elements => _elements;
        public Set(IEnumerable<T> elements)
        {
            _elements = new List<T>() { };
            SetElements(elements);
        }

        public void AddElement(T newElement)
        {
            if (!Contains(newElement))
            {
                _elements.Add(newElement);
            }
        }
        public void SetElements(IEnumerable<T> elements)
        {
            _elements.Clear();
            foreach (T element in elements)
            {
                AddElement(element);
            }
        }
        public void RemoveElement(T element) => _elements.Remove(element);
        public void Clear() => _elements.Clear();
        public bool Contains(T element) => _elements.Contains(element);

        public static bool AreEqual(Set<T> first, Set<T> second) => first.Equals(second);

        public Set<T> Union(Set<T> other)
        {
            var union = new Set<T>(_elements);
            foreach (T element in other._elements)
            {
                if (!union.Contains(element))
                {
                    union.AddElement(element);
                }
            }
            return union;
        }
        public Set<T> Intersection(Set<T> other)
        {
            var intersection = new Set<T>(new List<T>());
            foreach (T element in other._elements)
            {
                if (Contains(element))
                {
                    intersection.AddElement(element);
                }
            }
            return intersection;
        }
        public Set<T> Difference(Set<T> other)
        {
            var difference = new Set<T>(_elements);
            var intersection = Intersection(other);
            foreach (T element in intersection._elements)
            {
                difference.RemoveElement(element);
            }
            return difference;
        }
        public Set<T> Complement(Set<T> other)
        {
            var union = Union(other);
            var complement = union.Difference(this);
            return complement;
        }

        public override string ToString()
        {
            string result = "[";
            for (int i = 0; i < _elements.Count; i++)
            {
                result += _elements[i];
                if (i < _elements.Count - 1)
                    result += ",";
            }
            result += "]";
            return result;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherSet = (Set<T>)obj;

            if (_elements.Count != otherSet._elements.Count)
            {
                return false;
            }

            for (int i = 0; i < _elements.Count; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(_elements[i], otherSet._elements[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
