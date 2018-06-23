using GBIplantService.BindingModels;
using GBIplantService.Interfaces;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GBIplantWeb
{
    public partial class GBI : System.Web.UI.Page
    {

        private readonly IGBIpieceOfArtService service = UnityConfig.Container.Resolve<IGBIpieceOfArtService>();

        private int id;

        private List<GBIpieceofArt__ingridientViewModel> productComponents;

        private GBIpieceofArt__ingridientViewModel model;

        public Object returnGridViewSource() {
            return productComponents;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    GBIpieceOfArtViewModel view = service.GetGBIpieceOfArt(id);
                    if (view != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            textBoxName.Text = view.GBIpieceOfArtName;
                            textBoxPrice.Text = ((int)view.Price).ToString();
                        }
                        productComponents = view.GBIpieceofArt__ingridients;
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                productComponents = new List<GBIpieceofArt__ingridientViewModel>();
            }
            if (Session["prod_compId"] != null)
            {
                if (Session["selectedId"] != null)
                {                 
                    model = new GBIpieceofArt__ingridientViewModel
                    {
                        Id = Convert.ToInt32(Session["prod_compId"]),
                        GBIpieceofArtId = Convert.ToInt32(Session["GBIid"]),
                        GBIingridientId = Convert.ToInt32(Session["compId"]),
                        GBIingridientName = Convert.ToString(Session["compName"]),
                        Count = Convert.ToInt32(Session["count"])
                    };
                    productComponents[Convert.ToInt32(Session["selectedId"])] = model;
                }
                else
                {
                    model = new GBIpieceofArt__ingridientViewModel
                    {
                        GBIpieceofArtId = Convert.ToInt32(Session["GBIid"]),
                        GBIingridientId = Convert.ToInt32(Session["compId"]),
                        GBIingridientName = Convert.ToString(Session["compName"]),
                        Count = Convert.ToInt32(Session["count"])
                    };
                    productComponents.Add(model);
                }
                Session["prod_compId"] = null;
                Session["GBIid"] = null;
                Session["compId"] = null;
                Session["compName"] = null;
                Session["count"] = null;
                Session["selectedId"] = null;
            }
           
            List<GBIpieceofArt__ingridientBindingModel> productComponentBM = new List<GBIpieceofArt__ingridientBindingModel>();
            for (int i = 0; i < productComponents.Count; ++i)
            {
                productComponentBM.Add(new GBIpieceofArt__ingridientBindingModel
                {
                    Id = productComponents[i].Id,
                    GBIpieceofArtId = productComponents[i].GBIpieceofArtId,
                    GBIingridientId = productComponents[i].GBIingridientId,
                    Count = productComponents[i].Count
                });
            }
            if (productComponentBM.Count != 0)
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdGBIpieceOfArt(new GBIpieceOfArtBindingModel
                    {
                        Id = id,
                        GBIpieceOfArtName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        GBIpieceofArt__ingridients = productComponentBM
                    });
                }
                else
                {
                    
                    service.AddGBIpieceOfArt(new GBIpieceOfArtBindingModel
                    {

                        GBIpieceOfArtName = "empty",
                        Price = 0,
                        GBIpieceofArt__ingridients = productComponentBM
                    });
                    Session["id"] = service.GetList().Last().Id.ToString();

                }
            }
        }

       

        protected void add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните название');</script>");
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните цену');</script>");
                return;
            }
            Response.Redirect("GBI_comp.aspx");
        }

        protected void update_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                model = service.GetGBIpieceOfArt(id).GBIpieceofArt__ingridients[GridView1.SelectedIndex];
                Session["prod_compId"] = model.Id.ToString();
                Session["GBIid"] = model.GBIpieceofArtId.ToString();
                Session["compId"] = model.GBIingridientId.ToString();
                Session["compName"] = model.GBIingridientName;
                Session["count"] = model.Count.ToString();
                Session["selectedId"] = GridView1.SelectedIndex.ToString();
                Response.Redirect("GBI_comp.aspx");
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                try
                {
                    productComponents.RemoveAt(GridView1.SelectedIndex);

                    List<GBIpieceofArt__ingridientBindingModel> productComponentBM = new List<GBIpieceofArt__ingridientBindingModel>();
                    for (int i = 0; i < productComponents.Count; ++i)
                    {
                        productComponentBM.Add(new GBIpieceofArt__ingridientBindingModel
                        {
                            Id = productComponents[i].Id,
                            GBIpieceofArtId = productComponents[i].GBIpieceofArtId,
                            GBIingridientId = productComponents[i].GBIingridientId,
                            Count = productComponents[i].Count
                        });
                    }
                   
                        if (Int32.TryParse((string)Session["id"], out id))
                        {
                            service.UpdGBIpieceOfArt(new GBIpieceOfArtBindingModel
                            {
                                Id = id,
                                GBIpieceOfArtName = textBoxName.Text,
                                Price = Convert.ToInt32(textBoxPrice.Text),
                                GBIpieceofArt__ingridients = productComponentBM
                            });
                        }
                    Response.Redirect("GBI.aspx");

                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }

            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните название');</script>");
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните цену');</script>");
                return;
            }
            if (productComponents == null || productComponents.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните компоненты');</script>");
                return;
            }
            try
            {
                List<GBIpieceofArt__ingridientBindingModel> productComponentBM = new List<GBIpieceofArt__ingridientBindingModel>();
                for (int i = 0; i < productComponents.Count; ++i)
                {
                    productComponentBM.Add(new GBIpieceofArt__ingridientBindingModel
                    {
                        Id = productComponents[i].Id,
                        GBIpieceofArtId = productComponents[i].GBIpieceofArtId,
                        GBIingridientId = productComponents[i].GBIingridientId,
                        Count = productComponents[i].Count
                    });
                }
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdGBIpieceOfArt(new GBIpieceOfArtBindingModel
                    {
                        Id = id,
                        GBIpieceOfArtName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        GBIpieceofArt__ingridients = productComponentBM
                    });
                }
                else
                {
                    service.AddGBIpieceOfArt(new GBIpieceOfArtBindingModel
                    {
                        GBIpieceOfArtName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        GBIpieceofArt__ingridients = productComponentBM
                    });
                }
                Session["id"] = null;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Response.Redirect("GBIs.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {         
            if (String.Equals(Session["renew"], null))
            {
                service.DelGBIpieceOfArt(service.GetList().Last().Id);
            }
            Session["id"] = null;
            Session["renew"] = null;
            Response.Redirect("GBIs.aspx");
        }
    }
}