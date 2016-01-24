using Koala.ViewModels.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Order
{
    public static class OrderSearchEngine
    {
        /// <summary>
        /// Filters the source by the specified search text.
        /// </summary>
        /// <param name="source">The songs to search.</param>
        /// <param name="searchText">The search text.</param>
        /// <returns>The filtered sequence of songs.</returns>
        public static MutableObservableCollection<CreateOrderModel> Filter(MutableObservableCollection<CreateOrderModel> source, string searchText)  
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
                            item.PoNumber.ContainsIgnoreCase(keyword) ||
                            item.CustomerName.ContainsIgnoreCase(keyword) ||
                            item.CustomerPhone.ContainsIgnoreCase(keyword) ||
                            item.TotalPayment.LessThanOrEqual(keyword) ||
                            item.CreatedDate.ToString("dd-MMM-YYYY").ContainsIgnoreCase(keyword) ||
                            item.Details.Source.Any(detail => 
                                (
                                    detail.Title.ContainsIgnoreCase(keyword) ||
                                    detail.SelectedMaterial.ContainsIgnoreCase(keyword) ||
                                    detail.SelectedFinishing.ContainsIgnoreCase(keyword) ||
                                    detail.SelectedQuality.ContainsIgnoreCase(keyword) ||
                                    detail.Deadline.ToString() == (keyword)
                                )
                            )
                            
                    )
                )
                .Convert<CreateOrderModel>();
        }

        private static bool ContainsIgnoreCase(this KeyValueOption value, string other)
        {
            if (value == null)
                return false;

            return value.Description.IndexOf(other, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        private static bool ContainsIgnoreCase(this MaterialType value, string other)
        {
            if (value == null)
                return false;

            return value.Description.IndexOf(other, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

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
        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
    }
}
