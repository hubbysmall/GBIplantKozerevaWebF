using GBIplantModel;
using GBIplantService.BindingModels;
using GBIplantService.Interfaces;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.realizationOfInterfaces
{
    public class BuyerServiceList : IBuyerService
    {
        private ALLDataListSingleton source;

        public BuyerServiceList()
        {
            source = ALLDataListSingleton.GetInstance();
        }

        public List<BuyerViewModel> GetList()
        {
            List<BuyerViewModel> result = new List<BuyerViewModel>();
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                result.Add(new BuyerViewModel
                {
                    Id = source.Buyers[i].Id,
                    BuyerFIO = source.Buyers[i].BuyerFIO
                });
            }
            return result;
        }

        public BuyerViewModel GetBuyer(int id)
        {
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].Id == id)
                {
                    return new BuyerViewModel
                    {
                        Id = source.Buyers[i].Id,
                        BuyerFIO = source.Buyers[i].BuyerFIO
                    };
                }
            }
            throw new Exception("Покупатель не найден");
        }

        public void AddBuyer(BuyerBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].Id > maxId)
                {
                    maxId = source.Buyers[i].Id;
                }
                if (source.Buyers[i].BuyerFIO == model.BuyerFIO)
                {
                    throw new Exception("Уже есть покупатель с таким ФИО");
                }
            }
            source.Buyers.Add(new Buyer
            {
                Id = maxId + 1,
                BuyerFIO = model.BuyerFIO
            });
        }

        public void UpdBuyer(BuyerBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Buyers[i].BuyerFIO == model.BuyerFIO &&
                    source.Buyers[i].Id != model.Id)
                {
                    throw new Exception("Уже есть покупатель с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Покупатель не найден");
            }
            source.Buyers[index].BuyerFIO = model.BuyerFIO;
        }

        public void DelBuyer(int id)
        {
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].Id == id)
                {
                    source.Buyers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Покупатель не найден");
        }
    }
}
