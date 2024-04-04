using AutoMapper;
using Joshua.Domain.Models;
using Joshua.Domain.ViewModels;

namespace Joshua.Application.AutoMapper
{
    public class ProfileMapper : Profile
    {

        public ProfileMapper() {

                CreateMap<Funcionario, FuncionarioViewModel>().ReverseMap();

                //CreateMap<EnderecoModel, EnderecoDto>().ReverseMap();
                //CreateMap<EnderecoModel, EnderecoDto>().ReverseMap();
        }
    }
}
