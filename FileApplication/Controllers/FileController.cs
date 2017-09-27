using System.Net.Mime;
using System.Web.Mvc;
using FileApplication.Models;

namespace FileApplication.Controllers
{
    public class FileController : BaseController
    {
        public JsonResult CopyFile(string path)
        {
            FileManager.Copy(path);

            var res = new TreeViewModel { status = true, prompt = string.Empty };

            return Json(res, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GetFileInfo(string path)
        {
            FileManager.GetInfo(path);

            var res = new TreeViewModel { status = true, prompt = string.Empty };

            return Json(res, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult DeleteFile(string path)
        {
            FileManager.Delete(path);
            var res = new TreeViewModel { status = true, prompt = string.Empty };

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult RenameFile(string path, string name)
        {
            FileManager.Rename(path, name);
            var res = new TreeViewModel { status = true, prompt = string.Empty };

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult DownloadFile(string id)
        {
            var node = GetNodeById(id);

            if (node != null && node.type == (int) TypeEnum.File)
            {
                var fileModel = FileManager.Download(node.path);

                return File(fileModel.Content, MediaTypeNames.Application.Octet, fileModel.Name);
            }
            
            var res = new TreeViewModel { status = true, prompt = string.Empty };

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}