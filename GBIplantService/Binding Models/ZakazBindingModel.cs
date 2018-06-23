using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.BindingModels
{
    public class ZakazBindingModel
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }

        public int GBIpieceOfArtId { get; set; }

        public int? ExecutorId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
