# VHF omnidirectional range (VOR)

Aircraft navigation system. 

https://en.wikipedia.org/wiki/VHF_omnidirectional_range

## Flow graphs for gnuradio-companion:

* cvor_sim.grc
  * Simulate signals of conventional VOR (CVOR).
* dvor_sim.grc
  * Simulate signals of doppler VOR (DVOR).
* VOR-complete.grc
  * Receiver for VOR signals. Improved version out of http://www.housedillon.com/?p=2152
* VOR_RealTime_Clean.grc
  * Receiver for VOR signals. Improved version out of https://www.radiojitter.com/real-time-decoding-of-vor-using-rtl-sdr/
* phase_detect_hier.grc
  * Simple phase detection as hier block. Run once in gnuradio-companion and reload blocks to make this available.

## Other dependencies

Variable delay block from gr-baz (https://github.com/balint256/gr-baz) is
used in both simulation flow graphs. So gr-baz has to be installed before
use.