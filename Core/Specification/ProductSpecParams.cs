using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public string? Search { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        //public static bool TryParse(string input, out ProductSpecParams productSpecParams)
        //{
        //    // Implement parsing logic here
        //    // Example: Parse input string and initialize productSpecParams
        //    // Return true if parsing succeeds, false otherwise

        //    productSpecParams = null; // Initialize to null if parsing fails
        //    return false; // Return false if parsing fails
        //}

    }
}
