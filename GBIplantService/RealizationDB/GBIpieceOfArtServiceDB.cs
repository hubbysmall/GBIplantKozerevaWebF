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
    public class GBIpieceOfArtServiceDB : IGBIpieceOfArtService
    {
        private GBIwebDBcontext context;

        public GBIpieceOfArtServiceDB()
        {
            this.context = new GBIwebDBcontext();
        }

        public GBIpieceOfArtServiceDB(GBIwebDBcontext context)
        {
            this.context = context;
        }

        public List<GBIpieceofArt__ingridientViewModel> GetListOfComps(int id)
        {
            List<GBIpieceofArt__ingridientViewModel> result = context.GBIpieceofArt__ingridients
                .Where(recC => recC.GBIpieceOfArtId == id)
                 .Select(rec => new GBIpieceofArt__ingridientViewModel
                 {           
                     Id = rec.Id,
                     GBIpieceofArtId = rec.GBIpieceOfArtId,
                     GBIingridientId = rec.GBIindgridientId,
                     GBIingridientName = context.GBIindgridients.FirstOrDefault(recIng => recIng.Id == rec.GBIindgridientId).GBIindgridientName,
                     Count = rec.Count
                 })
                 .ToList();
            return result;


/*
            List<GBIpieceofArt__ingridientViewModel> productComponents = new List<GBIpieceofArt__ingridientViewModel>();
            for (int j = 0; j < source.GBIpieceofArt__ingridients.Count; ++j)
            {
                if (source.GBIpieceofArt__ingridients[j].GBIpieceOfArtId == id)
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
                    productComponents.Add(new GBIpieceofArt__ingridientViewModel
                    {
                        Id = source.GBIpieceofArt__ingridients[j].Id,
                        GBIpieceofArtId = source.GBIpieceofArt__ingridients[j].GBIpieceOfArtId,
                        GBIingridientId = source.GBIpieceofArt__ingridients[j].GBIindgridientId,
                        GBIingridientName = componentName,
                        Count = source.GBIpieceofArt__ingridients[j].Count
                    });
                }
            }
            return productComponents;*/
        }
    

        public List<GBIpieceOfArtViewModel> GetList()
        {
            List<GBIpieceOfArtViewModel> result = context.GBIpieceOfArts
                .Select(rec => new GBIpieceOfArtViewModel
                {
                    Id = rec.Id,
                    GBIpieceOfArtName = rec.GBIpieceOfArtNAme,
                    Price = rec.Price,
                    GBIpieceofArt__ingridients = context.GBIpieceofArt__ingridients
                            .Where(recPC => recPC.GBIpieceOfArtId == rec.Id)
                            .Select(recPC => new GBIpieceofArt__ingridientViewModel
                            {
                                Id = recPC.Id,
                                GBIpieceofArtId = recPC.GBIpieceOfArtId,
                                GBIingridientId = recPC.GBIindgridientId,
                                GBIingridientName = context.GBIindgridients
                                    .FirstOrDefault(recC => recC.Id == recPC.GBIindgridientId).GBIindgridientName,   //?.GBIindgridientName
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public GBIpieceOfArtViewModel GetGBIpieceOfArt(int id)
        {
            GBIpieceOfArt element = context.GBIpieceOfArts.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new GBIpieceOfArtViewModel
                {
                    Id = element.Id,
                    GBIpieceOfArtName = element.GBIpieceOfArtNAme,
                    Price = element.Price,
                    GBIpieceofArt__ingridients = context.GBIpieceofArt__ingridients
                            .Where(recPC => recPC.GBIpieceOfArtId == element.Id)
                            .Select(recPC => new GBIpieceofArt__ingridientViewModel
                            {
                                Id = recPC.Id,
                                GBIpieceofArtId = recPC.GBIpieceOfArtId,
                                GBIingridientId = recPC.GBIindgridientId,
                                GBIingridientName = recPC.GBIindgridient.GBIindgridientName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddGBIpieceOfArt(GBIpieceOfArtBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    GBIpieceOfArt element = context.GBIpieceOfArts.FirstOrDefault(rec => rec.GBIpieceOfArtNAme == model.GBIpieceOfArtName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new GBIpieceOfArt
                    {
                        GBIpieceOfArtNAme = model.GBIpieceOfArtName,
                        Price = model.Price
                    };
                    context.GBIpieceOfArts.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupComponents = model.GBIpieceofArt__ingridients
                                                .GroupBy(rec => rec.GBIingridientId)
                                                .Select(rec => new
                                                {
                                                    GBIingridientId = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    // добавляем компоненты
                    foreach (var groupComponent in groupComponents)
                    {
                        context.GBIpieceofArt__ingridients.Add(new GBIpieceofArt__ingridient
                        {
                            GBIpieceOfArtId = element.Id,
                            GBIindgridientId = groupComponent.GBIingridientId,
                            Count = groupComponent.Count
                        });
                        context.SaveChanges();
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

        public void UpdGBIpieceOfArt(GBIpieceOfArtBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    GBIpieceOfArt element = context.GBIpieceOfArts.FirstOrDefault(rec =>
                                        rec.GBIpieceOfArtNAme == model.GBIpieceOfArtName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.GBIpieceOfArts.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.GBIpieceOfArtNAme = model.GBIpieceOfArtName;
                    element.Price = model.Price;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты
                    var compIds = model.GBIpieceofArt__ingridients.Select(rec => rec.GBIingridientId).Distinct();
                    var updateComponents = context.GBIpieceofArt__ingridients
                                                    .Where(rec => rec.GBIpieceOfArtId == model.Id &&
                                                        compIds.Contains(rec.GBIindgridientId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.Count = model.GBIpieceofArt__ingridients
                                                        .FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
                    }
                    context.SaveChanges();
                    context.GBIpieceofArt__ingridients.RemoveRange(
                                        context.GBIpieceofArt__ingridients.Where(rec => rec.GBIpieceOfArtId == model.Id &&
                                                                            !compIds.Contains(rec.GBIindgridientId)));
                    context.SaveChanges();
                    // новые записи
                    var groupComponents = model.GBIpieceofArt__ingridients
                                                .Where(rec => rec.Id == 0)
                                                .GroupBy(rec => rec.GBIingridientId)
                                                .Select(rec => new
                                                {
                                                    ComponentId = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    foreach (var groupComponent in groupComponents)
                    {
                        GBIpieceofArt__ingridient elementPC = context.GBIpieceofArt__ingridients
                                                .FirstOrDefault(rec => rec.GBIpieceOfArtId == model.Id &&
                                                                rec.GBIindgridientId == groupComponent.ComponentId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.GBIpieceofArt__ingridients.Add(new GBIpieceofArt__ingridient
                            {
                                GBIpieceOfArtId = model.Id,
                                GBIindgridientId = groupComponent.ComponentId,
                                Count = groupComponent.Count
                            });
                            context.SaveChanges();
                        }
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

        public void DelGBIpieceOfArt(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    GBIpieceOfArt element = context.GBIpieceOfArts.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.GBIpieceofArt__ingridients.RemoveRange(
                                            context.GBIpieceofArt__ingridients.Where(rec => rec.GBIpieceOfArtId == id));
                        context.GBIpieceOfArts.Remove(element);
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
