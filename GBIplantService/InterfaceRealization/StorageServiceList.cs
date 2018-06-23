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
    public class StorageServiceList : IStorageService
    {
        private ALLDataListSingleton source;

        public StorageServiceList()
        {
            source = ALLDataListSingleton.GetInstance();
        }

        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = new List<StorageViewModel>();
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<Storage__GBIingridientViewModel> StockComponents = new List<Storage__GBIingridientViewModel>();
                for (int j = 0; j < source.Storage__GBIingridients.Count; ++j)
                {
                    if (source.Storage__GBIingridients[j].StorageId == source.Storages[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.GBIindgridients.Count; ++k)
                        {
                            if (source.Storage__GBIingridients[j].GBIingridientId == source.GBIindgridients[k].Id)
                            {
                                componentName = source.GBIindgridients[k].GBIindgridientName;
                                break;
                            }
                        }
                        StockComponents.Add(new Storage__GBIingridientViewModel
                        {
                            Id = source.Storage__GBIingridients[j].Id,
                            StorageId = source.Storage__GBIingridients[j].StorageId,
                            GBIingridientId = source.Storage__GBIingridients[j].GBIingridientId,
                            GBIingridientName = componentName,
                            Count = source.Storage__GBIingridients[j].Count
                        });
                    }
                }
                result.Add(new StorageViewModel
                {
                    Id = source.Storages[i].Id,
                    StorageName = source.Storages[i].StorageName,
                    Storage__GBIingridients = StockComponents
                });
            }
            return result;
        }

        public StorageViewModel GetStorage(int id)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<Storage__GBIingridientViewModel> StockComponents = new List<Storage__GBIingridientViewModel>();
                for (int j = 0; j < source.Storage__GBIingridients.Count; ++j)
                {
                    if (source.Storage__GBIingridients[j].StorageId == source.Storages[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.GBIindgridients.Count; ++k)
                        {
                            if (source.GBIpieceofArt__ingridients[j].GBIindgridientId == source.GBIindgridients[k].Id)
                            {
                                componentName = source.GBIindgridients[k].GBIindgridientName;
                                break;
                            }
                        }
                        StockComponents.Add(new Storage__GBIingridientViewModel
                        {
                            Id = source.Storage__GBIingridients[j].Id,
                            StorageId = source.Storage__GBIingridients[j].StorageId,
                            GBIingridientId = source.Storage__GBIingridients[j].GBIingridientId,
                            GBIingridientName = componentName,
                            Count = source.Storage__GBIingridients[j].Count
                        });
                    }
                }
                if (source.Storages[i].Id == id)
                {
                    return new StorageViewModel
                    {
                        Id = source.Storages[i].Id,
                        StorageName = source.Storages[i].StorageName,
                        Storage__GBIingridients = StockComponents
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddStorage(StorageBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id > maxId)
                {
                    maxId = source.Storages[i].Id;
                }
                if (source.Storages[i].StorageName == model.StorageName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }

        public void UpdStorage(StorageBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Storages[i].StorageName == model.StorageName &&
                    source.Storages[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Storages[index].StorageName = model.StorageName;
        }

        public void DelStorage(int id)
        {
            // при удалении удаляем все записи о компонентах на удаляемом складе
            for (int i = 0; i < source.Storage__GBIingridients.Count; ++i)
            {
                if (source.Storage__GBIingridients[i].StorageId == id)
                {
                    source.Storage__GBIingridients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
