using Ecoporto.AgendamentoConteiner.Extensions;
using System.Configuration;

namespace Ecoporto.AgendamentoConteiner.Config
{
    public static class AppConfig
    {
        public static string StringConexao() 
            => ConfigurationManager.ConnectionStrings["StringConexaoOracle"].ConnectionString;

        public static string UrlICC()
            => ConfigurationManager.AppSettings["UrlICC"].ToString();

        public static bool ValidarBDCC()
            => ConfigurationManager.AppSettings["ValidarBDCC"].ToInt() > 0;
    }
}