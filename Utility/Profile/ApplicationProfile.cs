using lab04.ViewModels;
using WebApplication1.Models;

namespace lab04.Utility.Profile
{
    public class ApplicationProfile : AutoMapper.Profile
    {

        public ApplicationProfile()
        {
            CreateMap<Brand, BrandViewModel>().ReverseMap();
        }

    }
}
