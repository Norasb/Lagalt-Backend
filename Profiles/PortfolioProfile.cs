using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Portfolio;

namespace Lagalt_Backend.Profiles
{
    /// <summary>
    /// Mappings for Portfolio entity to Post, Put and Read DTOs.
    /// </summary>
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<PortfolioPutDTO, Portfolio>();
            CreateMap<PortfolioPostDTO, Portfolio>();
            CreateMap<Portfolio, PortfolioDTO>()
                .ForMember(dto => dto.Projects, opt => opt
                .MapFrom(p => p.Projects.ToList()));
        }
    }
}
