using System.Collections.Generic;

namespace Ecoporto.AgendamentoCS.Models
{
    public class AgendamentoDUE: Entidade<AgendamentoDUE>
    {
        public int AUTONUM { get; set; }
        public int AUTONUM_AGENDAMENTO { get; set; }
        public string DUE { get; set; }
        public int BookingCsItemId { get; set; }
        public string Reserva { get; set; }        

        public override void Validar()
        {
            throw new System.NotImplementedException();
        }

    }
}