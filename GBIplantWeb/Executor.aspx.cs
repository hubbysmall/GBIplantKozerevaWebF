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
    public partial class Executor : System.Web.UI.Page
    {
        private readonly IExecutorService service = UnityConfig.Container.Resolve<IExecutorService>();
        private int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    ExecutorViewModel view = service.GetExecutor(id);
                    if (view != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            TextBoxName.Text = view.ExecutorFIO;
                        }
                    }
                    Page.DataBind();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните название');</script>");
                return;
            }
            try
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdExecutor(new ExecutorBindingModel
                    {
                        Id = id,
                        ExecutorFIO = TextBoxName.Text
                    });
                }
                else
                {
                    service.AddExecutor(new ExecutorBindingModel
                    {
                        ExecutorFIO = TextBoxName.Text
                    });
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                Response.Redirect("Executors.aspx");
            }
            Session["id"] = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Response.Redirect("Executors.aspx");
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Response.Redirect("Executors.aspx");
        }
    }
}