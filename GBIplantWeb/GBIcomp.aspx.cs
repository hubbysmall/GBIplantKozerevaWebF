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
    public partial class GBIcomp : System.Web.UI.Page
    {
        private readonly IGBIingridientService service = UnityConfig.Container.Resolve<IGBIingridientService>();
        private int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    GBIingridientViewModel view = service.GetGBIingridient(id);
                    if (view != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            TextBoxName.Text = view.GBIingridientName;
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
                    service.UpdGBIingridient(new GBIingridientBindingModel
                    {
                        Id = id,
                        GBIingridient = TextBoxName.Text
                    });
                }
                else
                {
                    service.AddGBIingridient(new GBIingridientBindingModel
                    {
                        GBIingridient = TextBoxName.Text
                    });
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                Response.Redirect("GBIcomps.aspx");
            }
            Session["id"] = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
            Response.Redirect("GBIcomps.aspx");
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Session["id"] = null;
            Response.Redirect("GBIcomps.aspx");
        }
    }
}