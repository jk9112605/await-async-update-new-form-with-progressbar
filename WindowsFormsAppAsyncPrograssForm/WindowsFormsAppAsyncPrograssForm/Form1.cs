using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppAsyncPrograssForm
{
    public partial class Form1 : Form
    {
        Form2 frm2;
        public Form1()
        {
            InitializeComponent();
            ProgressReport += Form1_ProgressReport;
            Completed += Form1_Completed;

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            //Task tsk1 = new Task(DoProcessing);
            using (Task tsk1 = Run())
            {
                using (frm2 = new Form2())
                {
                    frm2.ShowDialog();
                    await tsk1;
                    frm2 = null;
                }
            }
            btn.Enabled = true;
        }

        private void Form1_Completed(object sender, bool e)
        {
            if (frm2 != null)
                frm2.SetComplete();

        }

        private void Form1_ProgressReport(object sender, int e)
        {
            if (frm2 != null)
                frm2.UpdateProgress(e);
        }
        private event EventHandler<int> ProgressReport;
        private event EventHandler<bool> Completed;
        private async Task Run()
        {
            Text = "start";
            for (int i = 0; i < 100; i++)
            {
                ProgressReport?.Invoke(this,i+1);
                await Task.Delay(100);
            }
            Completed?.Invoke(this, true);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Class1 class1 = new Class1();
            using (frm2 = new Form2())
            {
                var progress = new Progress<int>();
                progress.ProgressChanged += (_sender, args) =>
                {
                    if (frm2 != null)
                        frm2.UpdateProgress(args);
                };
                Task task1 = class1.MyMethodAsync(progress).ContinueWith(x=> {
                    frm2.SetComplete();
                    //Thread.Sleep(500);
                    //Invoke(new Action(() =>
                    //{
                    //    frm2.SetComplete();
                    //}));
                });
                frm2.ShowDialog();
                await task1;
                //frm2.Completed = true;
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            //this method did not work
            frm2 = new Form2();
            Class1 class1 = new Class1();
            await class1.MyMethodAsync();
            frm2.ShowDialog();
        }
    }
}
