using System.Collections.Generic;

namespace Ecoporto.AgendamentoCS.Models
{
    public class NotaFiscal : Entidade<NotaFiscal>
    {
       

        public string Danfe { get; set; }

        public string NumeroNF { get; set; }

        public string SerieNF { get; set; }

        public string CFOP { get; set; }

        public int BookingCsItemId { get; set; }

        public string Reserva { get; set; }
        public string xml { get; set; }

        public NotaFiscal()
        {

        }

        public NotaFiscal(string danfe, string numeroNF, string serieNF, string xml)
        {
            Danfe = danfe;
            NumeroNF = numeroNF;
            SerieNF = serieNF;
            this.xml = xml;
        }
        public override void Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}