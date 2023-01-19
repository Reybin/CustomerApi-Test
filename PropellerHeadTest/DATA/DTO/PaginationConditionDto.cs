using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.DTO
{
    /// <summary>
    /// Dto class that is used for pagination filters.
    /// </summary>
    public class PaginationConditionDto
    {
        /// <summary>
        /// Name/ Path of Variable in Object to filter.
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// Value to find.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Contains, Equals, OrField, Between
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        ///It is used when the OrField method is used to look up a value in the Field or in Field2.
        /// </summary>
        public string? Field2 { get; set; }
        /// <summary>
        /// It is used when using the Between method: Field between Value to Value2.
        /// </summary>
        public string? Value2 { get; set; }

    }
}
