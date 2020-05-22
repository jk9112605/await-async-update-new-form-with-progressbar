using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppAsyncPrograssForm
{
    public partial class Form2 : Form
    {
        private bool completed;
        public Form2()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        public void SetComplete()
        {
            //this.InvokeIfRequired(new Action(() => button1.Enabled = true));
            this.InvokeIfRequired(x=> button1.Enabled = true);
        }

        public int GetCompletePercent => progressBar1.Value;

        public void UpdateProgress(int i)
        {
            Invoke(new Action(() =>
            {
                if (i == 100)
                {
                    progressBar1.Maximum = i;
                    progressBar1.Value = i;
                }
                //reference https://stackoverflow.com/a/47932843/4100915
                if (progressBar1.Value <= progressBar1.Maximum - 2)
                {
                    progressBar1.Value += 2;
                    progressBar1.Value--;
                }
                else if (progressBar1.Value <= progressBar1.Maximum)
                {
                    progressBar1.Maximum--;
                }
            }));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
    public static class ControlHelpers
    {
        public static void InvokeIfRequired<T>(this T control, Action<T> action) where T : ISynchronizeInvoke
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => action(control)), null);
            }
            else
            {
                action(control);
            }
        }
    }
}
