using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecoporto.AgendamentoCS.Models.DTO;

namespace Ecoporto.AgendamentoCS.Dados.Interfaces
{
    public interface IUploadXMLNfeRepositorio
    {
        bool InsertDocXML(string danfe, string arquivo, int id_transportadora);
        IEnumerable<UploadXMLNfe> GetListarRegistros(int id);
        UploadXMLNfe GetExcluirRegistro(int id);        
    }
}