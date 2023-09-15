using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory_1
{
    internal class SetEvaluator
    {
        public enum SetOperation
        {
            Union,
            Intersection,
            Difference,
            Complement,
            UndefinedOperation
        }
        public static SetOperation GetOperation(string operation)
        {
            operation.ToLower();
            switch (operation)
            {
                case "union":
                    return SetOperation.Union;
                case "intersection":
                    return SetOperation.Intersection;
                case "difference":
                    return SetOperation.Difference;
                case "complement":
                    return SetOperation.Complement;
                default:
                    return SetOperation.UndefinedOperation;
            }
        }

        public void Evaluate()
        {

        }
    }
}
