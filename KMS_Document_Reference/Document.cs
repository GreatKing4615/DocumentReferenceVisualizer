using DocumentFormat.OpenXml.Packaging;
using System.Collections.Generic;
using System.IO;

namespace Document_Reference_Visualizer
{
    public class Document
    {
        public int x { get; set; }
        public int y { get; set; }
        public int numCoord { get; set; }
        public string path { get; set; }
        public string fileName{ get; set; }
        public string fileNameWithoutExtencion{ get; set; }
        public List<Document> reference = new List<Document>();

        public string ReadText()
        {
            //using (var wordDocument = WordprocessingDocument.Open(path + "\\"+ fileName, false))
            //{
            //    var text = wordDocument.MainDocumentPart.Document.Body.InnerText;
            //    return text;
            //}
            using (FileStream fstream = File.OpenRead(path + "\\" + fileName))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                return textFromFile;
            }
        }
    }
}
