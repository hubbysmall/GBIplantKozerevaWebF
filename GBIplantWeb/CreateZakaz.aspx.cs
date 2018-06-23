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
    public partial class CreateZakaz : System.Web.UI.Page
    {
        private readonly IBuyerService serviceB = UnityConfig.Container.Resolve<IBuyerService>();

        private readonly IGBIpieceOfArtService serviceGBI = UnityConfig.Container.Resolve<IGBIpieceOfArtService>();

        private readonly IMainService serviceM = UnityConfig.Container.Resolve<IMainService>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    List<BuyerViewModel> listB = serviceB.GetList();
                    if (listB != null)
                    {
                        DropDownBuyers.DataSource = listB;
                        DropDownBuyers.DataBind();
                        DropDownBuyers.DataTextField = "BuyerFIO";
                        DropDownBuyers.DataValueField = "Id";
                    }
                    List<GBIpieceOfArtViewModel> listGBI = serviceGBI.GetList();
                    if (listGBI != null)
                    {
                        DropDownGBIs.DataSource = listGBI;
                        DropDownGBIs.DataBind();
                        DropDownGBIs.DataTextField = "GBIpieceOfArtName";
                        DropDownGBIs.DataValueField = "Id";
                    }
                    Page.DataBind();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void Total()
        {

            if (DropDownGBIs.SelectedValue != null && !string.IsNullOrEmpty(TextBoxQuantity.Text))
            {
                try
                {
                    int id = Convert.ToInt32(DropDownGBIs.SelectedValue);
                    GBIpieceOfArtViewModel GBI = serviceGBI.GetGBIpieceOfArt(id);
                    int count = Convert.ToInt32(TextBoxQuantity.Text);
                    TextBoxTotal.Text = ((int)(count * GBI.Price)).ToString();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxQuantity.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните количество');</script>");
                return;
            }
            if (DropDownBuyers.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите клиента');</script>");
                return;
            }
            if (DropDownGBIs.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите ЖБИ');</script>");
                return;
            }
            try
            {
                serviceM.CreateZakaz(new ZakazBindingModel
                {
                    BuyerId = Convert.ToInt32(DropDownBuyers.SelectedValue),
                    GBIpieceOfArtId = Convert.ToInt32(DropDownGBIs.SelectedValue),
                    Count = Convert.ToInt32(TextBoxQuantity.Text),
                    Sum = Convert.ToInt32(TextBoxTotal.Text)
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

        protected void DropDownGBIs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Total();
        }

        protected void TextBoxTotal_TextChanged(object sender, EventArgs e)
        {
            Total();
        }

        protected void ButtonTotal_Click(object sender, EventArgs e)
        {
            Total();
        }
    }
}