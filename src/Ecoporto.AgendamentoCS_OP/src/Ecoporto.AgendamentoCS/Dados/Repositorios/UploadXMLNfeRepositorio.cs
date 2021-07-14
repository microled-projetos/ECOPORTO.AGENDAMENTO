using System;
using System.Text;
using Dapper;
using Ecoporto.AgendamentoCS.Config;
using Ecoporto.AgendamentoCS.Dados.Interfaces;
using Ecoporto.AgendamentoCS.Models.DTO;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Ecoporto.AgendamentoCS.Dados.Repositorios
{
    public class UploadXMLNfeRepositorio: IUploadXMLNfeRepositorio
    {
        public bool InsertDocXML(string danfe, string arquivo, int id_transportadora)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" SELECT SGIPA.SEQ_TB_UPLOAD_XML.NEXTVAL FROM DUAL  ");

                    int newID = con.Query<int>(sb.ToString()).FirstOrDefault();

                    sb.Clear();

                    sb.AppendLine(" DECLARE  ");
                    sb.AppendLine(" XML_Txt CLOB :=  '" + arquivo + "';");

                    sb.AppendLine(" BEGIN ");

                    sb.AppendLine(" INSERT INTO SGIPA.TB_UPLOAD_XML ");
                    sb.AppendLine(" (   ");
                    sb.AppendLine(" AUTONUM,  ");
                    sb.AppendLine(" DANFE, ");
                    sb.AppendLine(" ARQUIVO_XML,  ");
                    sb.AppendLine(" DATA_CADASTRO,  ");
                    sb.AppendLine(" AUTONUM_TRANSPORTADORA ");                    
                    sb.AppendLine(" )  VALUES (  ");
                    sb.AppendLine(" " + newID + ",  ");
                    sb.AppendLine(" '" + danfe + "', ");
                    sb.AppendLine(" XML_Txt, ");
                    sb.AppendLine(" SYSDATE, ");
                    sb.AppendLine(" " + id_transportadora + " ");
                    sb.AppendLine(" ); ");

                    sb.AppendLine(" END; ");


                    bool ret = con.Query<bool>(sb.ToString()).FirstOrDefault();

                    return ret;

                }  
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<UploadXMLNfeDTO> GetListarRegistros(int id)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" SELECT  ");
                    sb.AppendLine(" A.AUTONUM AS ID, ");
                    sb.AppendLine(" A.DANFE AS Danfe, ");
                    sb.AppendLine(" A.ARQUIVO_XML as Arquivo_XML, ");
                    sb.AppendLine(" A.DATA_CADASTRO as DataCadastro, ");
                    sb.AppendLine(" A.AUTONUM_TRANSPORTADORA as TransportadoraID, ");
                    sb.AppendLine(" B.RAZAO as Razao ");
                    sb.AppendLine(" FROM  ");
                    sb.AppendLine(" SGIPA.TB_UPLOAD_XML A ");
                    sb.AppendLine(" INNER JOIN SGIPA.TB_CAD_TRANSPORTADORAS B ");
                    sb.AppendLine(" ON (A.AUTONUM_TRANSPORTADORA =  B.AUTONUM )  ");
                    sb.AppendLine(" WHERE  ");
                    sb.AppendLine(" AUTONUM_TRANSPORTADORA = " + id);


                    var query = con.Query<UploadXMLNfeDTO>(sb.ToString()).AsEnumerable();

                    return query;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public UploadXMLNfeDTO GetExcluirRegistro(int id)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(AppConfig.StringConexao()))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" DELETE FROM SGIPA.TB_UPLOAD_XML  WHERE AUTONUM = " + id);

                    con.Query<UploadXMLNfeDTO>(sb.ToString()).FirstOrDefault();

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}