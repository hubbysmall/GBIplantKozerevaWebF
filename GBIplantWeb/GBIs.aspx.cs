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
    public partial class GBIs : System.Web.UI.Page
    {

        private readonly IGBIpieceOfArtService service = UnityConfig.Container.Resolve<IGBIpieceOfArtService>();
        List<GBIpieceOfArtViewModel> list;
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
            Response.Redirect("GBI.aspx");
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                int id = list[GridView1.SelectedIndex].Id;
                try
                {
                    service.DelGBIpieceOfArt(id);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
                LoadData();
                Response.Redirect("GBIs.aspx");
            }
        }

        protected void update_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                string index = list[GridView1.SelectedIndex].Id.ToString();
                Session["id"] = index;
                Session["renew"] = true;
                Response.Redirect("GBI.aspx");
            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {
            LoadData();
            Response.Redirect("GBIs.aspx");
        }

        protected void Home_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}