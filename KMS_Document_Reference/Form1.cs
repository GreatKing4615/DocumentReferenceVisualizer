using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Document_Reference_Visualizer
{
    public partial class Form1 : Form
    {
        private int[][] coords;
        private int countDocument;
        private Random rnd=new Random();
        private Dictionary<int, int[]> coordNum = new Dictionary<int, int[]>();
        private List<Document> documents = new List<Document>();
        int cellWidth;
        int cellHeight;
        Rectangle rectangle;
        Graphics graph;
        public Form1()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += SearchEngine.BoyerMoore(ResourceTextBox.Text, TemplateTextBox.Text, SenseCheck.Checked);
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            //try
            //{
                Document document = new Document();
                openFileDialog1.ShowDialog();
                document.path = Path.GetDirectoryName(openFileDialog1.FileName);
                document.fileName = Path.GetFileName(openFileDialog1.FileName);


                countDocument++;
                countDocumentLabel.Text = countDocument.ToString();
                DrawingСoordinates();
                ChangeCoords(document);
                RefreshPaint();
                //PrintRectangle(document);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void RefreshPaint()
        {
            int[] _coord;
            foreach(Document document in documents)
            {
                _coord = coordNum[document.numCoord];
                PrintRectangle(document);
                graph.DrawString(document.fileName, new Font("Arial", 8), Brushes.Blue, new Point(_coord[0], _coord[1]));
            }
        }

        /// <summary>
        /// drawing a grid
        /// </summary>
        private void DrawingСoordinates()
        {
            graph = pictureBox1.CreateGraphics();
            graph.Clear(Color.White);
            int numOfCells = countDocument;
            cellWidth = pictureBox1.Width / countDocument;
            cellHeight = pictureBox1.Height / countDocument;
            Pen p = new Pen(Color.Black);
            coords = new int[numOfCells][];
            int num=0,offset;
            for (int y = 0; y < numOfCells; ++y)
            {
                offset = 0;
                graph.DrawLine(p, 0, y * cellHeight, numOfCells * cellWidth, y * cellHeight);
                graph.DrawLine(p, y * cellWidth, 0, y * cellWidth, numOfCells * cellHeight);
                coords[y] = new int[numOfCells];
                for (int j = 0; j < numOfCells; ++j)
                {
                    coords[j] = new int[4];
                    coords[j][0] = y * cellWidth + (cellWidth / 2);//x
                    coords[j][1] = j * cellHeight + (cellHeight / 2);//y
                    coords[j][2] = num+offset;//coordinate number
                    coords[j][3] = 0;//availability
                    offset++;
                    if (!coordNum.ContainsKey(coords[j][2]))
                        coordNum.Add(coords[j][2], coords[j]);
                    else
                    {
                        coords[j][3] = 1;
                        coordNum[coords[j][2]] = coords[j];
                    }
                    //graph.DrawString(coordNum[coords[j][2]].ToString(), new Font("Arial", 8), Brushes.Red, new Point(coordNum[coords[j][2]][1] , coordNum[coords[j][2]][2]));
                }
                num += offset;
            }
        }
        /// <summary>
        /// selection of coordinates for the document
        /// </summary>
        /// <param name="document">document for which the coordinates are looking for</param>
        private void ChangeCoords(Document document) {
            while (true)
            {
                int temp = rnd.Next(countDocument*countDocument);
                int[] coord = coordNum[temp];
                if ( coord[3] == 0 )
                {
                    coord[3] = 1;
                    document.x = coord[0];
                    document.y = coord[1];
                    document.numCoord = coord[2];
                    coordNum[temp] = coord;
                    documents.Add(document);
                    break;
                }
                else continue;
            }
            //graph.DrawString(document.fileName, new Font("Arial", 8), Brushes.Blue, new Point(document.x,document.y ));
        }

        private void PrintRectangle(Document document)
        {
            int[] _coord = coordNum[document.numCoord];
            graph.FillRectangle(Brushes.LightGray, new Rectangle(_coord[0]-cellWidth/4, _coord[1]-cellHeight/4, cellWidth/2,cellHeight/2));
            //graph.DrawString(document.fileName, new Font("Arial", 8), Brushes.Black,new Point(document.x, document.y));
        }
    }
}
