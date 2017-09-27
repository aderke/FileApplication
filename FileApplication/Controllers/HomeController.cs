using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FileApplication.Models;

namespace FileApplication.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCatalogueTree(string id, string idt, string pid, string cmt, string cmd)
        {
            var node = GetNodeById(id);

            if (cmd == "opn")
            {
                return RedirectToAction("GetFolderInfo", "Folder", new { path = node.path, includeRoot = node.includeRoot });
            }
            else if (cmd == "new")
            {
                var parentNode = GetNodeById(pid);
                return RedirectToAction("CreateFolder", "Folder", new { path = parentNode.path, name = idt });
            }
            else if (cmd == "ren")
            {
                var attr = System.IO.File.GetAttributes(node.path);

                return attr.HasFlag(FileAttributes.Directory) 
                    ? RedirectToAction("RenameFolder", "Folder", new { path = node.path, name = idt }) 
                    : RedirectToAction("RenameFile", "File", new { path = node.path, name = idt });
            }
            else if (cmd == "del")
            {
                var attr = System.IO.File.GetAttributes(node.path);

                return attr.HasFlag(FileAttributes.Directory)
                    ? RedirectToAction("DeleteFolder", "Folder", new { path = node.path })
                    : RedirectToAction("DeleteFile", "File", new { path = node.path });
            }
            else if (cmd == "cpy")
            {
                var parentNode = GetNodeById(pid);
                var path = parentNode.path + @"\" + Uri.UnescapeDataString(idt);
                var attr = System.IO.File.GetAttributes(path);

                return attr.HasFlag(FileAttributes.Directory)
                    ? RedirectToAction("CopyFolder", "Folder", new { path = path })
                    : RedirectToAction("CopyFile", "File", new { path = path });
            }

            var res = new TreeViewModel { status = true, prompt = string.Empty };

            return Json(res);
        }

        [HttpPost]
        public JsonResult GetNodeInfo(string id)
        {
            if (MemoryNodes != null && MemoryNodes.Count > 0)
            {
                var node = MemoryNodes.FirstOrDefault(n => n.id == id);

                if (node != null)
                {
                    var readableSize = GetReadableSize(node.size);

                    return
                        Json(new {filetype = ((TypeEnum) node.type).ToString(), name = node.text, size = readableSize});
                }
            }

            return null;
        }
       
        #region Helpers
        private string GetReadableSize(long size)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double len = size;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }

        #endregion
    }
}