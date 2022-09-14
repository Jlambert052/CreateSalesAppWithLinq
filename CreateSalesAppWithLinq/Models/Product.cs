﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Models {

    public class Product {

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }

        public override string ToString() {
            return ($" {Id} | {Name} | {Price} ");
        }
    }
}
