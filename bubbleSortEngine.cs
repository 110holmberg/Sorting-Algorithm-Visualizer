using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sortingAlgorithmVisualizer
{
    class bubbleSortEngine : ISortEngine
    {
        private int[] theArray;
        private Graphics g;
        private int maxVal;
        Brush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush blackBrush =  new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        public bubbleSortEngine(int[] theArrayIn, Graphics gIn, int maxValIn)
        {
            theArray = theArrayIn;
            g = gIn;
            maxVal = maxValIn;

        }
        public void nextStep()
        {
            for (int i = 0; i < theArray.Count() - 1; i++)
            {
                if (theArray[i] > theArray[i + 1])
                {
                    swap(i, i + 1);
                }

            }
        }
        public bool isSorted() //public so that it can be checked by the caller
        {
            for (int i = 0; i < theArray.Count() - 1; i++)
            {
                if (theArray[i] > theArray[i + 1]) return false;
            }
            return true;
        }
        private void swap(int i, int v)
        {
            int temp = theArray[i];
            theArray[i] = theArray[i + 1];
            theArray[i + 1] = temp;
            g.FillRectangle(blackBrush, i, 0, 1, maxVal);
            g.FillRectangle(blackBrush, v, 0, 1, maxVal);
            g.FillRectangle(whiteBrush, i, maxVal - theArray[i], 1, maxVal);
            g.FillRectangle(whiteBrush, v, maxVal - theArray[v], 1, maxVal);


        }
        public void reDraw()
        {

        }
    }
}
