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
    public class ExecutorServiceDB : IExecutorService
    {
        private GBIwebDBcontext context;

        public ExecutorServiceDB()
        {
            this.context = new GBIwebDBcontext();
        }

        public ExecutorServiceDB(GBIwebDBcontext context)
        {
            this.context = context;
        }

        public List<ExecutorViewModel> GetList()
        {
            List<ExecutorViewModel> result = context.Executors
                .Select(rec => new ExecutorViewModel
                {
                    Id = rec.Id,
                    ExecutorFIO = rec.ExecutorFIO
                })
                .ToList();
            return result;
        }

        public ExecutorViewModel GetExecutor(int id)
        {
            Executor element = context.Executors.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ExecutorViewModel
                {
                    Id = element.Id,
                    ExecutorFIO = element.ExecutorFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddExecutor(ExecutorBindingModel model)
        {
            Executor element = context.Executors.FirstOrDefault(rec => rec.ExecutorFIO == model.ExecutorFIO);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            context.Executors.Add(new Executor
            {
                ExecutorFIO = model.ExecutorFIO
            });
            context.SaveChanges();         
        }

        public void UpdExecutor(ExecutorBindingModel model)
        {
            Executor element = context.Executors.FirstOrDefault(rec =>
                                        rec.ExecutorFIO == model.ExecutorFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = context.Executors.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ExecutorFIO = model.ExecutorFIO;
            context.SaveChanges();
        }

        public void DelExecutor(int id)
        {
            Executor element = context.Executors.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Executors.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
