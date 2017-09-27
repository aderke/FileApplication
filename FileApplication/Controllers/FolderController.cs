using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using FileApplication.Models;

namespace FileApplication.Controllers
{
    public class FolderController : BaseController
    {
        public JsonResult CopyFolder(string path)
        {
            FolderManager.Copy(path);
            var res = new TreeViewModel { status = true, prompt = string.Empty };

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetFolderInfo(string path, bool includeRoot)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            
            var model = FolderManager.GetInfo(path);
            var nodes = ConvertToNodeViewModel(model, includeRoot);
            var res = new TreeViewModel { factor = nodes, status = true, prompt = string.Empty };

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult DeleteFolder(string path)
        {
            FolderManager.Delete(path);
            var res = new TreeViewModel { status = true, prompt = string.Empty };

            return Json(res, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult RenameFolder(string path, string name)
        {
            FolderManager.Rename(path, name);
            var res = new TreeViewModel { status = true, prompt = string.Empty };

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult CreateFolder(string path, string name)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            FolderManager.Create(path, name);
            var newPath = path + @"\" + name;
            
            var newNode = CreateNewFolderNode(name, newPath);
            MemoryNodes.Add(newNode);

            var res = new TreeViewModel { factor = new List<NodeViewModel> { newNode }, status = true, prompt = string.Empty };

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UploadToFolder(string id, HttpPostedFileBase file)
        {
            var node = GetNodeById(id);
            FolderManager.UploadTo(file.InputStream, node.path + @"\" + file.FileName);

            return Json(new { file = file.FileName, folder = node.path });
        }

        #region Helpers
        private static NodeViewModel CreateNewFolderNode(string name, string newPath)
        {
            return new NodeViewModel
            {
                id = newPath.GetHashCode().ToString(),
                path = newPath,
                text = name,
                type = (int)TypeEnum.Folder,
                state = new StateModel { opened = false },
                size = 0,
                children = false
            };
        }

        private List<NodeViewModel> ConvertToNodeViewModel(FolderModel root, bool includeRoot = false)
        {
            var fml = new List<NodeViewModel>();

            // folder children
            foreach (var folder in root.Folders)
            {
                var fm = new NodeViewModel
                {
                    id = folder.Path.GetHashCode().ToString(),
                    path = folder.Path,
                    text = folder.Name,
                    type = (int) TypeEnum.Folder,
                    state = new StateModel { opened = false },
                    size = folder.Size,
                    children = true
                };
                MemoryNodes.Add(fm);
                fml.Add(fm);
            }

            // file children
            var fl = new List<NodeViewModel>();

            foreach (var file in root.Files)
            {
                var f = new NodeViewModel
                {
                    id = file.Path.GetHashCode().ToString(),
                    path = file.Path,
                    text = file.Name,
                    size = file.Size,
                    type = (int) TypeEnum.File,
                    state = new StateModel { opened = false },
                    children = false
                };
                MemoryNodes.Add(f);
                fl.Add(f);
            }

            fml.AddRange(fl);

            if (includeRoot)
            {
                var rootModel = new NodeViewModel
                {
                    id = root.Path.GetHashCode().ToString(),
                    path = root.Path,
                    text = root.Name,
                    type = (int) TypeEnum.Root,
                    state = new StateModel {opened = true},
                    children = fml,
                };
                MemoryNodes.Add(rootModel);
                return new List<NodeViewModel> {rootModel};
            }
            else
            {
                return fml;
            }
        }
        
        #endregion
    }
}