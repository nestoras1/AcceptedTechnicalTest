using AcceptedTechnicalTest.DataRepository.Classes;
using AcceptedTechnicalTest.RequestResponses;
using AutoMapper;

namespace AcceptedTechnicalTest.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Match, MatchDao>();
            CreateMap<MatchOdds, MatchOddsDao>();
        }
    }
}
