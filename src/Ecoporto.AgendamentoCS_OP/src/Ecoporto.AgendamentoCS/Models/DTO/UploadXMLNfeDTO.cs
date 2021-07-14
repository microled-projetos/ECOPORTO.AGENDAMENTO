using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecoporto.AgendamentoCS.Models.DTO
{
    public class UploadXMLNfe
    {
        public int ID { get; set; }
        public string Danfe { get; set; }
        public string Arquivo_XML { get; set; }
        public DateTime DataCadastro { get; set; }
        public int TransportadoraID { get; set; }
        public static int id_transportadora { get; set; }
        public string Razao { get; set; }
    }
}