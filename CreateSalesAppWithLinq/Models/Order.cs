using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Models {

    public class Order {

        [Key]
        public int Id { get; set; } //PK of table

        [StringLength(100)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; }

        [StringLength(15)]
        public string Status { get; set; } = "New";

        public DateTime Date { get; set; } = DateTime.Now;


        public int CustomerId { get; set; } //intended as FK to customer

        public virtual Customer Customer { get; set; } //adding this virtual property points the system to CustomerId being a FK

    }
}
