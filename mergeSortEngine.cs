using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingAlgorithmVisualizer
{
    class mergeSortEngine : ISortEngine
    {
        private int[] theArray;
        private Graphics g;
        private int maxVal;
        Brush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        public mergeSortEngine(int[] theArrayIn, Graphics gIn, int maxValIn)
        {
            theArray = theArrayIn;
            g = gIn;
            maxVal = maxValIn;
        }
        public void nextStep()
        {
            List<int> unsorted = theArray.Cast<int>().ToList();
            mergeSort(unsorted);
        }
        public List<int> mergeSort(List<int> unsorted)
        {
            List<int> left = new List<int>();
            List<int> right = new List<int>();
            int middle = unsorted.Count() / 2;
            for (int i =0; i < middle; i++)
            {
                left.Add(unsorted[i]);

            }
            for (int i = middle; i < theArray.Count(); i++)
            {
                right.Add(unsorted[i]);
            }
            left = mergeSort(left);
            right = mergeSort(right);
            return merge(left, right);
        }
        public List<int> merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();
            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 || right.Count > 0)
                {
                    if (left.Count > 0 && right.Count > 0)
                    {
                        if (left.First() <= right.First())
                        {
                            result.Add(left.First());
                            left.Remove(left.First());
                        }
                        else
                        {
                            result.Add(right.First());
                            right.Remove(right.First());
                        }
                    }
                    else if (left.Count > 0)
                    {
                        result.Add(left.First());
                        left.Remove(left.First());
                    }
                    else if (right.Count > 0)
                    {
                        result.Add(right.First());

                        right.Remove(right.First());
                    }
                }
            }
            return result;
        }
        public bool isSorted()
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
