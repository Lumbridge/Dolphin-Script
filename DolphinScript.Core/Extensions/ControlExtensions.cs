using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace DolphinScript.Core.Extensions
{
    public static class ControlExtensions
    {
        private delegate void SetPropertyThreadSafeDelegate<TResult>(
            Control @this,
            Expression<Func<TResult>> property,
            TResult value);

        public static void ClearColumn(this DataGridView grid, string columnName)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                row.Cells[columnName].Value = string.Empty;
            }
        }

        public static List<int> GetSelectedIndices(this DataGridView grid)
        {
            if (grid.SelectedRows.Count == 0)
            {
                return new List<int>();
            }

            return grid.SelectedRows.Cast<DataGridViewRow>()
                .Where(x => x.Selected)
                .Select(x => x.Index)
                .OrderBy(x => x)
                .ToList();
        }

        public static void SetSelectedIndices(this DataGridView grid, IList<int> indices)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                row.Selected = indices.Contains(row.Index);
            }
        }

        public static void SetSelectedIndex(this DataGridView grid, int index)
        {
            grid.SetSelectedIndices(new List<int>(1){ index });
        }

        public static void SetPropertyThreadSafe<TResult>(
            this Control @this,
            Expression<Func<TResult>> property,
            TResult value)
        {
            var propertyInfo = (property.Body as MemberExpression)?.Member as PropertyInfo;

            if (propertyInfo == null || 
                !@this.GetType().IsSubclassOf(propertyInfo.ReflectedType) || 
                @this.GetType().GetProperty(propertyInfo.Name, propertyInfo.PropertyType) == null)
            {
                throw new ArgumentException("The lambda expression 'property' must reference a valid property on this Control.");
            }

            if (@this.InvokeRequired)
            {
                @this.Invoke(new SetPropertyThreadSafeDelegate<TResult>(SetPropertyThreadSafe), @this, property, value);
            }
            else
            {
                @this.GetType().InvokeMember(propertyInfo.Name, BindingFlags.SetProperty, null, @this, new object[] { value });
            }
        }
    }
}
