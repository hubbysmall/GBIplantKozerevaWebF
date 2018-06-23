using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantModel
{
    public class Zakaz
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }

        public int GBIpieceofArtId { get; set; }

        public int? ExecutorId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public ZakazStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateExecute { get; set; }

        public virtual Buyer Buyer { get; set; }

        public virtual GBIpieceOfArt GBIpieceOfArt { get; set; }

        public virtual Executor Executor { get; set; }
    }
}
