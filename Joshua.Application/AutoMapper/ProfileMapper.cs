using AutoMapper;
using Joshua.Domain.Models;
using Joshua.Domain.ViewModels;

namespace Joshua.Application.AutoMapper
{
    public class ProfileMapper : Profile
    {

        public ProfileMapper() {

                CreateMap<Funcionario, FuncionarioViewModel>().ReverseMap();
                CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
                CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        }
    }
}
