using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.ViewModels
{
    public class ZakazViewModel
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }

        public string BuyerFIO { get; set; }

        public int GBIpieceOfArtId { get; set; }

        public string GBIpieceOfArtName { get; set; }

        public int? ExecutorId { get; set; }

        public string ExecutorName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateExecute { get; set; }
    }
}
