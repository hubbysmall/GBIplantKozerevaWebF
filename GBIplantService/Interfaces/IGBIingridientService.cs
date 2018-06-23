using GBIplantService.BindingModels;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.Interfaces
{
   public interface IGBIingridientService
    {
       List<GBIingridientViewModel> GetList();

       GBIingridientViewModel GetGBIingridient(int id);

       void AddGBIingridient(GBIingridientBindingModel model);

       void UpdGBIingridient(GBIingridientBindingModel model);

       void DelGBIingridient(int id);
    }
}
