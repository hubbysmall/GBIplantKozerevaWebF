using GBIplantModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService
{
    class ALLDataListSingleton
    {
        private static ALLDataListSingleton instance;

        public List<Buyer> Buyers { get; set; }

        public List<GBIindgridient> GBIindgridients { get; set; }

        public List<Executor> Executors { get; set; }

        public List<Zakaz> Zakazes { get; set; }

        public List<GBIpieceOfArt> GBIpieceOfArts { get; set; }

        public List<GBIpieceofArt__ingridient> GBIpieceofArt__ingridients { get; set; }

        public List<Storage> Storages { get; set; }

        public List<Storage__GBIingridient> Storage__GBIingridients { get; set; }

        private ALLDataListSingleton()
        {
            Buyers = new List<Buyer>();
            GBIindgridients = new List<GBIindgridient>();
            Executors = new List<Executor>();
            Zakazes = new List<Zakaz>();
            GBIpieceOfArts = new List<GBIpieceOfArt>();
            GBIpieceofArt__ingridients = new List<GBIpieceofArt__ingridient>();
            Storages = new List<Storage>();
            Storage__GBIingridients = new List<Storage__GBIingridient>();
        }

        public static ALLDataListSingleton GetInstance()
        {
            if(instance == null)
            {
                instance = new ALLDataListSingleton();
            }

            return instance;
        }
    }
}
