using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mexacode.Web.Models
{
    public class EmailModel
    {
        [Required]
        [StringLength(maximumLength: 50)]
        public string Empresa { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Asunto { get; set; }

        [Required]
        [StringLength(maximumLength: 250)]
        public string Mensaje { get; set; }

        public string Para { get; set; }
    }
}


