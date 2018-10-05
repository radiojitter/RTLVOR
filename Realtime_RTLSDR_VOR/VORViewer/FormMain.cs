using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace VORViewer
{
    public partial class FormMain : Form
    {
        MySettings AllSettings;
        SkytracGNSS gnss;
        private static bool bQuit = false;

        Thread gnssThread;
        TcpClient clientPort;

        Queue<double> PhaseQ = new Queue<double>();

        StreamWriter w;
        public FormMain(StreamWriter _w)
        {
            w = _w;
            InitializeComponent();
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                AllSettings = new MySettings();
                string tcp_ip;
                int tcp_port;

                lock (AllSettings)
                {
                    tbVORLat.Text = AllSettings.VORLat.ToString();
                    tbVORLong.Text = AllSettings.VORLong.ToString();
                    tbVORFreq.Text = AllSettings.VORFrequency.ToString();

                    tcp_ip = AllSettings.TcpIPAddress;
                    tcp_port = AllSettings.TcpPort;
                }

                OnDisplayLocation();

                clientPort = new TcpClient();
                clientPort.Connect(tcp_ip, tcp_port);

                gnss = new SkytracGNSS(AllSettings.COMPort, AllSettings.BAUDRate);

                gnssThread = new Thread(GNSSThread);
                gnssThread.Priority = ThreadPriority.Lowest;
                gnssThread.Start();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        public bool IsQuit()
        {
            if (this.IsDisposed)
            {
                bQuit = true;
                return true;
            }

            if (bQuit)
                return true;

            return false;
        }

        private void Local_GNSS()
        {

            lock (gnss)
            {
                if (gnss.IsLine())
                {
                    string line = gnss.ReadLine();
                    //Print("{0}\n", line);

                    string[] token = line.Split(',');
                    if (token.Length > 0)
                    {
                        string first = token[0].Trim().ToUpper();
                        if (first.IndexOf('$') != -1 && first.IndexOf("RMC") != -1)
                        {

                            string lat_v = token[3].Trim();
                            string lat_NS = token[4].Trim();

                            string long_v = token[5].Trim();
                            string long_EW = token[6].Trim();

                            UpdateLocation(lat_v, lat_NS, long_v, long_EW);

                        }
                        else if (first.IndexOf('$') != -1 && first.IndexOf("GLL") != -1)
                        {
                            string lat_v = token[1].Trim();
                            string lat_NS = token[2].Trim();

                            string long_v = token[3].Trim();
                            string long_EW = token[4].Trim();

                            UpdateLocation(lat_v, lat_NS, long_v, long_EW);
                        }
                        else if (first.IndexOf('$') != -1 && first.IndexOf("GGA") != -1)
                        {
                            string lat_v = token[2].Trim();
                            string lat_NS = token[3].Trim();

                            string long_v = token[4].Trim();
                            string long_EW = token[5].Trim();

                            UpdateLocation(lat_v, lat_NS, long_v, long_EW);
                        }
                    }
                }
            }
        }

        void OnReceiveData()
        {
            try
            {
                if (IsQuit())
                    return;
                if (clientPort == null)
                    return;
                int len = clientPort.Available;
                const int needed = 32 * 2/8;

                if (len >= needed)
                {
                    NetworkStream reader = clientPort.GetStream();
                    if (reader.CanRead)
                    {
                        _OnReceiveData(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Print("Exception in TCPClient: {0}\n", ex.Message);
            }
        }

        void _OnReceiveData(NetworkStream s)
        {
            byte[] buffer = new byte[32 * 2/8];
            s.Read(buffer, 0, buffer.Length);

            double d1 = (double) BitConverter.ToSingle(buffer, 32 * 0/8);
            double d2 = (double) BitConverter.ToSingle(buffer, 32 * 1/8);

            double sig_level = Math.Sqrt(d1 * d1 + d2 * d2);
            double sig_phase = -Math.Atan2(d2, d1) * 180 / Math.PI;

            bool b_calibrate = false;
            lock (cbCalibrate)
            {
                b_calibrate = cbCalibrate.Checked;
            }

            lock (PhaseQ)
            {
                int nItems = PhaseQ.Count;
                int nPointAverage;
                double af;
                lock (AllSettings)
                {
                    nPointAverage = AllSettings.nPointAverage;
                    af = AllSettings.nPointAverage;
                }

                if (nItems >= (nPointAverage-1))
                {
                    double[] phases = PhaseQ.ToArray();
                    PhaseQ.Clear(); 

                    double sum = 0.0;
                    double sum2 = 0.0;
                    double N = 0.0;
                    foreach (double phase in phases)
                    {
                        sum += phase;
                        sum2 += phase * phase;
                        N = N+1.0;
                    }

                    double sd = Math.Sqrt(N * sum2 - sum*sum) / N;
                    double ave = sum/N;

                    UpdateVORSD(sd);

                    if (b_calibrate)
                    {
                        lock (AllSettings)
                        {
                            if (sd <= AllSettings.CalibrateSD)
                            {
                                double err = ave - AllSettings.GNSSBearing;
                                Print("Calibrated at {0} : Average bearing is {1} deg over {2} points, GNSS is {3} deg, Error = {4} deg\n", 
                                    DateTime.Now.ToLongTimeString(),
                                    ave,
                                    AllSettings.nPointAverage,
                                    AllSettings.GNSSBearing, 
                                    err);
                                AllSettings.VORError = AllSettings.VORAverageFactor * err + (1.0 - AllSettings.VORAverageFactor) * AllSettings.VORError;

                            }
                        }
                    }


                }
            }


            double new_bearing,  new_sig_level, new_error;
            lock (AllSettings)
            {
                AllSettings.VORBearing = AllSettings.VORAverageFactor * sig_phase + (1 - AllSettings.VORAverageFactor) * AllSettings.VORBearing;
                new_bearing = AllSettings.VORBearing;
                new_error = AllSettings.VORError;

                AllSettings.VORSignalLevel = AllSettings.VORAverageFactor * sig_level + (1 - AllSettings.VORAverageFactor) * AllSettings.VORSignalLevel;
                new_sig_level = AllSettings.VORSignalLevel;

            }

            lock (PhaseQ)
            {
                PhaseQ.Enqueue(new_bearing);
            }

            UpdateVORBearing(new_bearing-new_error);
            UpdateVORSignalLevel(new_sig_level);
            UpdateVORError(new_error);

            UpdateCompass(new_bearing - new_error);
            UpdateLog();
        }

        private void UpdateCompass(double deg)
        {
            if (IsQuit()) return;
            if (pbCompass.IsDisposed) return;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { _UpdateCompass(deg); }));
            }
            else
            {
                _UpdateCompass(deg);
            }
        }

        private void _UpdateCompass(double deg)
        {
            try
            {
                Bitmap b = DrawCompass(deg, 0, 360, 0, 360, pbCompass.Size);
                pbCompass.Image = b;

            }
            catch (Exception)
            {
            }
        }

        private void UpdateVORSD(double sd)
        {
            if (IsQuit()) return;
            if (tbVORSD.IsDisposed) return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { UpdateVORSD(sd); }));
            }
            else
            {
                tbVORSD.Text = sd.ToString();
            }
        }

        private void UpdateVORError(double deg)
        {
            if (IsQuit()) return;
            if (tbVORError.IsDisposed) return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { UpdateVORError(deg); }));
            }
            else
            {
                tbVORError.Text = deg.ToString();
            }
        }

        private void UpdateVORSignalLevel(double level)
        {
            if (IsQuit()) return;
            if (tbSignalLevel.IsDisposed) return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { UpdateVORSignalLevel(level); }));
            }
            else
            {
                double lvdb = 10 * Math.Log(level/1e-3);
                tbSignalLevel.Text = lvdb.ToString();
            }
        }

        private void UpdateVORBearing(double ang)
        {
            if (IsQuit()) return;
            if (tbVORAzimuth.IsDisposed) return;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { UpdateVORBearing(ang); }));
            }
            else
            {
                tbVORAzimuth.Text = ang.ToString();
            }
        }

        private void GNSSThread()
        {
            try
            {
                Print("GNSS Thread started\n");

                while (!IsQuit())
                {
                    Local_GNSS();
                    OnHaversineUpdate();
                    OnReceiveData();

                    System.Threading.Thread.Sleep(1);
                }


            }
            catch (Exception)
            { }
            finally
            {
                Print("GNSS Thread ended\n");
            }
        }

        private void UpdateLocation(string lat_v, string lat_NS, string long_v, string long_EW)
        {
            try
            {
                double i_lat, i_long;
                if (!double.TryParse(lat_v, out i_lat))
                    return;
                if (!double.TryParse(long_v, out i_long))
                    return;

                int m_lat = 1, m_long = 1;
                if (lat_NS.ToUpper().Trim() == "S")
                    m_lat = -1;
                if (long_EW.ToUpper().Trim() == "W")
                    m_long = -1;

                int lat_deg = (int)(i_lat / 100.0);
                double lat_min = i_lat - (lat_deg * 100.0);

                int long_deg = (int)(i_long / 100.0);
                double long_min = i_long - long_deg * 100.0;

                double v_lat = m_lat * (lat_deg * 1.0 + lat_min / 60.0);
                double v_long = m_long * (long_deg * 1.0 + long_min / 60.0);

                lock (AllSettings)
                {
                    double last_Lat = AllSettings.Lat;
                    double last_Long = AllSettings.Long;


                    AllSettings.Lat = Math.Abs(AllSettings.AverageFactor) * v_lat + Math.Abs(1.0 - AllSettings.AverageFactor) * AllSettings.Lat;
                    AllSettings.Long = Math.Abs(AllSettings.AverageFactor) * v_long + Math.Abs(1.0 - AllSettings.AverageFactor) * AllSettings.Long;


                    Print("Position Detected ({4}): {0}, {1} (Lat={2}, Long={3})\n",
                    v_lat, v_long, AllSettings.Lat, AllSettings.Long, DateTime.Now.ToString());
                }
                OnDisplayLocation();
            }
            catch (Exception)
            {

            }

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                try
                {
                    gnssThread.Priority = ThreadPriority.Highest;
                    Thread.Sleep(100);
                    bQuit = true;
                    Thread.Sleep(100);
                    gnssThread.Abort();
                    Thread.Sleep(100);
                    gnssThread = null;
                }
                catch (Exception) { }

                lock (AllSettings)
                {
                    Thread.Sleep(100);
                    AllSettings.Save();
                    Thread.Sleep(100);
                }


            }
            catch (Exception)
            { }
        }

        public void Print(string s, params object[] o)
        {
            try
            {
                string ss = string.Format(s, o);
                PrintString(ss);
            }
            catch (Exception)
            { }
        }
        public void PrintString(string s)
        {
            if (IsQuit())
                return;

            if (InvokeRequired)
            {
                Invoke(new Action(() => { PrintString(s); }));
            }
            else
            {
                lock (rtbOutput)
                {
                    try
                    {
                        rtbOutput.AppendText(s);
                        rtbOutput.SelectionStart = rtbOutput.MaxLength;
                        rtbOutput.ScrollToCaret();
                    }
                    catch (Exception)
                    { }
                }
            }
        }

        public void OnDisplayLocation()
        {
            if (IsQuit())
                return;

            if (InvokeRequired)
            {
                Invoke(new Action(() => { OnDisplayLocation(); }));
            }
            else
            {
                lock (AllSettings)
                {
                    lock (tbLat)
                    {
                        lock (tbLong)
                        {
                            try
                            {
                                tbLat.Text = AllSettings.Lat.ToString();
                                tbLong.Text = AllSettings.Long.ToString();

                            }
                            catch (Exception)
                            { }
                        }
                    }
                }
            }
        }

        private void tbVORLat_TextChanged(object sender, EventArgs e)
        {
            lock (AllSettings)
            {
                lock (tbVORLat)
                {
                    double r;
                    if (double.TryParse(tbVORLat.Text, out r))
                        AllSettings.VORLat = r;
                }
            }
        }

        private void tbVORLong_TextChanged(object sender, EventArgs e)
        {
            lock (AllSettings)
            {
                lock (tbVORLong)
                {
                    double r;
                    if (double.TryParse(tbVORLong.Text, out r))
                        AllSettings.VORLong = r;
                }
            }

        }

        private void tbVORFreq_TextChanged(object sender, EventArgs e)
        {
            lock (AllSettings)
            {
                lock (tbVORFreq)
                {
                    double r;
                    if (double.TryParse(tbVORFreq.Text, out r))
                        AllSettings.VORFrequency = r;
                }
            }
        }

        private void btnMapsCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                double c_lat, c_long;
                lock (AllSettings)
                {
                    c_lat = AllSettings.Lat;
                    c_long = AllSettings.Long;
                }
                string url = string.Format("http://maps.google.com/maps?q=loc:{0},{1}", c_lat, c_long);
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception)
            { }
        }

        private void btnVORMaps_Click(object sender, EventArgs e)
        {
            try
            {
                double c_lat, c_long;
                lock (AllSettings)
                {
                    c_lat = AllSettings.VORLat;
                    c_long = AllSettings.VORLong;
                }
                string url = string.Format("http://maps.google.com/maps?q=loc:{0},{1}", c_lat, c_long);
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception)
            { }

        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            bQuit = true;
        }

        // Update Based on GNSS
        private void OnHaversineUpdate()
        {
            if (IsQuit())
                return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { _OnHaversineUpdate(); }));
            }
            else
            {
                _OnHaversineUpdate();
            }
        }

        private void _OnHaversineUpdate()
        {
            double lat1, long1;
            double lat2, long2;

            lock (AllSettings)
            {
                lat1 = AllSettings.VORLat;
                long1 = AllSettings.VORLong;
                lat2 = AllSettings.Lat;
                long2 = AllSettings.Long;
            }

            Calculate(ToRadian(lat1), ToRadian(long1), ToRadian(lat2), ToRadian(long2));

        }
        private void Calculate(double s_lat, double s_long,
                               double e_lat, double e_long)
        {
            // Distance 
            double del_phi = e_lat - s_lat;
            double del_lambda = e_long - s_long;

            double a = Math.Pow(Math.Sin(del_phi / 2), 2) +
                Math.Cos(s_lat) * Math.Cos(e_lat) * Math.Pow(Math.Sin(del_lambda / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double R = 6371.0; // km
            double d = R * c;

            // distance.Text = d.ToString();

            // bearing
            double X = Math.Sin(del_lambda) * Math.Cos(e_lat);
            double Y = Math.Cos(s_lat) * Math.Sin(e_lat) - Math.Sin(s_lat) * Math.Cos(e_lat) * Math.Cos(del_lambda);
            double ang = ToDegree(Math.Atan2(X, Y));
            //azimuth.Text = ang.ToString();

            lock (AllSettings)
            {
                AllSettings.GNSSDistance = d;
                AllSettings.GNSSBearing = ang;

                lock (tbGNSSDistance)
                {
                    tbGNSSDistance.Text = AllSettings.GNSSDistance.ToString();
                }

                lock (tbGNSSBearing)
                {
                    tbGNSSBearing.Text = AllSettings.GNSSBearing.ToString();
                }
            }

        }
        private double ToRadian(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        private double ToDegree(double rad)
        {
            return rad * 180.0 / Math.PI;
        }

        private void UpdateLog()
        {
            if (IsQuit()) return;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { _UpdateLog(); }));
            }
            else
            {
                _UpdateLog();
            }
        }

        private void _UpdateLog()
        {
            if (IsQuit()) return;
            if (w == null) return;

            try
            {
                string c_lat = tbLat.Text;
                string c_long = tbLong.Text;
                string vor_lat = tbVORLat.Text;
                string vor_long = tbVORLong.Text;
                string vor_freq = tbVORFreq.Text;
                string gnss_distance = tbGNSSDistance.Text;
                string gnss_bearing = tbGNSSBearing.Text;
                string vor_azimuth = tbVORAzimuth.Text;
                string vor_signallevel = tbSignalLevel.Text;
                string vor_SD = tbVORSD.Text;
                string vor_error = tbVORError.Text;
                string vor_iscalibrate = cbCalibrate.Checked ? "Calibrate" : "No Calibrate";

                string csv_line = string.Format("{12},{13},{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                    c_lat, c_long,
                    vor_lat, vor_long, vor_freq,
                    gnss_distance, gnss_bearing,
                    vor_azimuth, vor_signallevel, vor_SD, vor_error, vor_iscalibrate,
                    DateTime.Now.ToLongDateString(),
                    DateTime.Now.ToLongTimeString());

                w.WriteLine(csv_line);
            }
            catch (Exception)
            { }

        }


        public static Bitmap DrawCompass(double degree, double pitch, double maxpitch, double tilt, double maxtilt, Size s)
        {


            double maxRadius = s.Width > s.Height ? s.Height / 2 : s.Width / 2;

            double sizeMultiplier = maxRadius / 200;
            double relativepitch = pitch / maxpitch;
            double relativetilt = tilt / maxtilt;

            Bitmap result = null;
            SolidBrush drawBrushWhite = new SolidBrush(Color.FromArgb(255, 244, 255));
            SolidBrush drawBrushRed = new SolidBrush(Color.FromArgb(240, 255, 0, 0));
            SolidBrush drawBrushOrange = new SolidBrush(Color.FromArgb(240, 255, 150, 0));
            SolidBrush drawBrushBlue = new SolidBrush(Color.FromArgb(100, 0, 250, 255));
            SolidBrush drawBrushWhiteGrey = new SolidBrush(Color.FromArgb(20, 255, 255, 255));
            double outerradius = (((maxRadius - sizeMultiplier * 60) / maxRadius) * maxRadius);
            double innerradius = (((maxRadius - sizeMultiplier * 90) / maxRadius) * maxRadius);
            double degreeRadius = outerradius + 37 * sizeMultiplier;
            double dirRadius = innerradius - 30 * sizeMultiplier;
            double TriRadius = outerradius + 20 * sizeMultiplier;
            double PitchTiltRadius = innerradius * 0.55;
            if (s.Width * s.Height > 0)
            {
                result = new Bitmap(s.Width, s.Height);
                using (Font font2 = new Font("Arial", (float)(16 * sizeMultiplier)))
                {
                    using (Font font1 = new Font("Arial", (float)(14 * sizeMultiplier)))
                    {
                        using (Pen penblue = new Pen(Color.FromArgb(100, 0, 250, 255), ((int)(sizeMultiplier) < 4 ? 4 : (int)(sizeMultiplier))))
                        {
                            using (Pen penorange = new Pen(Color.FromArgb(255, 150, 0), ((int)(sizeMultiplier) < 1 ? 1 : (int)(sizeMultiplier))))
                            {
                                using (Pen penred = new Pen(Color.FromArgb(255, 0, 0), ((int)(sizeMultiplier) < 1 ? 1 : (int)(sizeMultiplier))))
                                {

                                    using (Pen pen1 = new Pen(Color.FromArgb(255, 255, 255), (int)(sizeMultiplier * 4)))
                                    {

                                        using (Pen pen2 = new Pen(Color.FromArgb(255, 255, 255), ((int)(sizeMultiplier) < 1 ? 1 : (int)(sizeMultiplier))))
                                        {
                                            using (Pen pen3 = new Pen(Color.FromArgb(0, 255, 255, 255), ((int)(sizeMultiplier) < 1 ? 1 : (int)(sizeMultiplier))))
                                            {
                                                using (Graphics g = Graphics.FromImage(result))
                                                {

                                                    // Calculate some image information.
                                                    double sourcewidth = s.Width;
                                                    double sourceheight = s.Height;

                                                    int xcenterpoint = (int)(s.Width / 2);
                                                    int ycenterpoint = (int)((s.Height / 2));// maxRadius;

                                                    Point pA1 = new Point(xcenterpoint, ycenterpoint - (int)(sizeMultiplier * 45));
                                                    Point pB1 = new Point(xcenterpoint - (int)(sizeMultiplier * 7), ycenterpoint - (int)(sizeMultiplier * 45));
                                                    Point pC1 = new Point(xcenterpoint, ycenterpoint - (int)(sizeMultiplier * 90));
                                                    Point pB2 = new Point(xcenterpoint + (int)(sizeMultiplier * 7), ycenterpoint - (int)(sizeMultiplier * 45));

                                                    Point[] a2 = new Point[] { pA1, pB1, pC1 };
                                                    Point[] a3 = new Point[] { pA1, pB2, pC1 };

                                                    g.DrawPolygon(penred, a2);
                                                    g.FillPolygon(drawBrushRed, a2);
                                                    g.DrawPolygon(penred, a3);
                                                    g.FillPolygon(drawBrushWhite, a3);

                                                    double[] Cos = new double[360];
                                                    double[] Sin = new double[360];

                                                    //draw centercross
                                                    g.DrawLine(pen2, new Point(((int)(xcenterpoint - (PitchTiltRadius - sizeMultiplier * 50))), ycenterpoint), new Point(((int)(xcenterpoint + (PitchTiltRadius - sizeMultiplier * 50))), ycenterpoint));
                                                    g.DrawLine(pen2, new Point(xcenterpoint, (int)(ycenterpoint - (PitchTiltRadius - sizeMultiplier * 50))), new Point(xcenterpoint, ((int)(ycenterpoint + (PitchTiltRadius - sizeMultiplier * 50)))));

                                                    //draw pitchtiltcross
                                                    Point PitchTiltCenter = new Point((int)(xcenterpoint + PitchTiltRadius * relativetilt), (int)(ycenterpoint - PitchTiltRadius * relativepitch));
                                                    int rad = (int)(sizeMultiplier * 8);
                                                    int rad2 = (int)(sizeMultiplier * 25);

                                                    Rectangle r = new Rectangle((int)(PitchTiltCenter.X - rad2), (int)(PitchTiltCenter.Y - rad2), (int)(rad2 * 2), (int)(rad2 * 2));
                                                    g.DrawEllipse(pen3, r);
                                                    g.FillEllipse(drawBrushWhiteGrey, r);
                                                    g.DrawLine(penorange, PitchTiltCenter.X - rad, PitchTiltCenter.Y, PitchTiltCenter.X + rad, PitchTiltCenter.Y);
                                                    g.DrawLine(penorange, PitchTiltCenter.X, PitchTiltCenter.Y - rad, PitchTiltCenter.X, PitchTiltCenter.Y + rad);

                                                    //prep here because need before and after for red triangle.
                                                    for (int d = 0; d < 360; d++)
                                                    {
                                                        //   map[y] = new long[src.Width];
                                                        double angleInRadians = ((((double)d) + 270d) - degree) / 180F * Math.PI;
                                                        Cos[d] = Math.Cos(angleInRadians);
                                                        Sin[d] = Math.Sin(angleInRadians);
                                                    }

                                                    for (int d = 0; d < 360; d++)
                                                    {


                                                        Point p1 = new Point((int)(outerradius * Cos[d]) + xcenterpoint, (int)(outerradius * Sin[d]) + ycenterpoint);
                                                        Point p2 = new Point((int)(innerradius * Cos[d]) + xcenterpoint, (int)(innerradius * Sin[d]) + ycenterpoint);

                                                        //Draw Degree labels
                                                        if (d % 30 == 0)
                                                        {
                                                            g.DrawLine(penblue, p1, p2);

                                                            Point p3 = new Point((int)(degreeRadius * Cos[d]) + xcenterpoint, (int)(degreeRadius * Sin[d]) + ycenterpoint);
                                                            SizeF s1 = g.MeasureString(d.ToString(), font1);
                                                            p3.X = p3.X - (int)(s1.Width / 2);
                                                            p3.Y = p3.Y - (int)(s1.Height / 2);

                                                            g.DrawString(d.ToString(), font1, drawBrushWhite, p3);
                                                            Point pA = new Point((int)(TriRadius * Cos[d]) + xcenterpoint, (int)(TriRadius * Sin[d]) + ycenterpoint);

                                                            int width = (int)(sizeMultiplier * 3);
                                                            int dp = d + width > 359 ? d + width - 360 : d + width;
                                                            int dm = d - width < 0 ? d - width + 360 : d - width;

                                                            Point pB = new Point((int)((TriRadius - (15 * sizeMultiplier)) * Cos[dm]) + xcenterpoint, (int)((TriRadius - (15 * sizeMultiplier)) * Sin[dm]) + ycenterpoint);
                                                            Point pC = new Point((int)((TriRadius - (15 * sizeMultiplier)) * Cos[dp]) + xcenterpoint, (int)((TriRadius - (15 * sizeMultiplier)) * Sin[dp]) + ycenterpoint);

                                                            Pen p = penblue;
                                                            Brush b = drawBrushBlue;
                                                            if (d == 0)
                                                            {
                                                                p = penred;
                                                                b = drawBrushRed;
                                                            }
                                                            Point[] a = new Point[] { pA, pB, pC };

                                                            g.DrawPolygon(p, a);
                                                            g.FillPolygon(b, a);
                                                        }
                                                        else if (d % 2 == 0)
                                                            g.DrawLine(pen2, p1, p2);

                                                        //draw N,E,S,W
                                                        if (d % 90 == 0)
                                                        {
                                                            string dir = (d == 0 ? "N" : (d == 90 ? "E" : (d == 180 ? "S" : "W")));
                                                            Point p4 = new Point((int)(dirRadius * Cos[d]) + xcenterpoint, (int)(dirRadius * Sin[d]) + ycenterpoint);
                                                            SizeF s2 = g.MeasureString(dir, font1);
                                                            p4.X = p4.X - (int)(s2.Width / 2);
                                                            p4.Y = p4.Y - (int)(s2.Height / 2);

                                                            g.DrawString(dir, font1, d == 0 ? drawBrushRed : drawBrushBlue, p4);

                                                            //}
                                                            ////Draw red triangle at 0 degrees
                                                            //if (d == 0)
                                                            //{

                                                        }

                                                    }
                                                    //draw course

                                                    //g.DrawLine(pen1, new Point(xcenterpoint, ycenterpoint - (int)innerradius), new Point(xcenterpoint, ycenterpoint - ((int)outerradius + (int)(sizeMultiplier * 50))));


                                                    String deg = Math.Round(degree, 2).ToString("0.00") + "°";
                                                    SizeF s3 = g.MeasureString(deg, font1);

                                                    g.DrawString(deg, font2, drawBrushOrange, new Point(xcenterpoint - (int)(s3.Width / 2), ycenterpoint - (int)(sizeMultiplier * 40)));

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
