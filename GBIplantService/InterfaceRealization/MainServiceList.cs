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
    public class MainServiceList : IMainService
    {
        private ALLDataListSingleton source;

        public MainServiceList()
        {
            source = ALLDataListSingleton.GetInstance();
        }

        public List<ZakazViewModel> GetList()
        {
            List<ZakazViewModel> result = new List<ZakazViewModel>();
            for (int i = 0; i < source.Zakazes.Count; ++i)
            {
                string clientFIO = string.Empty;
                for (int j = 0; j < source.Buyers.Count; ++j)
                {
                    if(source.Buyers[j].Id == source.Zakazes[i].BuyerId)
                    {
                        clientFIO = source.Buyers[j].BuyerFIO;
                        break;
                    }
                }
                string productName = string.Empty;
                for (int j = 0; j < source.GBIpieceOfArts.Count; ++j)
                {
                    if (source.GBIpieceOfArts[j].Id == source.Zakazes[i].GBIpieceofArtId)
                    {
                        productName = source.GBIpieceOfArts[j].GBIpieceOfArtNAme;
                        break;
                    }
                }
                string implementerFIO = string.Empty;
                if(source.Zakazes[i].ExecutorId.HasValue)
                {
                    for (int j = 0; j < source.Executors.Count; ++j)
                    {
                        if (source.Executors[j].Id == source.Zakazes[i].ExecutorId.Value)
                        {
                            implementerFIO = source.Executors[j].ExecutorFIO;
                            break;
                        }
                    }
                }
                result.Add(new ZakazViewModel
                {
                    Id = source.Zakazes[i].Id,
                    BuyerId = source.Zakazes[i].BuyerId,
                    BuyerFIO = clientFIO,
                    GBIpieceOfArtId = source.Zakazes[i].GBIpieceofArtId,
                    GBIpieceOfArtName = productName,   /////??????
                    ExecutorId = source.Zakazes[i].ExecutorId,
                    ExecutorName = implementerFIO,
                    Count = source.Zakazes[i].Count,
                    Sum = source.Zakazes[i].Sum,
                    DateCreate = source.Zakazes[i].DateCreate.ToLongDateString(),
                   // DateExecute = source.Zakazes[i].DateExecute?.ToLongDateString(),
                    DateExecute = source.Zakazes[i].DateCreate.ToLongDateString(),
                    Status = source.Zakazes[i].Status.ToString()
                });
            }
            return result;
        }

        public void CreateZakaz(ZakazBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Zakazes.Count; ++i)
            {
                if (source.Zakazes[i].Id > maxId)
                {
                    maxId = source.Buyers[i].Id;
                }
            }
            source.Zakazes.Add(new Zakaz
            {
                Id = maxId + 1,
                BuyerId = model.BuyerId,
                GBIpieceofArtId = model.GBIpieceOfArtId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = ZakazStatus.taken
            });
        }

        public void TakeZakazInWork(ZakazBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Zakazes.Count; ++i)
            {
                if (source.Zakazes[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            for (int i = 0; i < source.GBIpieceofArt__ingridients.Count; ++i)
            {
                if (source.GBIpieceofArt__ingridients[i].GBIpieceOfArtId == source.Zakazes[index].GBIpieceofArtId)
                {
                    int countOnStocks = 0;
                    for (int j = 0; j < source.Storage__GBIingridients.Count; ++j)
                    {
                        if (source.Storage__GBIingridients[j].GBIingridientId == source.GBIpieceofArt__ingridients[i].GBIindgridientId)
                        {
                            countOnStocks += source.Storage__GBIingridients[j].Count;
                        }
                    }
                    if (countOnStocks < source.GBIpieceofArt__ingridients[i].Count * source.Zakazes[index].Count)
                    {
                        for (int j = 0; j < source.GBIindgridients.Count; ++j)
                        {
                            if (source.GBIindgridients[j].Id == source.GBIpieceofArt__ingridients[i].GBIindgridientId)
                            {
                                throw new Exception("Не достаточно компонента " + source.GBIindgridients[j].GBIindgridientName +
                                    " требуется " + source.GBIpieceofArt__ingridients[i].Count + ", в наличии " + countOnStocks);
                            }
                        }
                    }
                }
            }
            // списываем
            for (int i = 0; i < source.GBIpieceofArt__ingridients.Count; ++i)
            {
                if (source.GBIpieceofArt__ingridients[i].GBIpieceOfArtId == source.Zakazes[index].GBIpieceofArtId)
                {
                    int countOnStocks = source.GBIpieceofArt__ingridients[i].Count * source.Zakazes[index].Count;
                    for (int j = 0; j < source.Storage__GBIingridients.Count; ++j)
                    {
                        if (source.Storage__GBIingridients[j].GBIingridientId == source.GBIpieceofArt__ingridients[i].GBIindgridientId)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (source.Storage__GBIingridients[j].Count >= countOnStocks)
                            {
                                source.Storage__GBIingridients[j].Count -= countOnStocks;
                                break;
                            }
                            else
                            {
                                countOnStocks -= source.Storage__GBIingridients[j].Count;
                                source.Storage__GBIingridients[j].Count = 0;
                            }
                        }
                    }
                }
            }
            source.Zakazes[index].ExecutorId = model.ExecutorId;
            source.Zakazes[index].DateExecute = DateTime.Now;
            source.Zakazes[index].Status = ZakazStatus.inProcess;
        }

        public void FinishZakaz(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Zakazes.Count; ++i)
            {
                if (source.Buyers[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Zakazes[index].Status = ZakazStatus.ready;
        }

        public void PayZakaz(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Zakazes.Count; ++i)
            {
                if (source.Buyers[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Zakazes[index].Status = ZakazStatus.paid;
        }

        public void PutGBIingridientInStorage(Storage__GBIingridientBindingModel model)
        {
            //List<GBIindgridient> result = source.GBIindgridients;
            int maxId = 0;
            for (int i = 0; i < source.Storage__GBIingridients.Count; ++i)
            {
                if (source.Storage__GBIingridients[i].StorageId == model.StorageId &&
                    source.Storage__GBIingridients[i].GBIingridientId == model.GBIingridientId)
                {
                    source.Storage__GBIingridients[i].Count += model.Count;
                    return;
                }
                if (source.Storage__GBIingridients[i].Id > maxId)
                {
                    maxId = source.Storage__GBIingridients[i].Id;
                }
            }
            source.Storage__GBIingridients.Add(new Storage__GBIingridient
            {
                Id = ++maxId,
                StorageId = model.StorageId,
                GBIingridientId = model.GBIingridientId,
                Count = model.Count
            });
        }
    }
}
