using GBIplantService.BindingModels;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.Interfaces
{
    public interface IStorageService
    {
        List<StorageViewModel> GetList();

        StorageViewModel GetStorage(int id);

        void AddStorage(StorageBindingModel model);

        void UpdStorage(StorageBindingModel model);

        void DelStorage(int id);
    }
}
