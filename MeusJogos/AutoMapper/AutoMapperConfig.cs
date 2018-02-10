using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeusJogos.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //Faz o registro dos meus mapeamentos
            //Domínio -> View Model
            //View Model -> Domínio
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}