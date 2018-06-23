using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantModel
{
    public class Buyer
    {
        public int Id { get; set; }

        [Required]
        public string BuyerFIO { get; set; }


        [ForeignKey("BuyerId")]
        public virtual List<Zakaz> Zakazes { get; set; }
    }
}
