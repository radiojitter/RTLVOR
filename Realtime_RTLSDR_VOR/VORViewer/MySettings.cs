using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VORViewer
{
    class MySettings
    {
        public string COMPort;
        public int BAUDRate;
        public double AverageFactor;

        public double VORLat;
        public double VORLong;
        public double VORFrequency;

        public double Lat;
        public double Long;

        public double GNSSDistance;
        public double GNSSBearing;

        public string TcpIPAddress;
        public int TcpPort;

        public int nPointAverage;
        public double VORAverageFactor;

        public double VORBearing;
        public double VORError;
        public double VORSignalLevel;

        public double CalibrateSD;


        private IniFile ini;



        public MySettings()
        {
            ini = new IniFile();
            COMPort = "COM1,COM2,COM3,COM4,COM5,COM6,COM7,COM7,COM8,COM8,COM10,COM11,COM12";
            BAUDRate = 115200;
            AverageFactor = 0.6;

            VORLat = 0;
            VORLong = 0;
            VORFrequency = 0;

            Lat = 0;
            Long = 0;

            GNSSDistance = 0;
            GNSSBearing = 0;

            TcpIPAddress = "127.0.0.1";
            TcpPort = 20000;

            nPointAverage = 16;
            VORAverageFactor = 0.8;

            VORBearing = 0.0;
            VORError = 0.0;
            VORSignalLevel = 0.0;

            CalibrateSD = 5.0;

            Load();
        }

        private void Load()
        {
            string string_COMPort = ini.Read("SkyTracPort");
            string string_BAUDRate = ini.Read("SkyTracBaud");
            string string_AverageFactor = ini.Read("AverageFactor");
            string string_VORLatitude = ini.Read("VORLatitude");
            string string_VORLongitude = ini.Read("VORLongitude");
            string string_VORFrequency = ini.Read("VORFrequency");
            string string_Lat = ini.Read("Latitude");
            string string_Long = ini.Read("Longitude");
            string string_GNSSDistance = ini.Read("GNSSDistance");
            string string_GNSSBearing = ini.Read("GNSSBearing");
            string string_TcpIPAddress = ini.Read("VORIP");
            string string_TcpPort = ini.Read("VORPort");
            string string_nPointAverage = ini.Read("nPointAverage");
            string string_VORAverageFactor = ini.Read("VORAverageFactor");
            string string_VORBearing = ini.Read("VORBearing");
            string string_VORError = ini.Read("VORError");
            string string_VORSignalLevel = ini.Read("VORSignalLevel");
            string string_CalibrateSD = ini.Read("CalibrateSD");

             if (string_COMPort.Trim().Length >= 4)
                  COMPort = string_COMPort.Trim();


            int r_BAUDRate;
            if (int.TryParse(string_BAUDRate, out r_BAUDRate))
                BAUDRate = r_BAUDRate;

            int r_AverageFactor;
            if (int.TryParse(string_AverageFactor, out r_AverageFactor))
                AverageFactor = r_AverageFactor;

            double r_VORLat;
            if (double.TryParse(string_VORLatitude, out r_VORLat))
                VORLat = r_VORLat;

            double r_VORLong;
            if (double.TryParse(string_VORLongitude, out r_VORLong))
                VORLong = r_VORLong;

            double r_VORFrequency;
            if (double.TryParse(string_VORFrequency, out r_VORFrequency))
                VORFrequency = r_VORFrequency;


            double r_Lat;
            double r_Long;
            if (double.TryParse(string_Lat, out r_Lat))
                Lat = r_Lat;

            if (double.TryParse(string_Long, out r_Long))
                Long = r_Long;


            double r_GNSSDistance, r_GNSSBearing;
            if (double.TryParse(string_GNSSDistance, out r_GNSSDistance))
                GNSSDistance = r_GNSSDistance;

            if (double.TryParse(string_GNSSBearing, out r_GNSSBearing))
                GNSSBearing = r_GNSSBearing;

            if (string_TcpIPAddress.Trim().Length < 0)
                TcpIPAddress = string_TcpIPAddress;

            int r_TcpPort;
            if (int.TryParse(string_TcpPort, out r_TcpPort))
                TcpPort = r_TcpPort;

            int r_nPointAverage;
            if (int.TryParse(string_nPointAverage, out r_nPointAverage))
                nPointAverage = r_nPointAverage;

            double r_VORAverageFactor;
            if (double.TryParse(string_VORAverageFactor, out r_VORAverageFactor))
                VORAverageFactor = r_VORAverageFactor;


            double r_VORBearing, r_VORError;
            if (double.TryParse(string_VORBearing, out r_VORBearing))
                VORBearing = r_VORBearing;
            if (double.TryParse(string_VORError, out r_VORError))
                VORError = r_VORError;

            double r_VORSignalLevel;
            if (double.TryParse(string_VORSignalLevel, out r_VORSignalLevel))
                VORSignalLevel = r_VORSignalLevel;

            double r_CalibrateSD;
            if (double.TryParse(string_CalibrateSD, out r_CalibrateSD))
                CalibrateSD = r_CalibrateSD;

        }

        public void Save()
        {
            ini.Write("SkyTracPort", COMPort);
            ini.Write("SkyTracBaud", BAUDRate.ToString());
            ini.Write("AverageFactor", AverageFactor.ToString());
            ini.Write("VORLatitude", VORLat.ToString());
            ini.Write("VORLongitude", VORLong.ToString());
            ini.Write("VORFrequency", VORFrequency.ToString());
            ini.Write("Latitude", Lat.ToString());
            ini.Write("Longitude", Long.ToString());
            ini.Write("GNSSDistance", GNSSDistance.ToString());
            ini.Write("GNSSBearing", GNSSBearing.ToString());

            ini.Write("VORIP", TcpIPAddress);
            ini.Write("VORPort", TcpPort.ToString());
            ini.Write("nPointAverage", nPointAverage.ToString());
            ini.Write("VORAverageFactor", VORAverageFactor.ToString());

            ini.Write("VORBearing", VORBearing.ToString());
            ini.Write("VORError", VORError.ToString());
            ini.Write("VORSignalLevel", VORSignalLevel.ToString());

            ini.Write("CaliubrateSD", CalibrateSD.ToString());
        }
    }
}
