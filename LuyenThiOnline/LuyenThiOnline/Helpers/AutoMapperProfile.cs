using System.Linq;
using AutoMapper;
using LuyenThiOnline.DTOs;
using LuyenThiOnline.Models;

namespace LuyenThiOnline.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountBadge,AccountBadgeDTO>()
            .ForMember(dest => dest.BadgeLogo, opts => opts.MapFrom(src => src.Badge.BadgeLogo))
            .ForMember(dest => dest.Badgename, opts => opts.MapFrom(src => src.Badge.Badgename));
            CreateMap<AccountCertificate,CertificateForAccountDetailDTO>()
            .ForMember(dest => dest.CertificateName,opts => opts.MapFrom(src => src.Certificate.CertificateName))
            .ForMember(dest => dest.ExpireDate, opts => opts.MapFrom(src => src.ExpireDate));
            CreateMap<Account,AccountForDetailDTO>()
            .ForMember(dest => dest.LevelName, options => options.MapFrom(src => src.Level.LevelName))
            .ForMember(dest => dest.RoleName,options => options.MapFrom(src => src.Role.RoleName))
            .ForMember(dest => dest.Age,options => options.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Account,AccountForLevelListDTO>();
            CreateMap<Level,LevelDetailDTO>();
            CreateMap<Question,QuestionForExamDTO>();
            CreateMap<Badge,BadgeDTO>();
            CreateMap<Certificate,CertificateDTO>();
            CreateMap<Subject,SubjectDTO>()
            .ForMember(dest => dest.RankName, opts => opts.MapFrom(src => src.Rank.RankName));
            CreateMap<History,HistoryDTO>();
            CreateMap<AccountRegisterDTO,Account>();
        }
    }
}