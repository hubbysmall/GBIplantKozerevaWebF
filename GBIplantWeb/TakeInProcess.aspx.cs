using GBIplantService.BindingModels;
using GBIplantService.Interfaces;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;


namespace GBIplantWeb
{
    public partial class TakeInProcess : System.Web.UI.Page
    {
        private readonly IExecutorService serviceEx = UnityConfig.Container.Resolve<IExecutorService>();

        private readonly IMainService serviceM = UnityConfig.Container.Resolve<IMainService>();

        private int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Int32.TryParse((string)Session["id"], out id))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Не указан заказ');</script>");
                    Response.Redirect("Default.aspx");
                }
                List<ExecutorViewModel> listEx = serviceEx.GetList();
                if (listEx != null)
                {
                    DropDownExecutors.DataSource = listEx;
                    DropDownExecutors.DataBind();
                    DropDownExecutors.DataTextField = "ExecutorFIO";
                    DropDownExecutors.DataValueField = "Id";
                    DropDownExecutors.SelectedIndex = -1;
                }
                Page.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Response.Redirect("Default.aspx");
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (DropDownExecutors.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите исполнителя');</script>");
                return;
            }
            try
            {
                serviceM.TakeZakazInWork(new ZakazBindingModel
                {
                    Id = id,
                    ExecutorId = Convert.ToInt32(DropDownExecutors.SelectedValue)
                });
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Session["id"] = null;
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}