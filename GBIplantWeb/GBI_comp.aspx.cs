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
    public partial class GBI_comp : System.Web.UI.Page
    {
        private readonly IGBIingridientService service = UnityConfig.Container.Resolve<IGBIingridientService>();
        private GBIpieceofArt__ingridientViewModel model;
        int save_selected;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<GBIingridientViewModel> list = service.GetList();
                if (list != null)
                {
                    if (!Page.IsPostBack)
                    {
                        DropDownListElement.DataSource = list;
                        DropDownListElement.DataValueField = "Id";
                        DropDownListElement.DataTextField = "GBIingridientName";
                        Page.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
            if (Session["prod_compId"] != null)
            {
                DropDownListElement.Enabled = false;
                DropDownListElement.SelectedValue = (string)Session["compId"];
                //TextBoxCount.Text = (string)Session["count"];
                selectedId.Text = (string)Session["selectedId"];
                //save_selected = (int)Session["selectedId"];
                save_selected = Convert.ToInt32(Session["selectedId"]);
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("GBI.aspx");
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCount.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поле Количество');</script>");
                return;
            }
            if (DropDownListElement.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите компонент');</script>");
                return;
            }
            try
            {
                
                 if (Session["prod_compId"] == null){
                model = new GBIpieceofArt__ingridientViewModel
                    {
                        GBIingridientId = Convert.ToInt32(DropDownListElement.SelectedValue),
                        GBIingridientName = DropDownListElement.SelectedItem.Text,
                        Count = Convert.ToInt32(TextBoxCount.Text)
                    };
                    Session["prod_compId"] = model.Id;
                    Session["GBIid"] = model.GBIpieceofArtId;
                    Session["compId"] = model.GBIingridientId;
                    Session["compName"] = model.GBIingridientName;
                    Session["count"] = model.Count;             
                   }
           
                else
                {
                    Session["selectedId"] = save_selected;


                    Session["count"] = Convert.ToInt32(TextBoxCount.Text).ToString();
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Response.Redirect("GBI.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}