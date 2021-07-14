using Ecoporto.AgendamentoCS.Config;
using Ecoporto.AgendamentoCS.Dados.Interfaces;
using Ecoporto.AgendamentoCS.Enums;
using Ecoporto.AgendamentoCS.Extensions;
using Ecoporto.AgendamentoCS.Helpers;
using Ecoporto.AgendamentoCS.Models;
using Ecoporto.AgendamentoCS.Models.ViewModels;
using Ecoporto.AgendamentoCS.Models.DTO;
using MvcRazorToPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecoporto.AgendamentoCS.Controllers
{
    public class UploadXMLNFEController : BaseController
    {

        private readonly IUploadXMLNfeRepositorio _uploadXMLNfeRepositorio;
        public UploadXMLNFEController(IUploadXMLNfeRepositorio uploadXMLNfeRepositorio)
        {
            _uploadXMLNfeRepositorio = uploadXMLNfeRepositorio;
        }

        // GET: UploadXMLNFE
        public ActionResult Index()
        {
            UploadXMLNfeDTO.id_transportadora = User.ObterTransportadoraId();

            ViewBag.ListarXmlDocs = _uploadXMLNfeRepositorio.GetListarRegistros(UploadXMLNfeDTO.id_transportadora);

            return View();
        }
        public JsonResult GetDeleteFiles(int id)
        {
            try
            {
                _uploadXMLNfeRepositorio.GetExcluirRegistro(id);

                return Json(new
                {
                    possuiDados = true,                     
                    Mensagem = "Dados excluídos com successo", 
                    statusRetorno = "200"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    possuiDados = true,
                    Mensagem = "Os dados não foram excluídos",
                    statusRetorno = "500"
                });
            }
        }
    }
}