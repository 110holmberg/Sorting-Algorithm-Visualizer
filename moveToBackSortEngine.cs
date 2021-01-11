using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingAlgorithmVisualizer
{
    class moveToBackSortEngine : ISortEngine
    {
        private int[] theArray;
        private Graphics g;
        private int maxVal;
        Brush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        private int currentListPointer = 0;
        public moveToBackSortEngine(int[] theArrayIn, Graphics gIn, int maxValIn)
        {
            theArray = theArrayIn;
            g = gIn;
            maxVal = maxValIn;

        }
        public void nextStep()
        {
            if (currentListPointer >= theArray.Count() - 1) currentListPointer = 0;
            if (theArray[currentListPointer] > theArray[currentListPointer + 1])
            {
                rotate(currentListPointer);
            }
            currentListPointer++;
        }
        private void rotate(int currentListPointer)
        {
            int temp = theArray[currentListPointer];
            int endPoint = theArray.Count() - 1;

            for (int i = currentListPointer; i < endPoint; i++)
            {
                theArray[i] = theArray[i + 1];
                drawBar(i, theArray[i]);
            }

            theArray[endPoint] = temp;
            drawBar(endPoint, theArray[endPoint]);

        }
        public bool isSorted() //public so that it can be checked by the caller
        {
            for (int i = 0; i < theArray.Count() - 1; i++)
            {
                if (theArray[i] > theArray[i + 1]) return false;
            }
            return true;
        }
        private void drawBar(int position, int height)
        {
            g.FillRectangle(blackBrush, position, 0, 1, maxVal);
            g.FillRectangle(whiteBrush, position, maxVal - theArray[position], 1, maxVal);
        }
        public void reDraw()
        {
            for (int i = 0; i < (theArray.Count() - 1); i++)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, maxVal - theArray[i], 1, maxVal);
            }

        }
    }
}
