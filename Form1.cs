using Microsoft.Win32;
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
        private static string configFolderPath = "%USERPROFILE%\\.IntelliJIdea2018.2\\config";
        string configFolder = Environment.ExpandEnvironmentVariables(configFolderPath);

        public Proza()
        {
            InitializeComponent();
        }


        private Boolean deleteEval()
        {
            string evalDirectory = configFolder + "\\eval";

            if (Directory.Exists(evalDirectory))
            {
                try
                {
                    Directory.Delete(evalDirectory, true);
                    return true;
                }
                catch (Exception e)
                {
                    this.putMessageToHistory(e.Message);
                    return false;
                }
            }
            else
            {
                this.putMessageToHistory("Directory: " + evalDirectory + " not exist");
            }
            return false;
        }

        private Boolean deleteEvlsprLines()
        {
            string optionsXmlFile = configFolder + "\\options\\options.xml";

            if (File.Exists(optionsXmlFile))
            {
                try
                {
                    XDocument doc = XDocument.Load(optionsXmlFile);
                    doc.Descendants("property").Where(rec => rec.Attribute("name").Value.StartsWith("evlspr")).Remove();
                    doc.Save(optionsXmlFile);
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

        private Boolean deleteRegistryRow()
        {
            try
            {
                RegistryKey parentKey = Registry.CurrentUser;
                RegistryKey softwareKey = parentKey.OpenSubKey("Software\\JavaSoft\\Prefs\\jetbrains", true);
                softwareKey.DeleteSubKeyTree("idea");
                softwareKey.Close();
                parentKey.Close();
                return true;
            }catch(Exception e)
            {
                this.putMessageToHistory(e.Message);
                return false;
            }
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

        private void RunButton_Click(object sender, EventArgs e)
        {
            if (this.deleteEval())
            {
                this.putMessageToHistory("Eval folder deleted");
            }

            if (this.deleteEvlsprLines())
            {
                this.putMessageToHistory("Xml property stating with 'evlspr*' deleted from Options file");
            }

            if (this.deleteRegistryRow())
            {
                this.putMessageToHistory("Registry Idea folder deleted");
            }
        }
    }
}
