using System;

namespace MyUtilities.DataSheets
{
    public abstract class DSRecordBase<T> where T : struct, IComparable
    {
        public T recordID;
    }
}
