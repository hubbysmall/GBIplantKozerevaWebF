using GBIplantModel;
using GBIplantService.BindingModels;
using GBIplantService.Interfaces;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.realizationDB
{
    public class BuyerServiceDB: IBuyerService
    {
        private GBIwebDBcontext context;

        public BuyerServiceDB()
        {
            this.context = new GBIwebDBcontext();
        }

        public BuyerServiceDB(GBIwebDBcontext context)
        {
            this.context = context;
        }

        public List<BuyerViewModel> GetList()
        {
            List<BuyerViewModel> result = context.Buyers
                .Select(rec => new BuyerViewModel
                {
                    Id = rec.Id,
                    BuyerFIO = rec.BuyerFIO
                })
                .ToList();
            return result;
        }

        public BuyerViewModel GetBuyer(int id)
        {
            Buyer element = context.Buyers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new BuyerViewModel
                {
                    Id = element.Id,
                    BuyerFIO = element.BuyerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddBuyer(BuyerBindingModel model)
        {
            Buyer element = context.Buyers.FirstOrDefault(rec => rec.BuyerFIO == model.BuyerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Buyers.Add(new Buyer
            {
                BuyerFIO = model.BuyerFIO
            });
            context.SaveChanges();           
        }
        public void UpdBuyer(BuyerBindingModel model)
        {
            Buyer element = context.Buyers.FirstOrDefault(rec =>
                                    rec.BuyerFIO == model.BuyerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Buyers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.BuyerFIO = model.BuyerFIO;
            context.SaveChanges();
        }


        public void DelBuyer(int id)
        {
            Buyer element = context.Buyers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Buyers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
