using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Mostrar
{
    public class TreeViewNode
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public StateViewNode state { get; set; }
    }

    public class StateViewNode {
        public bool opened { get; set; }
    }
}