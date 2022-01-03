using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssignmentPenFormat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            prograssBar.Value = 10;
            insertFiles(); // dummy files creating method
        }
        private void insertFiles()
        {
            bool insertFiles = true; //boolean value for run
            char disk = cmbPort.SelectedItem.ToString()[0]; //get selected drive
            try
            {
                prograssBar.Value = 15; // this is for progras bar, not nessesory
                int i = 0;
                            while (insertFiles)
                            {
                                string path = @""+disk+ ":\\myFile"+i+".txt"; //location and name of file that you want to create
                                using (FileStream fs= new FileStream(path, FileMode.OpenOrCreate)) //this is the function for creating file series
                                {
                                    fs.SetLength(1024*1024); //size of the file
                                    using (TextWriter tw = new StreamWriter(fs)) //function for some txt that you want t write in the file
                                    {
                                        tw.WriteLine("hellow"); //txt inside the file
                                    }
                                }
                                    i++; //incrementing by one count

                            }
                prograssBar.Value = 50;// prograss bar
            }
            catch(Exception e)
            {
                insertFiles = false;//it become false if boolean value false
            }
            finally
            {
                prograssBar.Value = 60;// prograss bar
                FormatDrive_CommandLine(disk); // after creating dummy files, by this method it will format 
                prograssBar.Value = 100;// prograss bar
                MessageBox.Show("Complete");//show after creating dummy files
                this.Close();//end of the function
            }

        }
        public static bool FormatDrive_CommandLine(char driveLetter, string label = "", string fileSystem = "NTFS", bool quickFormat = true, bool enableCompression = false, int? clusterSize = null)
        {
            #region args check

            if (!Char.IsLetter(driveLetter) ||
                !IsFileSystemValid(fileSystem))
            {
                return false;
            }

            #endregion//code for Format 
            bool success = false;
            string drive = driveLetter + ":";//getting driver letter as a string
            try
            {
                var di = new DriveInfo(drive);
                var psi = new ProcessStartInfo();
                psi.FileName = "format.com";
                psi.CreateNoWindow = true; //if you want to hide the window
                psi.WorkingDirectory = Environment.SystemDirectory;
                psi.Arguments = "/FS:" + fileSystem +
                                             " /Y" +
                                             " /V:" + label +
                                             (quickFormat ? " /Q" : "") +
                                             ((fileSystem == "NTFS" && enableCompression) ? " /C" : "") +
                                             (clusterSize.HasValue ? " /A:" + clusterSize.Value : "") +
                                             " " + drive;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                var formatProcess = Process.Start(psi);
                var swStandardInput = formatProcess.StandardInput;
                swStandardInput.WriteLine();
                formatProcess.WaitForExit();
                success = true;
            }
            catch (Exception) { }
            return success;
        }

        private static bool IsFileSystemValid(string fileSystem)
        {
            throw new NotImplementedException();
        }

        public void test()
        {
            try
            {
                int j = 5000, m;
                DriveInfo[] allDrives = DriveInfo.GetDrives();

                foreach (DriveInfo d in allDrives)
                {
                    if (d.IsReady == true)
                    {
                        for (long i = 0; i <= 131072; i++)
                        {
                            try
                            {

                                using (TextWriter writer = File.CreateText(@"" + comboBox1.Text + " " + i + "dummy.txt"))
                                {
                                   
                                    for (m = 0; j >= m; m++)
                                    {
                                        writer.WriteLine("Hi " + m);

                                    }

                                }

                            }
                            catch 
                            {

                            }

                        }
                    }


                }
            }
            catch { }
        }
    }
}
    }
}
