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
    public class GBIpieceOfArtServiceList : IGBIpieceOfArtService
    {
        private ALLDataListSingleton source;

        public GBIpieceOfArtServiceList()
        {
            source = ALLDataListSingleton.GetInstance();
        }

        public List<GBIpieceOfArtViewModel> GetList()
        {
            List<GBIpieceOfArtViewModel> result = new List<GBIpieceOfArtViewModel>();
            for (int i = 0; i < source.GBIpieceOfArts.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<GBIpieceofArt__ingridientViewModel> productComponents = new List<GBIpieceofArt__ingridientViewModel>();
                for (int j = 0; j < source.GBIpieceofArt__ingridients.Count; ++j)
                {
                    if (source.GBIpieceofArt__ingridients[j].GBIpieceOfArtId == source.GBIpieceOfArts[i].Id)
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
                result.Add(new GBIpieceOfArtViewModel
                {
                    Id = source.GBIpieceOfArts[i].Id,
                    GBIpieceOfArtName = source.GBIpieceOfArts[i].GBIpieceOfArtNAme,
                    Price = source.GBIpieceOfArts[i].Price,
                    GBIpieceofArt__ingridients = productComponents
                });
            }
            return result;
        }

        public List<GBIpieceofArt__ingridientViewModel> GetListOfComps(int id)
        {
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
            return productComponents;
        }

            public GBIpieceOfArtViewModel GetGBIpieceOfArt(int id)
        {
            for (int i = 0; i < source.GBIpieceOfArts.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<GBIpieceofArt__ingridientViewModel> productComponents = new List<GBIpieceofArt__ingridientViewModel>();
                for (int j = 0; j < source.GBIpieceofArt__ingridients.Count; ++j)
                {
                    if (source.GBIpieceofArt__ingridients[j].GBIpieceOfArtId == source.GBIpieceOfArts[i].Id)
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
                if (source.GBIpieceOfArts[i].Id == id)
                {
                    return new GBIpieceOfArtViewModel
                    {
                        Id = source.GBIpieceOfArts[i].Id,
                        GBIpieceOfArtName = source.GBIpieceOfArts[i].GBIpieceOfArtNAme,
                        Price = source.GBIpieceOfArts[i].Price,
                        GBIpieceofArt__ingridients = productComponents
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddGBIpieceOfArt(GBIpieceOfArtBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.GBIpieceOfArts.Count; ++i)
            {
                if (source.GBIpieceOfArts[i].Id > maxId)
                {
                    maxId = source.GBIpieceOfArts[i].Id;
                }
                if (source.GBIpieceOfArts[i].GBIpieceOfArtNAme == model.GBIpieceOfArtName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.GBIpieceOfArts.Add(new GBIpieceOfArt
            {
                Id = maxId + 1,
                GBIpieceOfArtNAme = model.GBIpieceOfArtName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.GBIpieceofArt__ingridients.Count; ++i)
            {
                if (source.GBIpieceofArt__ingridients[i].Id > maxPCId)
                {
                    maxPCId = source.GBIpieceofArt__ingridients[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.GBIpieceofArt__ingridients.Count; ++i)
            {
                for (int j = 1; j < model.GBIpieceofArt__ingridients.Count; ++j)
                {
                    if (model.GBIpieceofArt__ingridients[i].GBIingridientId ==
                        model.GBIpieceofArt__ingridients[j].GBIingridientId)
                    {
                        model.GBIpieceofArt__ingridients[i].Count +=
                            model.GBIpieceofArt__ingridients[j].Count;
                        model.GBIpieceofArt__ingridients.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.GBIpieceofArt__ingridients.Count; ++i)
            {
                source.GBIpieceofArt__ingridients.Add(new GBIpieceofArt__ingridient
                {
                    Id = ++maxPCId,
                    GBIpieceOfArtId = maxId + 1,
                    GBIindgridientId = model.GBIpieceofArt__ingridients[i].GBIingridientId,
                    Count = model.GBIpieceofArt__ingridients[i].Count
                });
            }
        }

        public void UpdGBIpieceOfArt(GBIpieceOfArtBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.GBIpieceOfArts.Count; ++i)
            {
                if (source.GBIpieceOfArts[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.GBIpieceOfArts[i].GBIpieceOfArtNAme == model.GBIpieceOfArtName &&
                    source.GBIpieceOfArts[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.GBIpieceOfArts[index].GBIpieceOfArtNAme = model.GBIpieceOfArtName;
            source.GBIpieceOfArts[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.GBIpieceofArt__ingridients.Count; ++i)
            {
                if (source.GBIpieceofArt__ingridients[i].Id > maxPCId)
                {
                    maxPCId = source.GBIpieceofArt__ingridients[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.GBIpieceofArt__ingridients.Count; ++i)
            {
                if (source.GBIpieceofArt__ingridients[i].GBIpieceOfArtId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.GBIpieceofArt__ingridients.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.GBIpieceofArt__ingridients[i].Id == model.GBIpieceofArt__ingridients[j].Id)
                        {
                            source.GBIpieceofArt__ingridients[i].Count = model.GBIpieceofArt__ingridients[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if(flag)
                    {
                        source.GBIpieceofArt__ingridients.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.GBIpieceofArt__ingridients.Count; ++i)
            {
                if (model.GBIpieceofArt__ingridients[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.GBIpieceofArt__ingridients.Count; ++j)
                    {
                        if (source.GBIpieceofArt__ingridients[j].GBIpieceOfArtId == model.Id &&
                            source.GBIpieceofArt__ingridients[j].GBIindgridientId == model.GBIpieceofArt__ingridients[i].GBIingridientId)
                        {
                            source.GBIpieceofArt__ingridients[j].Count += model.GBIpieceofArt__ingridients[i].Count;
                            model.GBIpieceofArt__ingridients[i].Id = source.GBIpieceofArt__ingridients[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.GBIpieceofArt__ingridients[i].Id == 0)
                    {
                        source.GBIpieceofArt__ingridients.Add(new GBIpieceofArt__ingridient
                        {
                            Id = ++maxPCId,
                            GBIpieceOfArtId = model.Id,
                            GBIindgridientId = model.GBIpieceofArt__ingridients[i].GBIingridientId,
                            Count = model.GBIpieceofArt__ingridients[i].Count
                        });
                    }
                }
            }
        }

        public void DelGBIpieceOfArt(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.GBIpieceofArt__ingridients.Count; ++i)
            {
                if (source.GBIpieceofArt__ingridients[i].GBIpieceOfArtId == id)
                {
                    source.GBIpieceofArt__ingridients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.GBIpieceOfArts.Count; ++i)
            {
                if (source.GBIpieceOfArts[i].Id == id)
                {
                    source.GBIpieceOfArts.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
