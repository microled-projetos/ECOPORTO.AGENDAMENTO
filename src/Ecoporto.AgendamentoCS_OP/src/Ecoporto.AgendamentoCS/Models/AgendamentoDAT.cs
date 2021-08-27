using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecoporto.AgendamentoCS.Models
{
    public class AgendamentoDAT : Entidade<AgendamentoDAT>
    {
        public int AUTONUM { get; set; }
        public int AUTONUM_AGENDAMENTO { get; set; }
        public string DAT { get; set; }


        public AgendamentoDAT()
        { 
            
        }

        public override void Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}