using MeusJogos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Infra.Data.EntityConfig
{
    public class EmprestimoSituacaoConfig : EntityTypeConfiguration<EmprestimoSituacao>
    {
        public EmprestimoSituacaoConfig()
        {
            HasKey(e => e.EmprestimoSituacaoID);

            Property(e => e.Descricao).IsRequired();
        }
    }
}
