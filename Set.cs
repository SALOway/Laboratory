namespace Laboratory_1
{
    internal class Set<T>
    {
        private List<T> _elements;
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

        public Set<T> Union(Set<T> other)
        {
            Set<T> union = new Set<T>(_elements);
            foreach (T element in other.Elements)
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
            Set<T> intersection = new Set<T>(new List<T>());
            foreach (T element in other.Elements)
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
            Set<T> difference = new Set<T>(_elements);
            Set<T> intersection = Intersection(other);
            foreach (T element in intersection.Elements)
            {
                difference.RemoveElement(element);
            }
            return difference;
        }
        public Set<T> Complement(Set<T> other)
        {
            Set<T> union = Union(other);
            Set<T> complement = union.Difference(this);
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
    }
}
