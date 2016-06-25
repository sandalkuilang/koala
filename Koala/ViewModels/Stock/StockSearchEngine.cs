using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Stock
{
    public static class StockSearchEngine
    {
        /// <summary>
        /// Filters the source by the specified search text.
        /// </summary>
        /// <param name="source">The songs to search.</param>
        /// <param name="searchText">The search text.</param>
        /// <returns>The filtered sequence of songs.</returns>
        public static MutableObservableCollection<TransactionStock> Filter(MutableObservableCollection<TransactionStock> source, string searchText)
        {
            if (String.IsNullOrWhiteSpace(searchText))
                return source;

            IEnumerable<string> keyWords = searchText.Split(' ');

            return source
                .AsParallel()
                .Where
                (
                    item => keyWords.All
                    (
                        keyword =>
                            item.MaterialName.ContainsIgnoreCase(keyword) ||
                            item.QualityName.ContainsIgnoreCase(keyword) ||
                            item.SupplierName.ContainsIgnoreCase(keyword) ||
                            item.Qty.LessThanOrEqual(keyword) ||
                            item.CreatedDate.ToString("dd-MMM-YYYY").ContainsIgnoreCase(keyword)   
                    )
                )
                .Convert<TransactionStock>();
        }

        private static bool ContainsIgnoreCase(this KeyValueOption value, string other)
        {
            if (value == null)
                return false;

            return value.Description.IndexOf(other, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        //private static bool ContainsIgnoreCase(this MaterialType value, string other)
        //{
        //    if (value == null)
        //        return false;

        //    return value.Description.IndexOf(other, StringComparison.InvariantCultureIgnoreCase) >= 0;
        //}

        private static bool ContainsIgnoreCase(this string value, string other)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            return value.IndexOf(other, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        private static bool LessThanOrEqual(this decimal value, string other)
        {
            if (!IsNumeric(other))
                return false;

            return value <= Convert.ToDecimal(other);
        }

        private static bool LessThanOrEqual(this int value, string other)
        {
            if (!IsNumeric(other))
                return false;

            return value <= Convert.ToInt32(other);
        }
        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
    }
}
