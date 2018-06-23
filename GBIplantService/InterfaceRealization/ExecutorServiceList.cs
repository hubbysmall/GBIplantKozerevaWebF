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
    public class ExecutorServiceList : IExecutorService
    {
        private ALLDataListSingleton source;

        public ExecutorServiceList()
        {
            source = ALLDataListSingleton.GetInstance();
        }

        public List<ExecutorViewModel> GetList()
        {
            List<ExecutorViewModel> result = new List<ExecutorViewModel>();
            for (int i = 0; i < source.Executors.Count; ++i)
            {
                result.Add(new ExecutorViewModel
                {
                    Id = source.Executors[i].Id,
                    ExecutorFIO = source.Executors[i].ExecutorFIO
                });
            }
            return result;
        }

        public ExecutorViewModel GetExecutor(int id)
        {
            for (int i = 0; i < source.Executors.Count; ++i)
            {
                if (source.Executors[i].Id == id)
                {
                    return new ExecutorViewModel
                    {
                        Id = source.Executors[i].Id,
                        ExecutorFIO = source.Executors[i].ExecutorFIO
                    };
                }
            }
            throw new Exception("Исполнитель не найден");
        }

        public void AddExecutor(ExecutorBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Executors.Count; ++i)
            {
                if (source.Executors[i].Id > maxId)
                {
                    maxId = source.Executors[i].Id;
                }
                if (source.Executors[i].ExecutorFIO == model.ExecutorFIO)
                {
                    throw new Exception("Уже есть Исполнитель с таким ФИО");
                }
            }
            source.Executors.Add(new Executor
            {
                Id = maxId + 1,
                ExecutorFIO = model.ExecutorFIO
            });
        }

        public void UpdExecutor(ExecutorBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Executors.Count; ++i)
            {
                if (source.Executors[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Executors[i].ExecutorFIO == model.ExecutorFIO &&
                    source.Executors[i].Id != model.Id)
                {
                    throw new Exception("Уже есть Исполнитель с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Исполнитель не найден");
            }
            source.Executors[index].ExecutorFIO = model.ExecutorFIO;
        }

        public void DelExecutor(int id)
        {
            for (int i = 0; i < source.Executors.Count; ++i)
            {
                if (source.Executors[i].Id == id)
                {
                    source.Executors.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Исполнитель не найден");
        }
    }
}
