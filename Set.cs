using System.Collections;

namespace Laboratory
{
    /// <summary>
    /// Represents a collection of unique objects and provides methods to perform basic set operations such as union, intersection, and difference
    /// </summary>
    class Set
    {
        private List<object> _elements = new List<object>() { };
        public int Count => _elements.Count;
        public Set() { }
        public Set(IEnumerable elements)
        {
            AddElements(elements);
        }
        /// <summary>
        /// Adds an element to the set if it is not already present and sort set
        /// </summary>
        /// <param name="element">The new element to add to the set</param>
        public void AddElement(object element)
        {
            if (!Contains(element))
            {
                _elements.Add(element);
            }
        }
        /// <summary>
        /// Adds elements to the set. Existing elements in the set will not be duplicated
        /// </summary>
        /// <param name="elements">The new elements to add to the set</param>
        public void AddElements(IEnumerable elements)
        {
            foreach (object element in elements)
            {
                AddElement(element);
            }
        }
        /// <summary>
        /// Replaces all elements in the set with the specified new elements
        /// </summary>
        /// <param name="elements">The new elements to replace the existing ones in the set</param>
        public void ReplaceElements(IEnumerable elements)
        {
            _elements.Clear();
            AddElements(elements);
        }
        /// <summary>
        /// Removes the specified element from the set if it exists in the set
        /// </summary>
        /// <param name="element">The element to remove</param>
        public void RemoveElement(object element) => _elements.Remove(element);
        /// <summary>
        /// Removes all elements from the set
        /// </summary>
        public void Clear() => _elements.Clear();
        /// <summary>
        /// Checks if the specified element is present in the set
        /// </summary>
        /// <param name="element">The element to check for</param>
        /// <returns>True if the element is found in the set; otherwise, false</returns>
        public bool Contains(object element) => _elements.Contains(element);
        /// <summary>
        /// Compares two sets for equality by checking if they have the same number of elements and all of their elements are the same
        /// </summary>
        /// <param name="first">The first set to compare</param>
        /// <param name="second">The second set to compare</param>
        /// <returns>True if the sets have the same number of elements and all elements are equal; otherwise, false</returns>
        public static bool ElementsAreEqual(Set first, Set second) => first.Count == second.Count && first._elements.SequenceEqual(second._elements);
        /// <summary>
        /// Adds elements from another set to this set. Existing elements in this set will not be duplicated
        /// </summary>
        /// <param name="other">The set whose elements will be added to this set</param>
        public void Union(Set other) => AddElements(other._elements);
        /// <summary>
        /// Combines the elements of the first set with the elements of the second set to create a new set. 
        /// Existing elements in the second set will not be duplicated.
        /// </summary>
        /// <param name="first">The first set to combine</param>
        /// <param name="second">The second set to combine</param>
        /// <returns>A new set containing the combined elements of the two input sets</returns>
        public static Set Union(Set first, Set second)
        {
            var union = new Set(first._elements);
            union.Union(second);
            return union;
        }
        /// <summary>
        /// Modifies this set to contain only the elements that are common with another set
        /// </summary>
        /// <param name="other">The set to intersect with</param>
        public void Intersection(Set other)
        {
            var intersection = new List<object>();
            foreach (object element in other._elements)
            {
                if (Contains(element))
                {
                    intersection.Add(element);
                }
            }
            ReplaceElements(intersection);
        }
        /// <summary>
        /// Computes the intersection of two sets and returns a new set containing the common elements
        /// </summary>
        /// <param name="first">The first set for the intersection</param>
        /// <param name="second">The second set for the intersection</param>
        /// <returns>A new set containing the elements that are common to both input sets</returns>
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
        /// <summary>
        /// Modifies this set to contain only the elements that are not present in another set
        /// </summary>
        /// <param name="other">The set whose elements will be excluded from this set</param>
        public void Difference(Set other)
        {
            var intersection = Intersection(this, other);
            foreach (object element in intersection._elements)
            {
                RemoveElement(element);
            }
        }
        /// <summary>
        /// Computes the set difference between two sets and returns a new set containing the elements that are in the first set but not in the second set
        /// </summary>
        /// <param name="first">The first set for the difference operation</param>
        /// <param name="second">The second set for the difference operation</param>
        /// <returns>A new set containing the elements that are in the first set but not in the second set</returns>
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
        /// <summary>
        /// Modifies this set to contain its complement with respect to a given universal set
        /// </summary>
        /// <param name="universalSet">The universal set to compute the complement with</param>
        public void Complement(Set universalSet) => _elements = Difference(universalSet, this)._elements;
        /// <summary>
        /// Computes the complement of a set with respect to a given universal set and returns a new set
        /// </summary>
        /// <param name="set">The set to compute the complement for</param>
        /// <param name="universalSet">The universal set to use as a reference</param>
        /// <returns>A new set containing the complement of the input set with respect to the universal set</returns>
        public static Set Complement(Set set, Set universalSet) => Difference(universalSet, set);
        /// <summary>
        /// Computes the Cartesian product of two sets, resulting in a new set of pairs containing all possible combinations of elements from the input sets
        /// </summary>
        /// <param name="first">The first set</param>
        /// <param name="second">The second set</param>
        /// <returns>A new set representing the Cartesian product of the input sets</returns>
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
        /// <summary>
        /// Computes the Cartesian product of two sets with generic element types and applies a filter function to include only pairs that satisfy the specified criteria
        /// </summary>
        /// <typeparam name="T1">The expected element type of the first set</typeparam>
        /// <typeparam name="T2">The expected element type of the second set</typeparam>
        /// <param name="first">The first set</param>
        /// <param name="second">The second set</param>
        /// <param name="FilterFunction">A function that defines the filtering criteria for pairs of elements from the two sets</param>
        /// <returns>A new set representing the filtered Cartesian product of the input sets</returns>
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
        /// <summary>
        /// Computes the Cartesian product of this set with another set and replaces the current contents with the result
        /// </summary>
        /// <param name="other">The other set to compute the Cartesian product with</param>
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
        }
        /// <summary>
        /// Computes the Cartesian product of this set with another set, applying a filter function to include only pairs that satisfy the specified criteria, and replaces the current contents with the result
        /// </summary>
        /// <typeparam name="T1">The expected element type of the first set</typeparam>
        /// <typeparam name="T2">The expected element type of the second set</typeparam>
        /// <param name="other">The other set to compute the Cartesian product with</param>
        /// <param name="FilterFunction">A function that defines the filtering criteria for pairs of elements from the two sets</param>
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
        }
        /// <summary>
        /// Checks if a given relation is valid by comparing it to the Cartesian product of two sets
        /// </summary>
        /// <param name="relation">The relation to be checked for validity</param>
        /// <param name="first">The first set involved in the Cartesian product</param>
        /// <param name="second">The second set involved in the Cartesian product</param>
        /// <returns>True if the relation is valid; otherwise, false</returns>
        public static bool IsRelationValid(Set relation, Set first, Set second) // relation is a set because list of ordered pairs can contains dublicates and it is easier to simply transform it to the Set
        {
            var cartesian = CartesianProduct(first, second);
            return ElementsAreEqual(relation, cartesian);
        }
        /// <summary>
        /// Finds relations within a set of elements by applying a relation function
        /// </summary>
        /// <typeparam name="T1">The expected element type of the first set</typeparam>
        /// <typeparam name="T2">The expected element type of the second set</typeparam>
        /// <param name="set">The set of elements to search for relations in</param>
        /// <param name="relationFunction">A function that defines the relation between elements</param>
        /// <returns>A new set containing pairs of elements that satisfy the relation function</returns>
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
