using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sortingAlgorithmVisualizer
{
    public partial class Form1 : Form
    {
        int[] theArray;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            int numEntries = panel1.Width;
            int maxVal = panel1.Height;
            theArray = new int[numEntries];
            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 0, 0, numEntries, maxVal);
            Random rand = new Random();
            for (int i = 0; i < numEntries; i++)
            {
                theArray[i] = rand.Next(0, maxVal);
            }
            for (int i = 0; i < numEntries; i++)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, maxVal - theArray[i], 1, maxVal);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iSortEngine se = new bubbleSortEngine();
            se.DoWork(theArray, g, panel1.Height);
        }
    }
}
