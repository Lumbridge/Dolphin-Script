using System.Collections;

namespace DolphinScript.Core.Interfaces
{
    public interface IListService
    {
        void Swap(IList list, int indexA, int indexB);
        void ShiftItem(IList list, int startIndex, int shiftAmount);
        void ShiftRange(IList list, int startIndex, int groupSize, int shiftAmount);
    }
}