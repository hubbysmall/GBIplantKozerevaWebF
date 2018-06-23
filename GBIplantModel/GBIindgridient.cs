using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantModel
{
    public class GBIindgridient
    {
        public int Id { get; set; }

        [Required]
        public string GBIindgridientName { get; set; }

        [ForeignKey("GBIindgridientId")]
        public virtual List<GBIpieceofArt__ingridient> GBIpieceofArt__ingridients { get; set; }

        [ForeignKey("GBIingridientId")]
        public virtual List<Storage__GBIingridient> Storage__GBIingridients { get; set; }
    }
}
