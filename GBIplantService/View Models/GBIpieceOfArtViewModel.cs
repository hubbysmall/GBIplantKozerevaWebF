using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.ViewModels
{
    public class GBIpieceOfArtViewModel
    {
        public int Id { get; set; }

        public string GBIpieceOfArtName { get; set; }

        public decimal Price { get; set; }

        public List<GBIpieceofArt__ingridientViewModel> GBIpieceofArt__ingridients { get; set; }
    }
}
