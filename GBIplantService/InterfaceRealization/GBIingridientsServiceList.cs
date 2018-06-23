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
    public class GBIingridientsServiceList : IGBIingridientService
    {
        private ALLDataListSingleton source;

        public GBIingridientsServiceList()
        {
            source = ALLDataListSingleton.GetInstance();
        }

        public List<GBIingridientViewModel> GetList()
        {
            List<GBIingridientViewModel> result = new List<GBIingridientViewModel>();
            for (int i = 0; i < source.GBIindgridients.Count; ++i)
            {
                result.Add(new GBIingridientViewModel
                {
                    Id = source.GBIindgridients[i].Id,
                    GBIingridientName = source.GBIindgridients[i].GBIindgridientName
                });
            }
            return result;
        }

        public GBIingridientViewModel GetGBIingridient(int id)
        {
            for (int i = 0; i < source.GBIindgridients.Count; ++i)
            {
                if (source.GBIindgridients[i].Id == id)
                {
                    return new GBIingridientViewModel
                    {
                        Id = source.GBIindgridients[i].Id,
                        GBIingridientName = source.GBIindgridients[i].GBIindgridientName
                    };
                }
            }
            throw new Exception("Ингридиент не найден");
        }

        public void AddGBIingridient(GBIingridientBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.GBIindgridients.Count; ++i)
            {
                if (source.GBIindgridients[i].Id > maxId)
                {
                    maxId = source.GBIindgridients[i].Id;
                }
                if (source.GBIindgridients[i].GBIindgridientName == model.GBIingridient)
                {
                    throw new Exception("Уже есть Ингридиент с таким названием");
                }
            }
            source.GBIindgridients.Add(new GBIindgridient
            {
                Id = maxId + 1,
                GBIindgridientName = model.GBIingridient
            });
        }

        public void UpdGBIingridient(GBIingridientBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.GBIindgridients.Count; ++i)
            {
                if (source.GBIindgridients[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.GBIindgridients[i].GBIindgridientName == model.GBIingridient &&
                    source.GBIindgridients[i].Id != model.Id)
                {
                    throw new Exception("Уже есть Ингридиент с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Ингридиент не найден");
            }
            source.GBIindgridients[index].GBIindgridientName = model.GBIingridient;
        }

        public void DelGBIingridient(int id)
        {
            for (int i = 0; i < source.GBIindgridients.Count; ++i)
            {
                if (source.GBIindgridients[i].Id == id)
                {
                    source.GBIindgridients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Ингридиент не найден");
        }
    }
}
