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
using System.Xml;
using System.Xml.Linq;

namespace Proza
{
    public partial class Proza : Form
    {
        const string configFolder = "%@USERPROFILE%\\.IntelliJIdea2018.2\\config";
        public Proza()
        {
            InitializeComponent();
        }

        private Boolean deleteEval()
        {
            const String evalDirectory = configFolder+"\\eval";

            if (Directory.Exists(evalDirectory))
            {
                try
                {
                    Directory.Delete(@evalDirectory, true);
                    return true;
                }
                catch (Exception e)
                {
                    this.putMessageToHistory(e.Message);
                    return false;
                }
            }
            return false;
        }

        private Boolean deleteEvlsprLines()
        {
            const String optionsXmlFile = configFolder + "\\options\\options.xml";

            if (File.Exists(optionsXmlFile))
            {
                try
                {
                    XDocument doc = XDocument.Load(@optionsXmlFile);
                    doc.Descendants("component").Where(rec => rec.Attribute("name").Value.StartsWith("evlspr")).Remove();
                    doc.Save(@optionsXmlFile);
                    return true;
                }
                catch(Exception e)
                {
                    this.putMessageToHistory(e.Message);
                    return false;
                }
                

            }
            return false;
        }

        private void putMessageToHistory(String message)
        {
            if (!string.IsNullOrWhiteSpace(history.Text))
            {
                history.AppendText("\r\n" + message);
            }
            else
            {
                history.AppendText(message);
            }
            history.ScrollToCaret();
        }

    }
}
