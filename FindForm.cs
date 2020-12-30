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
using System.Text.RegularExpressions;

namespace Find
{
    public partial class FindForm : Form
    {
        string settingsDir = Environment.ExpandEnvironmentVariables("%AppData%\\FindSettings.txt");
        string directory = "";
        string pattern = "";
        public FindForm()
        {
            InitializeComponent();
            ReadInFile();
            textBoxDirectory.Text = directory;
            textBoxPattern.Text = pattern;
        }

        private void findBtn_Click(object sender, EventArgs e)
        {

            if (CheckDirectory(textBoxDirectory.Text))
                directory = textBoxDirectory.Text;

            pattern = textBoxPattern.Text;

            WriteInFile(directory, pattern);
        }
        public void WriteInFile(string dir, string pattern)
        {
            using (StreamWriter file = new StreamWriter(settingsDir, false))
            {
                file.WriteLine(dir);
                file.WriteLine(pattern);
            }
        }
        public void ReadInFile()
        {
            try
            {
                if (File.Exists(settingsDir))
                {
                    using (StreamReader sr = new StreamReader(settingsDir))
                    {
                        directory = sr.ReadLine();
                        pattern = sr.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка чтения файла настроек {e.ToString()}");
            }
        }
        public bool CheckDirectory(string dir)
        {
            if (String.IsNullOrEmpty(dir))
            {
                MessageBox.Show("Путь не должен быть пустым");
                return false;
            }

            if (Directory.Exists(dir))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Путь не найден");
                return false;
            }
        }
    }
}
