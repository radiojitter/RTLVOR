using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace VORViewer
{
    class SkytracGNSS
    {
        private SerialPort port;
        private StringBuilder sb = new StringBuilder();
        string last_line = "";
        public SkytracGNSS(string _port, int _baud)
        {
            string[] portList = _port.Split(',');
            int[] baudlist = new int[] { 115200, 19200, 57600, 38400, 9600, 4800, 2400 };

            foreach (string oneport in portList)
            {
                foreach (int onebaud in baudlist)
                {
                    try
                    {
                        port = new SerialPort(oneport,onebaud);
                        if (!port.IsOpen)
                        {
                            port.Open();
                        }
                       
                        return;
                    }
                    catch (Exception)
                    { }
                }
            }
            throw new Exception(string.Format("GNSS Receiver is not detected in any of the COM ports ({0}) specified",_port));
        }

        public bool IsLine()
        {
            if (port.BytesToRead > 0)
            {
                char ch = (char) port.ReadChar();
                if (ch == '\n')
                {
                    sb.Append(ch);
                    last_line = sb.ToString().Trim();
                    sb = new StringBuilder();
                    return true;
                }
                else
                {
                    sb.Append(ch);
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public string ReadLine()
        {
            return last_line;
        }

    }
}
