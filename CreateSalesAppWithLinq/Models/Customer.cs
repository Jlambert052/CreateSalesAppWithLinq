using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Models {

    [Index("Code", IsUnique = true, Name = "UIDX_Code")] //Applied outside of the individual property since index can be used in more ways/against properties
    public class Customer {

        [Key] //attach to let the code know this column is the primary key.
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(4)]
        public string Code { get; set; }

        [Column(TypeName = "decimal(11,2)")]
        public decimal Sales { get; set; }

        [StringLength(255)]
        public string? Notes { get; set; }

    }
}
