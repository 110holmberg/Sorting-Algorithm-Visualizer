using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sortingAlgorithmVisualizer
{
    public partial class Form1 : Form
    {
        int[] theArray;
        Graphics g;
        BackgroundWorker bgw = null; //in System.ComponentModel... so no namespace
        bool paused = false;
        public Form1()
        {
            InitializeComponent();
            populateDropdown();
        }
        private void populateDropdown()
        {
            List<string> classList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeof(ISortEngine).IsAssignableFrom(x)
            && !x.IsAbstract).Select(x => x.Name).ToList();  /* We are trying to get a list of names of classes that implement the interface.
            We go through the current app domain and get the assemblies that are implementations of the ISortEngine interface.
            We exclude the interface itself and any abstract classes. (There are no abstract classes right now, but it is there for completeness)
            We get the names of those candidates and cast them as a list. They are put in the variable classList. */
            classList.Sort(); //sort the list alphabetically
            foreach(string entry in classList)//populate the dropdown list with the names
            {
                comboBox1.Items.Add(entry);
            }
            comboBox1.SelectedIndex = 0; //set the combo box to the first entry
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnStart_Click(object sender, EventArgs e) //START BUTTON
        {
            if (theArray == null) btnReset_Click(null, null); //to allow the user to start sorting without first hitting reset
            bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerAsync(argument: comboBox1.SelectedItem); // run the background worker and pass to it what is selected in the dropdown 
        }
        private void btnPause_Click(object sender, EventArgs e) //PAUSE BUTTON
        {
            if (!paused)
            {
                bgw.CancelAsync();
                paused = true;
            }
            else
            {
                if (bgw.IsBusy) return;
                int numEntries = panel1.Width;
                int maxVal = panel1.Height;
                paused = false;
                for (int i = 0; i < numEntries; i++)
                {
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), i, 0, 1, maxVal);
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, maxVal - theArray[i], 1, maxVal);
                }
                bgw.RunWorkerAsync(argument: comboBox1.SelectedItem);
            }
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
        #region backgroundStuff
        public void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) //two arguments to event handler, what raised the event, and a block of arguments
        {
            BackgroundWorker bw = sender as BackgroundWorker; //explicitly identify sender as background worker
            string sortEngineName = (string)e.Argument; //now we know the name of the sort engine we would like to run
            Type type = Type.GetType("sortingAlgorithmVisualizer." + sortEngineName); //identifying of the class that we are going to create
            var ctors = type.GetConstructors(); //get the constructors of that type in our sorting engines
            //try-catch block in case something goes wrong
            try
            {
                ISortEngine se = (ISortEngine)ctors[0].Invoke(new object[] { theArray, g, panel1.Height }); //create a sort engine of the type identified and invoke its contructor
                /* We are creating a sort engine. We are invoking the first constructor in the list (there should only be one).
                   Pass to the constructor the list of the three parameters that we need. */
                while (!se.isSorted() && (!bgw.CancellationPending))
                {
                    se.nextStep();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


    }
}
