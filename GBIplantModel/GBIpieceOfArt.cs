using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantModel
{
    public class GBIpieceOfArt
    {
        public int Id { get; set; }

        [Required]
        public string GBIpieceOfArtNAme { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("GBIpieceofArtId")]
        public virtual List<Zakaz> Zakazes { get; set; }

        [ForeignKey("GBIpieceOfArtId")]
        public virtual List<GBIpieceofArt__ingridient> GBIpieceofArt__ingridients { get; set; }
    }
}
