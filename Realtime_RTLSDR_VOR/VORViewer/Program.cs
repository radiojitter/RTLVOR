using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.IO;


namespace VORViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                DateTime t = DateTime.Now;
                string s_filename = string.Format("LOG_{0:D02}_{1:D02}_{2:D02}_{3:D02}_{4:D02}_{5:D02}_{6:D03}.csv",
                    t.Year,
                    t.Month,
                    t.Day,
                    t.Hour,
                    t.Minute,
                    t.Second,
                    t.Millisecond);

                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                string path = string.Format("{0}\\{1}", appPath, s_filename);

                FileInfo fi = new FileInfo(path);
                if (fi.Exists)
                {
                    throw new Exception(string.Format("LOG file {0} already exist", path));
                }

                using(StreamWriter w = new StreamWriter(fi.Create()))
                {
              
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain(w));
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
