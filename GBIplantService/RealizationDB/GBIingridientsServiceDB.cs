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
    public class GBIingridientsServiceDB : IGBIingridientService
    {
         private GBIwebDBcontext context;

        public GBIingridientsServiceDB()
        {
            this.context = new GBIwebDBcontext();
        }

        public GBIingridientsServiceDB(GBIwebDBcontext context)
        {
            this.context = context;
        }
         public List<GBIingridientViewModel> GetList()
         {
             List<GBIingridientViewModel> result = context.GBIindgridients
                 .Select(rec => new GBIingridientViewModel
                 {
                     Id = rec.Id,
                     GBIingridientName = rec.GBIindgridientName
                 })
                 .ToList();
             return result;
         }

         public GBIingridientViewModel GetGBIingridient(int id)
         {
             GBIindgridient element = context.GBIindgridients.FirstOrDefault(rec => rec.Id == id);
             if (element != null)
             {
                 return new GBIingridientViewModel
                 {
                     Id = element.Id,
                     GBIingridientName = element.GBIindgridientName
                 };
             }
             throw new Exception("Элемент не найден");
         }

         public void AddGBIingridient(GBIingridientBindingModel model)
         {
             GBIindgridient element = context.GBIindgridients.FirstOrDefault(rec => rec.GBIindgridientName == model.GBIingridient);
             if (element != null)
             {
                 throw new Exception("Уже есть компонент с таким названием");
             }            
             context.GBIindgridients.Add(new GBIindgridient
             {
                 GBIindgridientName = model.GBIingridient
             });
             context.SaveChanges();

         }

         public void UpdGBIingridient(GBIingridientBindingModel model)
         {
             GBIindgridient element = context.GBIindgridients.FirstOrDefault(rec =>
                                         rec.GBIindgridientName == model.GBIingridient && rec.Id != model.Id);
             if (element != null)
             {
                 throw new Exception("Уже есть компонент с таким названием");
             }
             element = context.GBIindgridients.FirstOrDefault(rec => rec.Id == model.Id);
             if (element == null)
             {
                 throw new Exception("Элемент не найден");
             }
             element.GBIindgridientName = model.GBIingridient;
             context.SaveChanges();
         }
         public void DelGBIingridient(int id)
         {
             GBIindgridient element = context.GBIindgridients.FirstOrDefault(rec => rec.Id == id);
             if (element != null)
             {
                 context.GBIindgridients.Remove(element);
                 context.SaveChanges();
             }
             else
             {
                 throw new Exception("Элемент не найден");
             }
         }
    }
}
