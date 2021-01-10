using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace sortingAlgorithmVisualizer
{
    interface ISortEngine
    {
        void nextStep();
        bool isSorted();
        void reDraw();
    }
}
