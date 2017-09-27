using System;
using System.Collections.Generic;

namespace FileApplication.Models
{
    [Serializable]
    public class TreeViewModel
    {
        public bool status { get; set; }

        public string prompt { get; set; }

        public List<NodeViewModel> factor { get; set; } 
    }
}