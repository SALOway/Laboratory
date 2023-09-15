namespace Laboratory_1
{
    internal struct Set<T>
    {
        private List<T> _elements;
        public List<T> Elements => new List<T>(_elements);
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
        public void SetElements(IEnumerable<T> elements) => _elements = elements.Distinct().ToList();
        public void RemoveElement(T element) => _elements.Remove(element);
        public void Clear() => _elements.Clear();
        public bool Contains(T element) => _elements.Contains(element);

        public Set<T> Union(Set<T> other) => new Set<T>(_elements.Union(other.Elements));
        public Set<T> Intersection(Set<T> other) => new Set<T>(_elements.Intersect(other.Elements));
        public Set<T> Difference(Set<T> other) => new Set<T>(_elements.Except(other.Elements));
        public Set<T> Complement(Set<T> other) => Union(other).Difference(this);

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
    }
}
