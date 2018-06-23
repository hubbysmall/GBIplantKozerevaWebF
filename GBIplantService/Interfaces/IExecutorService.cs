using GBIplantService.BindingModels;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.Interfaces
{
    public interface IExecutorService
    {
        List<ExecutorViewModel> GetList();

        ExecutorViewModel GetExecutor(int id);

        void AddExecutor(ExecutorBindingModel model);

        void UpdExecutor(ExecutorBindingModel model);

        void DelExecutor(int id);
    }
}
