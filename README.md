# RTLVOR  
Real time reception and display of VOR using RTL-SDR, GNU-Radio and VORViewer  
[https://www.radiojitter.com/real-time-decoding-of-vor-using-rtl-sdr/](https://www.radiojitter.com/real-time-decoding-of-vor-using-rtl-sdr/)  
  

# File Contents  
* **\Realtime_RTLSDR_VOR\GNURadio_Arnav**  Contains the GNURadio Flow graph which is designed by ** Arnav Mukhopadhyay **  
* **\Realtime_RTLSDR_VOR\GNURadio_DL9CAT** Contains the updated and corrected GNURadio Flowgraph designed by the user ** DL9CAT ** which will automatically correct the 30 degree phase shift without GNSS adapter.  
* **\Realtime_RTLSDR_VOR\VORViewer**       Contains the VOR Viewer application source code which consumes data from GNURadio VOR Receiver and GNSS Receiver.  
* **\Realtime_RTLSDR_VOR\VORViewer_EXE**   Contains the application with settings file. Run the program name ** VORViewer.exe ** on Microsoft Windows platform, after running the GNURadio based VOR Receiver, to view the VOR compass.  
* **\Realtime_RTLSDR_VOR\VORViewer_EXE**   Contains 32-bit float IQ file downsampled to a sample rate of 32 KSPS for real time VOR signals.  

## [http://gudduarnav.com/](Arnav Mukhopadhyay)  
## [https://www.radiojitter.com/](RadioJitter Concepts Lab)  
## Bengaluru, India  
