using SAC.Helpers;
using SAC.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using SAC.Models;

namespace SAC.Controllers
{
    public class ArchivoController : Controller
    {

        ArchivoHelper  archivoHelper;
        String tempPath = "~/Temporal/";
        String serverMapPath = "~/Archivos/Temporal/";
        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }
        private string UrlBase = "/Archivos/Temporal/";
        String DeleteURL = "/Archivo/DeleteFile/?file=";
        String DeleteType = "GET";

        public ArchivoController()
        {
            archivoHelper = new ArchivoHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
        }

     
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            JsonFiles ListOfFiles = archivoHelper.GetFileList();
            var model = new ArchivoView()
            {
                Files = ListOfFiles.files
            };

            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;

            archivoHelper.UploadAndShowResults(CurrentContext, resultList);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            else
            {
                return Json(files);
            }
        }
        public JsonResult GetFileList()
        {
            var list = archivoHelper.GetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteFile(string file)
        {
            archivoHelper.DeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Descargar(int archivoId, int version)
        {
            EtapasModel modelEtapas = new EtapasModel();
            byte[] contenido = modelEtapas.obtenerContenidoArchivo(archivoId, version);
            Archivo archivo = modelEtapas.obtenerArchivoPorId(archivoId, version);
            return File(contenido, "application/force-download", archivo.Nombre + "." + archivo.Extension);
        }


    }
}