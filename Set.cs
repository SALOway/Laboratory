namespace Laboratory
{
    class Set
    {
        private List<object> _elements = new List<object>() { };
        public int Count => _elements.Count;
        public Set() { }
        public Set(IEnumerable<object> elements)
        {
            AddElements(elements);
        }
        public void AddElement(object element)
        {
            if (!Contains(element))
            {
                _elements.Add(element);
            }
        }
        public void AddElements(IEnumerable<object> elements)
        {
            foreach (object element in elements)
            {
                AddElement(element);
            }
        }
        public void ReplaceElements(IEnumerable<object> elements)
        {
            _elements.Clear();
            AddElements(elements);
        }
        public void RemoveElement(object element) => _elements.Remove(element);
        public void Clear() => _elements.Clear();
        public bool Contains(object element) => _elements.Contains(element);
        public static bool ElementsAreEqual(Set first, Set second) => first.Count == second.Count && first._elements.SequenceEqual(second._elements);
        public void Union(Set other) => AddElements(other._elements);
        public static Set Union(Set first, Set second)
        {
            var union = new Set(first._elements);
            union.Union(second);
            return union;
        }
        public void Intersection(Set other)
        {
            Clear();
            foreach (object element in other._elements)
            {
                if (Contains(element))
                {
                    AddElement(element);
                }
            }
        }
        public static Set Intersection(Set first, Set second)
        {
            var intersection = new Set();
            foreach (object element in second._elements)
            {
                if (first.Contains(element))
                {
                    intersection.AddElement(element);
                }
            }
            return intersection;
        }
        public void Difference(Set other)
        {
            var intersection = Intersection(this, other);
            foreach (object element in intersection._elements)
            {
                RemoveElement(element);
            }
        }
        public static Set Difference(Set first, Set second)
        {
            var difference = new Set(first._elements);
            var intersection = Intersection(first, second);
            foreach (object element in intersection._elements)
            {
                difference.RemoveElement(element);
            }
            return difference;
        }
        public void Complement(Set universalSet) => _elements = Difference(universalSet, this)._elements;
        public static Set Complement(Set set, Set universalSet) => Difference(universalSet, set);
        public static Set CartesianProduct(Set first, Set second)
        {
            var cartesianProduct = new Set();
            foreach (var firstElement in first._elements)
            {
                foreach (var secondElement in second._elements)
                {
                    cartesianProduct.AddElement((firstElement, secondElement));
                }
            }
            return cartesianProduct;
        }
        public static Set CartesianProduct<T1, T2>(Set first, Set second, Func<T1,T2,bool> FilterFunction)
        {
            var cartesianProduct = new Set();
            foreach (var firstElement in first._elements)
            {
                if (firstElement is not T1) continue;
                foreach (var secondElement in second._elements)
                {
                    if (secondElement is not T2) continue;
                    if (FilterFunction((T1)firstElement, (T2)secondElement))
                    {
                        cartesianProduct.AddElement((firstElement, secondElement));
                    }
                }
            }
            return cartesianProduct;
        }
        public void CartesianProduct(Set other)
        {
            Clear();
            foreach (var firstElement in _elements)
            {
                foreach (var secondElement in other._elements)
                {
                    AddElement((firstElement, secondElement));
                }
            }
        public void CartesianProduct<T1, T2>(Set other, Func<T1, T2, bool> FilterFunction)
        {
            Clear();
            foreach (var firstElement in _elements)
            {
                if (firstElement is not T1) continue;
                foreach (var secondElement in other._elements)
                {
                    if (secondElement is not T2) continue;
                    if (FilterFunction((T1)firstElement, (T2)secondElement))
                    {
                        AddElement((firstElement, secondElement));
                    }
                }
            }
        public static bool IsRelationValid(Set relation, Set first, Set second) // relation is a set because list of ordered pairs can contains dublicates and it is easier to simply transform it to the Set
        {
            var cartesian = CartesianProduct(first, second);
            return ElementsAreEqual(relation, cartesian);
        }
        public static Set FindRelations<T1, T2>(Set set, Func<T1, T2, bool> relationFunction)
        {
            var relation = new Set();
            foreach (var firstElement in set._elements)
            {
                if (firstElement is not T1) continue;
                foreach (var secondElement in set._elements)
                {
                    if (secondElement is not T2 || firstElement.Equals(secondElement)) continue;
                    if (relationFunction((T1)firstElement, (T2)secondElement))
                    {
                        relation.AddElement((firstElement, secondElement));
                    }
                }
            }
            return relation;
        }


        public override string ToString() => "{ " + string.Join(", ", _elements) + " }";
    }
    }
}
