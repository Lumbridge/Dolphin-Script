using System.Collections;
using DolphinScript.Core.Interfaces;

namespace DolphinScript.Core.Concrete
{
    public class ListService : IListService
    {
        /// <summary>
        /// swaps position of two elements in a collection
        /// </summary>
        /// <param name="list"></param>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        public void Swap(IList list, int indexA, int indexB)
        {
            (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        }

        /// <summary>
        /// moves a list item to another position in the collection
        /// </summary>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <param name="shiftAmount"></param>
        public void ShiftItem(IList list, int startIndex, int shiftAmount)
        {
            for (var i = startIndex; i < startIndex + shiftAmount; i++)
            {
                Swap(list, i, i + 1);
            }
        }

        /// <summary>
        /// moves a range of elements down a list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <param name="groupSize"></param>
        /// <param name="shiftAmount"></param>
        public void ShiftRange(IList list, int startIndex, int groupSize, int shiftAmount)
        {
            for (var i = startIndex; i < shiftAmount; i++)
            {
                Swap(list, i, i + groupSize);
            }
        }
    }
}