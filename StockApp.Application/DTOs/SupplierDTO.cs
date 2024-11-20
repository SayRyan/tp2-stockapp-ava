using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class SupplierDTO
    {
        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(4)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The ContactEmail is Required")]
        [MinLength(16)]
        [MaxLength(100)]
        public string ContactEmail { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "The PhoneNumber is Required")]
        [MaxLength(11)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
