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
    public partial class Executors : System.Web.UI.Page
    {
        private readonly IExecutorService service = UnityConfig.Container.Resolve<IExecutorService>();
        List<ExecutorViewModel> list;

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
            Response.Redirect("Executor.aspx");
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                int id = list[GridView1.SelectedIndex].Id;
                try
                {
                    service.DelExecutor(id);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
                LoadData();
                Response.Redirect("Executors.aspx");
            }
        }

        protected void update_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                string index = list[GridView1.SelectedIndex].Id.ToString();
                Session["id"] = index;
                Response.Redirect("Executor.aspx");
            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {
            LoadData();
            Response.Redirect("Executors.aspx");
        }

        protected void Home_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}