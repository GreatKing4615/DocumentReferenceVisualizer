using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Document_Reference_Visualizer
{
    public partial class Form1 : Form
    {
        private int[][] xCoords;
        private int[][] yCoords;
        private int countDocument;
        private Random rnd=new Random();
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
            try
            {
                Document document = new Document();
                openFileDialog1.ShowDialog();
                document.path = Path.GetDirectoryName(openFileDialog1.FileName);
                document.fileName = Path.GetFileName(openFileDialog1.FileName);


                countDocument++;
                countDocumentLabel.Text = countDocument.ToString();
                DrawingСoordinates();
                ChangeCoords(document);
                //PrintRectangle(document);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            int cellWidth = pictureBox1.Width / countDocument;
            int cellHeight = pictureBox1.Height / countDocument;
            Pen p = new Pen(Color.Black);
            xCoords = new int[numOfCells][];
            yCoords = new int[numOfCells][];
            for (int y = 0, x = 0; y < numOfCells; ++y,++x)
            {
                graph.DrawLine(p, 0, y * cellHeight, numOfCells * cellWidth, y * cellHeight);
                graph.DrawLine(p, x * cellWidth, 0, x * cellWidth, numOfCells * cellHeight);
                for (int j = 0; j <= numOfCells; j++)
                {
                    //graph.DrawString(x.ToString(), new Font("Arial", 8), Brushes.Red, new Point(x * cellWidth - (cellWidth / 2), j * cellHeight - (cellHeight / 2)));
                    //graph.DrawString(y.ToString(), new Font("Arial", 8), Brushes.Blue, new Point(j * cellWidth - (cellWidth / 2) + 10, y * cellHeight - (cellHeight / 2)));
                    //xCoords[j] = new int[2];
                    //xCoords[j][0] = x * cellWidth - (cellWidth / 2);
                    //xCoords[j][1] = 0;
                    //yCoords[j] = new int[2];
                    //yCoords[j][0] = pictureBox1.Width / countDocument;
                    //yCoords[j][1] = 0;
                }
            }

        }
        /// <summary>
        /// selection of coordinates for the document
        /// </summary>
        /// <param name="document">document for which the coordinates are looking for</param>
        private void ChangeCoords(Document document) {
            while (true)
            {
                int temp = rnd.Next(countDocument-1);
                if (xCoords[temp][1] == 0)
                {
                    xCoords[temp][1] = 1;
                    document.x = xCoords[temp][1]+(countDocument/2);
                    break; 
                }
                else continue;
            }
            while (true)
            {
                int temp = rnd.Next(countDocument-1);
                if (yCoords[temp][1] == 0)
                {
                    yCoords[temp][1] = 1;
                    document.y = yCoords[temp][1] + (countDocument / 2);
                    break;
                }
                else continue;
            }
            graph.DrawString(document.fileName, new Font("Arial", 8), Brushes.Blue, new Point(document.x,document.y ));
        }

        private void PrintRectangle(Document document)
        {
            int width = countDocument;
            graph.FillRectangle(Brushes.White, new Rectangle(document.x, document.y, 40, 40));
            graph.DrawString(document.fileName, new Font("Arial", 8), Brushes.Black,new Point(document.x, document.y));
        }
    }
}
