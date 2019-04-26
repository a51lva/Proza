using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proza
{
    public partial class Proza : Form
    {
        public Proza()
        {
            InitializeComponent();
        }

        private Boolean deleteEval()
        {
            const String evalDirectory = "%USERPROFILE%\\.IntelliJIdea2018.2\\config\\eval";

            if (Directory.Exists(evalDirectory))
            {
                try
                {
                    Directory.Delete(@evalDirectory, true);
                    return true;
                }
                catch (Exception e)
                {
                    history.Text = e.Message;
                    Console.WriteLine("The process failed: {0}", e.Message);
                    if (!string.IsNullOrWhiteSpace(history.Text))
                    {
                        history.AppendText("\r\n" + e.Message);
                    }
                    else
                    {
                        history.AppendText(e.Message);
                    }
                    history.ScrollToCaret();
                    return false;
                }
            }
            return false;
        }
    }
}
