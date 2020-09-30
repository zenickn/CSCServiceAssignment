using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Product
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Must Be maximum 30 Characters")]
        public string Name { get; set; }
        public string Category { get; set; }

        [Required]
        [Range(0.01,100.00, ErrorMessage = "Price must be between 0.01 and 100.00")]
        public decimal Price { get; set; }
    }
}