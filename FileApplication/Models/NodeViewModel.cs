using System;
using System.Web.Script.Serialization;

namespace FileApplication.Models
{
    // DTO to work with jsTree and js filer plugins
    [Serializable]
    public class NodeViewModel
    {
        public string id { get; set; }

        public string text { get; set; }

        [ScriptIgnore]
        public string path { get; set; }

        public object children { get; set; }

        public int type { get; set; }

        [ScriptIgnore]
        public long size { get; set; }

        [ScriptIgnore]
        public bool includeRoot { get; set; }

        public StateModel state { get; set; }
    }

    public class StateModel
    {
        public bool opened { get; set; }
    }
}