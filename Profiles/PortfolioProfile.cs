using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Portfolio;

namespace Lagalt_Backend.Profiles
{
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<PortfolioPutDTO, Portfolio>()
                .ForMember(p => p.Projects, opt => opt
                .MapFrom(dto => dto.ProjectId));
            CreateMap<PortfolioPostDTO, Portfolio>();
            CreateMap<Portfolio, PortfolioDTO>()
                .ForMember(dto => dto.Projects, opt => opt
                .MapFrom(p => p.Projects.ToList()));
        }
    }
}
