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
    public partial class GBIcomps : System.Web.UI.Page
    {
        private readonly IGBIingridientService service = UnityConfig.Container.Resolve<IGBIingridientService>();
        List<GBIingridientViewModel> list;
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

        protected void add_Click(object sender, EventArgs e)
        {
            Response.Redirect("GBIcomp.aspx");
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                int id = list[GridView1.SelectedIndex].Id;
                try
                {
                    service.DelGBIingridient(id);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
                LoadData();
                Response.Redirect("GBIcomps.aspx");
            }
        }

        protected void update_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                string index = list[GridView1.SelectedIndex].Id.ToString();
                Session["id"] = index;
                Response.Redirect("GBIcomp.aspx");
            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {
            LoadData();
            Response.Redirect("GBIcomps.aspx");
        }

        protected void Home_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}