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
    public partial class Refill : System.Web.UI.Page
    {
        private readonly IStorageService serviceH = UnityConfig.Container.Resolve<IStorageService>();

        private readonly IGBIingridientService serviceC = UnityConfig.Container.Resolve<IGBIingridientService>();

        private readonly IMainService serviceM = UnityConfig.Container.Resolve<IMainService>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<GBIingridientViewModel> listComps = serviceC.GetList();
                if (listComps != null)
                {
                    if (!Page.IsPostBack)
                    {
                        DropDownComps.DataSource = listComps;
                        // DropDownComps.DataBind();
                        DropDownComps.DataValueField = "Id";
                        DropDownComps.DataTextField = "GBIingridientName";                      
                    }

                }
                List<StorageViewModel> listHalls = serviceH.GetList();
                if (listHalls != null)
                {
                    if (!Page.IsPostBack)
                    {
                        DropDownHalls.DataSource = listHalls;
                        //DropDownHalls.DataBind();
                        DropDownHalls.DataValueField = "Id";
                        DropDownHalls.DataTextField = "StorageName";
                    }
                    
                }
                Page.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TextBoxQuantity.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поле Количество');</script>");
                return;
            }
            if (DropDownComps.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите компонент');</script>");
                return;
            }
            if (DropDownHalls.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите склад');</script>");
                return;
            }
            try
            {
                serviceM.PutGBIingridientInStorage(new Storage__GBIingridientBindingModel
                {
                    GBIingridientId = Convert.ToInt32(DropDownComps.SelectedValue),
                    StorageId = Convert.ToInt32(DropDownHalls.SelectedValue),
                    Count = Convert.ToInt32(TextBoxQuantity.Text)
                });
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}