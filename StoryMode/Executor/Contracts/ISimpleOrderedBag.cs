namespace Executor.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ISimpleOrderedBag<T> : IEnumerable<T> where T : IComparable<T>
    {
        int Size { get; }

        int Capacity { get; }

        void Add(T element);

        bool Remove(T element);

        void AddAll(ICollection<T> elements);

        string JoinWith(string joiner);
    }
}
