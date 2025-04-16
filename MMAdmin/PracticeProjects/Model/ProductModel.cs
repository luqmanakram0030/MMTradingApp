using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MMAdmin.PracticeProjects.Model
{
    public class ProductModel
    {
        [PrimaryKey,AutoIncrement]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; } = 0;
        [Required]
        [StringLength(5000)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0,1000)]
        public decimal StockQty {  get; set; } = 0;
        [Required]
        [StringLength (500)]
        public string Category {  get; set; } = string.Empty;
        public string Images { get; set; } = string.Empty;
       
    }
}
