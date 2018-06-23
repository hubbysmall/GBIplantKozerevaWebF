using GBIplantService.BindingModels;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.Interfaces
{
    public interface IBuyerService
    {
        List<BuyerViewModel> GetList();

        BuyerViewModel GetBuyer(int id);

        void AddBuyer(BuyerBindingModel model);

        void UpdBuyer(BuyerBindingModel model);

        void DelBuyer(int id);
    }
}
