using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecoporto.AgendamentoCS.Models
{
    public class UploadXMLNfe : Entidade<UploadXMLNfe>
    {
        public int ID { get; set; }
        public string Danfe { get; set; }
        public string Arquivo_XML { get; set; }
        public int TransportadoraID { get; set; }
        public UploadXMLNfe(int id, string arquivoXml, string danfe)
        {
            Id = id;
            Arquivo_XML = arquivoXml;
            Danfe = danfe;

        }    
        public override void Validar()
        {
            throw new NotImplementedException();
        }
    }
}