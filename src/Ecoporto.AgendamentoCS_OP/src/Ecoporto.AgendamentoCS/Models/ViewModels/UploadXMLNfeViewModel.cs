using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ecoporto.AgendamentoCS.Models.ViewModels
{
    public class UploadXMLNfeViewModel
    {
        public int ID { get; set; }
        public string Danfe { get; set; }
        public string Arquivo_XML { get; set; }
        public int TransportadoraID { get; set; }
        public static int id_transportadora { get; set; }
    }
}