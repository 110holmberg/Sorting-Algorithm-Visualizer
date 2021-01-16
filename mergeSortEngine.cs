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
            theArray = mergeSort(theArray);
            for (int i = 0; i < theArray.Count(); i++)
            {
                drawBar(i, theArray[i]);
            }

        }
        public int[] mergeSort(int[] numbers)
        {
            if (numbers.Count() <=1)
            {
                return numbers;
            }
            var left = new List<int>();
            var right = new List<int>();
            for (int i = 0; i < numbers.Length; i++)
            {
                if (i % 2 > 0)
                {
                    left.Add(numbers[i]);
                }
                else
                {
                    right.Add(numbers[i]);
                }
                theArray = (left.Concat(right).ToArray());
                for (int j = 0; j < theArray.Count(); j++)
                {
                    drawBar(j, theArray[j]);
                }
            }
            left = mergeSort(left.ToArray()).ToList();
            right = mergeSort(right.ToArray()).ToList();
            return merge(left, right);

        }
        public int[] merge(List<int> left, List<int> right)
        {
            var result = new List<int>();
            while (notEmpty(left) && notEmpty(right))
            {
                if (left.First() <= right.First())
                {
                    moveValueFromSourceToResult(left, result);
                    theArray = left.Concat(right).ToArray();
                    for (int j = 0; j < theArray.Count(); j++)
                    {
                        drawBar(j, theArray[j]);
                    }
                }
                else
                {
                    moveValueFromSourceToResult(right, result);
                    theArray = left.Concat(right).ToArray();
                    for (int j = 0; j < theArray.Count(); j++)
                    {
                        drawBar(j, theArray[j]);
                    }
                }

            }
            while (notEmpty(left))
            {
                moveValueFromSourceToResult(left, result);
            }
            while (notEmpty(right))
            {
                moveValueFromSourceToResult(right, result);
            }
            return result.ToArray();
        }

        private static bool notEmpty(List<int> list)
        {
            return list.Count() > 0;
        }

        public void moveValueFromSourceToResult(List<int> list, List<int> result)
        {
            result.Add(list.First());
            list.RemoveAt(0);
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
