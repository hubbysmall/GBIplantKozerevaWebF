﻿using GBIplantModel;
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
    public class StorageServiceBD : IStorageService
    {
         private GBIwebDBcontext context;

        public StorageServiceBD()
        {
            this.context = new GBIwebDBcontext();
        }

        public StorageServiceBD(GBIwebDBcontext context)
        {
            this.context = context;
        }

        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = context.Storages
                .Select(rec => new StorageViewModel
                {
                    Id = rec.Id,
                    StorageName = rec.StorageName,
                    Storage__GBIingridients = context.Storage__GBIingridients
                            .Where(recPC => recPC.StorageId == rec.Id)
                            .Select(recPC => new Storage__GBIingridientViewModel
                            {
                                Id = recPC.Id,
                                StorageId = recPC.StorageId,
                                GBIingridientId = recPC.GBIingridientId,
                                GBIingridientName = recPC.GBIindgridient.GBIindgridientName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }


        public StorageViewModel GetStorage(int id)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StorageViewModel
                {
                    Id = element.Id,
                    StorageName = element.StorageName,
                    Storage__GBIingridients = context.Storage__GBIingridients
                            .Where(recPC => recPC.StorageId == element.Id)
                            .Select(recPC => new Storage__GBIingridientViewModel
                            {
                                Id = recPC.Id,
                                StorageId = recPC.StorageId,
                                GBIingridientId = recPC.GBIingridientId,
                                GBIingridientName = recPC.GBIindgridient.GBIindgridientName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddStorage(StorageBindingModel model)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            context.Storages.Add(new Storage
            {
                StorageName = model.StorageName
            });
            context.SaveChanges();
        }

        public void UpdStorage(StorageBindingModel model)
        {
            Storage element = context.Storages.FirstOrDefault(rec =>
                                        rec.StorageName == model.StorageName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StorageName = model.StorageName;
            context.SaveChanges();
        }

        public void DelStorage(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Storage element = context.Storages.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // при удалении удаляем все записи о компонентах на удаляемом складе
                        context.Storage__GBIingridients.RemoveRange(
                                            context.Storage__GBIingridients.Where(rec => rec.StorageId == id));
                        context.Storages.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}
