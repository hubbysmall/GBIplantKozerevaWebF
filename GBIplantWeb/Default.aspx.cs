using GBIplantService.Interfaces;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;
using Unity.Attributes;

namespace GBIplantWeb
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly IMainService service = UnityConfig.Container.Resolve<IMainService>();

        List<ZakazViewModel> list;

        private void LoadData()
        {
            try
            {
                list = service.GetList();
               
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void Halls_Click(object sender, EventArgs e)
        {
            Response.Redirect("Storages.aspx");
        }

        protected void Executors_Click(object sender, EventArgs e)
        {
            Response.Redirect("Executors.aspx");
        }

        protected void GBIcomponents_Click(object sender, EventArgs e)
        {
            Response.Redirect("GBIcomps.aspx");
        }

        protected void GBIs_Click(object sender, EventArgs e)
        {
            Response.Redirect("GBIs.aspx");
        }

        protected void Buyers_Click(object sender, EventArgs e)
        {
            Response.Redirect("Buyers.aspx");
        }

        protected void RefillHalls_Click(object sender, EventArgs e)
        {
            Response.Redirect("Refill.aspx");
        }

        protected void ButtonCreateOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateZakaz.aspx");
        }

        protected void ButtonTakeInPocess_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                string index = list[GridView1.SelectedIndex].Id.ToString();
                Session["id"] = index;
                Response.Redirect("TakeInProcess.aspx");
            }
        }

        protected void ButtonOrderReady_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                int id = list[GridView1.SelectedIndex].Id;
                try
                {
                    service.FinishZakaz(id);
                    LoadData();
                    Response.Redirect("Default.aspx");
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonOrderPaid_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                int id = list[GridView1.SelectedIndex].Id;
                try
                {
                    service.PayZakaz(id);
                    LoadData();
                    Response.Redirect("Default.aspx");
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            Response.Redirect("Default.aspx");
        }
    }
}