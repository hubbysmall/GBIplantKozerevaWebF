using GBIplantService.BindingModels;
using GBIplantService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBIplantService.Interfaces
{
    public interface IGBIpieceOfArtService
    {
        List<GBIpieceOfArtViewModel> GetList();

        GBIpieceOfArtViewModel GetGBIpieceOfArt(int id);

        void AddGBIpieceOfArt(GBIpieceOfArtBindingModel model);

        void UpdGBIpieceOfArt(GBIpieceOfArtBindingModel model);

        void DelGBIpieceOfArt(int id);

        List<GBIpieceofArt__ingridientViewModel> GetListOfComps(int id);
    }
}
