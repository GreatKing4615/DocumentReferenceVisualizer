using System.Collections.Generic;

namespace Document_Reference_Visualizer
{
    public class Document
    {
        public int x { get; set; }
        public int y { get; set; }
        public int numCoord { get; set; }
        public string path { get; set; }
        public string fileName{ get; set; }
        public List<string> reference { get; set; }
    }
}
