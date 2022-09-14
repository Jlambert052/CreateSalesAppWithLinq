using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Models {

    public class OrderLine {

        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual OrderLine Order { get; set; }


        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


        public int Quantity { get; set; } = 1;

    }
}
