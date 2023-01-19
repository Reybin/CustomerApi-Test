using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.DTO
{
    public class PaginationDto<T>
    {
        /// <summary>
        /// Pagina Actual.
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Cantidad de registros por pagina.
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Total de paginas segun los filtros aplicados.
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Total de Registos segun los filtros aplicados.
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// Campo de la entidad por la cual se esta ordenando.
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// Especifica si el orden es descendente o Ascendente.
        /// </summary>
        public bool OrderByDesc { get; set; }
        /// <summary>
        /// Listado de la entidad.
        /// </summary>
        public List<T> Result { get; set; }
    }
}
