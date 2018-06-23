using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.BindingModels
{
    public class GBIpieceOfArtBindingModel
    {
        public int Id { get; set; }

        public string GBIpieceOfArtName { get; set; }

        public decimal Price { get; set; }

        public List<GBIpieceofArt__ingridientBindingModel> GBIpieceofArt__ingridients{ get; set; }
    }
}
