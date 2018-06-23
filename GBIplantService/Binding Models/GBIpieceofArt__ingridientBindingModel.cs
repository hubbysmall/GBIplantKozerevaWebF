using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.BindingModels
{
    public class GBIpieceofArt__ingridientBindingModel
    {
        public int Id { get; set; }

        public int GBIpieceofArtId { get; set; }

        public int GBIingridientId { get; set; }

        public int Count { get; set; }
    }
}
