using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using FileApplication.ManagerFactory;
using FileApplication.Managers;
using FileApplication.Models;

namespace FileApplication.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            IManagerFactory entityFactory = new DiskManagerFactory();
            FileManager = entityFactory.GetFileManager();
            FolderManager = entityFactory.GetFolderManager();
        }

        public List<NodeViewModel> MemoryNodes
        {
            get
            {
                var nodes = HttpContext.Application["MemoryNodes"];
                if (nodes == null)
                {
                    var f = new List<NodeViewModel>();
                    HttpContext.Application["MemoryNodes"] = f;
                }

                return (List<NodeViewModel>) nodes;
            }
            set
            {
                var nodes = HttpContext.Application["MemoryNodes"];
                if (nodes == null)
                {
                    var f = new List<NodeViewModel>();
                    f.AddRange(value);
                    HttpContext.Application["MemoryNodes"] = f;
                }
                else
                {
                    var f = (List<NodeViewModel>) nodes;
                    f.AddRange(value);
                    HttpContext.Application["MemoryNodes"] = f;
                }
            }
        } 

        public IFileManager FileManager { get; set; }

        public IFolderManager FolderManager { get; set; }

        #region Helpers
        public NodeViewModel GetNodeById(string id)
        {
            if (MemoryNodes != null && MemoryNodes.Count > 0 && id != "-1")
            {
                var node = MemoryNodes.FirstOrDefault(n => n.id == id);

                if (node != null)
                {
                    // not root folder or file
                    node.includeRoot = false;
                    return node;
                }

                return null;
            }

            // root node
            return new NodeViewModel
            {
                includeRoot = true,
                path = ConfigurationManager.AppSettings["mappedRootDir"]
            };
        }
        #endregion
    }
}