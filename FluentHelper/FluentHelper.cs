using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentHelper
{

    public class FluentCollectionFilter<CollectionType, ItemType, ParamaterType>
        where CollectionType : ICollection<ItemType>, new()
        where ParamaterType : IComparable
    {
        CollectionType InternalList;
        Func<ItemType, ParamaterType> InternalFunction;
        bool useNot;

        public FluentCollectionFilter(CollectionType list, Func<ItemType, ParamaterType> _Func, bool _Not = false)
        {
            InternalList = list;
            InternalFunction = _Func;
            useNot = _Not;
        }

        public FluentCollectionFilter<CollectionType, ItemType, ParamaterType> Not()
        {
            return new FluentCollectionFilter<CollectionType, ItemType, ParamaterType>(InternalList, s => InternalFunction(s), true);
        }

        public CollectionType Filter(Func<ItemType, bool> xFunc)
        {
            //CollectionType functionReturnValue;
            CollectionType ReturnList = new CollectionType();
            foreach (ItemType item in InternalList)
            {
                //item = item_loopVariable;
                if (useNot)
                {
                    if (!xFunc(item))
                        ReturnList.Add(item);
                }
                else
                {
                    if (xFunc(item))
                        ReturnList.Add(item);
                }
            }
            return ReturnList;
        }


        public CollectionType Greater(ParamaterType value)
        {
            return Filter(item => InternalFunction(item).CompareTo(value) > 0);
        }

        public CollectionType Lesser(ParamaterType value)
        {
            return Filter(item => InternalFunction(item).CompareTo(value) < 0);
        }

        public CollectionType LesserOrEqual(ParamaterType value)
        {
            return Filter(item => InternalFunction(item).CompareTo(value) <= 0);
        }

        public CollectionType GreaterOrEqual(ParamaterType value)
        {
            return Filter(item => InternalFunction(item).CompareTo(value) >= 0);
        }

        public CollectionType Between(ParamaterType Min, ParamaterType Max)
        {
            return Filter(item => InternalFunction(item).CompareTo(Min) >= 0 & InternalFunction(item).CompareTo(Max) <= 0);
        }

        public new CollectionType Equals(ParamaterType value)
        {
            return Filter(item => InternalFunction(item).Equals(value));
        }

    }

    public delegate void Setter<ItemType, ParamaterType>(ItemType item, ParamaterType value);

    public class FluentCollectionSet<CollectionType, ItemType, ParamaterType>
        where CollectionType : ICollection<ItemType>, new()
        where ParamaterType : IComparable
    {
        public CollectionType InternalList;
        public Func<ItemType, ParamaterType> InternalFunction;
        public Setter<ItemType, ParamaterType> InternalSetter;


        public FluentCollectionSet(CollectionType list, Func<ItemType, ParamaterType> _Func, Setter<ItemType, ParamaterType> _Setter)
        {
            InternalList = list;
            InternalFunction = _Func;
            InternalSetter = _Setter;
        }

        public CollectionType Set(ParamaterType Value)
        {
            foreach (ItemType item in InternalList)
            {
                InternalSetter(item, Value);
            }
            return InternalList;
        }
    }


    public class FluentCollection<CollectionType, ItemType, ParamaterType>
        where CollectionType : ICollection<ItemType>, new()
        where ParamaterType : IComparable
    {
        public CollectionType InternalList;
        public Func<ItemType, ParamaterType> InternalFunction;
        public bool useNot;
        public Setter<ItemType, ParamaterType> InternalSetter;
        //public delegate Setter

        public FluentCollection(CollectionType list, Func<ItemType, ParamaterType> _Func, Setter<ItemType, ParamaterType> _Setter, bool _Not = false)
        {
            InternalList = list;
            InternalFunction = _Func;
            InternalSetter = _Setter;
            useNot = _Not;
        }

        public CollectionType Filter(Func<ItemType, bool> _Func)
        {
            return new FluentCollectionFilter<CollectionType, ItemType, ParamaterType>(InternalList, InternalFunction, useNot).Filter(_Func);
        }


        public CollectionType Set(ParamaterType value)
        {
            return new FluentCollectionSet<CollectionType, ItemType, ParamaterType>(InternalList, InternalFunction, InternalSetter).Set(value);
        }

        public CollectionType Greater(ParamaterType value)
        {
            return new FluentCollectionFilter<CollectionType, ItemType, ParamaterType>(InternalList, InternalFunction, useNot).Greater(value);
        }

        public CollectionType Lesser(ParamaterType value)
        {
            return new FluentCollectionFilter<CollectionType, ItemType, ParamaterType>(InternalList, InternalFunction, useNot).Lesser(value);
        }

        public CollectionType LesserOrEqual(ParamaterType value)
        {
            return new FluentCollectionFilter<CollectionType, ItemType, ParamaterType>(InternalList, InternalFunction, useNot).LesserOrEqual(value);
        }

        public CollectionType GreaterOrEqual(ParamaterType value)
        {
            return new FluentCollectionFilter<CollectionType, ItemType, ParamaterType>(InternalList, InternalFunction, useNot).GreaterOrEqual(value);
        }

        public CollectionType Between(ParamaterType Min, ParamaterType Max)
        {
            return new FluentCollectionFilter<CollectionType, ItemType, ParamaterType>(InternalList, InternalFunction, useNot).Between(Min, Max);
        }

        public new CollectionType Equals(ParamaterType value)
        {
            return new FluentCollectionFilter<CollectionType, ItemType, ParamaterType>(InternalList, InternalFunction, useNot).Equals(value);
        }
    }

    public class FluentCollectionString<CollectionType, ItemType> : FluentCollection<CollectionType, ItemType, string> where CollectionType : ICollection<ItemType>, new()
    {

        public FluentCollectionString(CollectionType list, Func<ItemType, string> _Func, Setter<ItemType, string> _Setter, bool _Not = false)
            : base(list, _Func, _Setter, _Not)
        {
        }

        public new FluentCollectionString<CollectionType, ItemType> Not()
        {
            return new FluentCollectionString<CollectionType, ItemType>(InternalList, s => InternalFunction(s), InternalSetter, true);
        }

        public CollectionType StartsWith(string value)
        {
            return Filter(item => InternalFunction(item).StartsWith(value));
        }

        public CollectionType EndsWith(string value)
        {
            return Filter(item => InternalFunction(item).EndsWith(value));
        }

        public CollectionType Contains(string value)
        {
            return Filter(item => InternalFunction(item).Contains(value));
        }

        public FluentCollectionFilter<CollectionType, ItemType, int> IndexOf(string value)
        {
            return new FluentCollectionFilter<CollectionType, ItemType, int>(InternalList, s => InternalFunction(s).IndexOf(value));
        }

        public CollectionType Replace(string oldValue, string newValue)
        {
            foreach (ItemType item in InternalList)
            {
                this.InternalSetter(item, InternalFunction(item).Replace(oldValue, newValue));
            }
            return InternalList;
        }
    }
}
