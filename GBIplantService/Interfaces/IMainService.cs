using GBIplantService.BindingModels;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.Interfaces
{
    public interface IMainService
    {
        List<ZakazViewModel> GetList();

        void CreateZakaz(ZakazBindingModel model);

        void TakeZakazInWork(ZakazBindingModel model);

        void FinishZakaz(int id);

        void PayZakaz(int id);

        void PutGBIingridientInStorage(Storage__GBIingridientBindingModel model);
    }
}
