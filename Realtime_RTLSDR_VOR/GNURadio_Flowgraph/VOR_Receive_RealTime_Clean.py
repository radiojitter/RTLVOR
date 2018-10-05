#!/usr/bin/env python2
# -*- coding: utf-8 -*-
##################################################
# GNU Radio Python Flow Graph
# Title: Vor Receive Realtime Clean
# Generated: Fri Oct  5 15:19:02 2018
##################################################

if __name__ == '__main__':
    import ctypes
    import sys
    if sys.platform.startswith('linux'):
        try:
            x11 = ctypes.cdll.LoadLibrary('libX11.so')
            x11.XInitThreads()
        except:
            print "Warning: failed to XInitThreads()"

from gnuradio import analog
from gnuradio import blocks
from gnuradio import eng_notation
from gnuradio import fft
from gnuradio import filter
from gnuradio import gr
from gnuradio import wxgui
from gnuradio.eng_option import eng_option
from gnuradio.filter import firdes
from gnuradio.filter import pfb
from gnuradio.wxgui import scopesink2
from grc_gnuradio import wxgui as grc_wxgui
from optparse import OptionParser
import osmosdr
import time
import wx


class VOR_Receive_RealTime_Clean(grc_wxgui.top_block_gui):

    def __init__(self):
        grc_wxgui.top_block_gui.__init__(self, title="Vor Receive Realtime Clean")
        _icon_path = "C:\GNURadio-3.7\share\icons\hicolor\scalable/apps\gnuradio-grc.png"
        self.SetIcon(wx.Icon(_icon_path, wx.BITMAP_TYPE_ANY))

        ##################################################
        # Variables
        ##################################################
        self.samp_rate = samp_rate = 32000
        self.rtl_gain = rtl_gain = 42
        self.rtl_freq = rtl_freq = 115.5e6+64
        self.PI = PI = 3.14159

        ##################################################
        # Blocks
        ##################################################
        self.wxgui_scopesink2_1_0_0 = scopesink2.scope_sink_f(
        	self.GetWin(),
        	title='Morse',
        	sample_rate=samp_rate/(1600),
        	v_scale=0,
        	v_offset=0,
        	t_scale=0,
        	ac_couple=False,
        	xy_mode=False,
        	num_inputs=1,
        	trig_mode=wxgui.TRIG_MODE_STRIPCHART,
        	y_axis_label='Amp',
        )
        self.Add(self.wxgui_scopesink2_1_0_0.win)
        self.rtlsdr_source_0 = osmosdr.source( args="numchan=" + str(1) + " " + '' )
        self.rtlsdr_source_0.set_sample_rate(samp_rate*64)
        self.rtlsdr_source_0.set_center_freq(rtl_freq, 0)
        self.rtlsdr_source_0.set_freq_corr(0, 0)
        self.rtlsdr_source_0.set_dc_offset_mode(0, 0)
        self.rtlsdr_source_0.set_iq_balance_mode(0, 0)
        self.rtlsdr_source_0.set_gain_mode(False, 0)
        self.rtlsdr_source_0.set_gain(rtl_gain, 0)
        self.rtlsdr_source_0.set_if_gain(20, 0)
        self.rtlsdr_source_0.set_bb_gain(20, 0)
        self.rtlsdr_source_0.set_antenna('', 0)
        self.rtlsdr_source_0.set_bandwidth(0, 0)

        self.pfb_decimator_ccf_0 = pfb.decimator_ccf(
        	  64,
        	  (firdes.low_pass(1,samp_rate*64,16e3,200e3)),
        	  0,
        	  100,
                  True,
                  True)
        self.pfb_decimator_ccf_0.declare_sample_delay(0)

        self.hilbert_fc_0 = filter.hilbert_fc(64, firdes.WIN_HAMMING, 6.76)
        self.goertzel_fc_0_0 = fft.goertzel_fc(samp_rate, 3200, 30)
        self.goertzel_fc_0 = fft.goertzel_fc(samp_rate, 3200, 30)
        self.freq_xlating_fft_filter_ccc_0 = filter.freq_xlating_fft_filter_ccc(1, (firdes.low_pass(1,samp_rate,480*2,500*2)), 9960, samp_rate)
        self.freq_xlating_fft_filter_ccc_0.set_nthreads(1)
        self.freq_xlating_fft_filter_ccc_0.declare_sample_delay(0)
        self.blocks_tcp_server_sink_0 = blocks.tcp_server_sink(gr.sizeof_gr_complex*1, '0.0.0.0', 20000, True)
        self.blocks_multiply_conjugate_cc_0 = blocks.multiply_conjugate_cc(1)
        self.blocks_integrate_xx_1 = blocks.integrate_ff(1600, 1)
        self.blocks_integrate_xx_0 = blocks.integrate_cc(8, 1)
        self.blocks_complex_to_mag_squared_0 = blocks.complex_to_mag_squared(1)
        self.blocks_complex_to_mag_0 = blocks.complex_to_mag(1)
        self.band_pass_filter_0 = filter.fir_filter_fcc(1, firdes.complex_band_pass(
        	100, samp_rate, 1015, 1022, 500, firdes.WIN_HAMMING, 6.76))
        self.analog_wfm_rcv_0 = analog.wfm_rcv(
        	quad_rate=samp_rate,
        	audio_decimation=1,
        )



        ##################################################
        # Connections
        ##################################################
        self.connect((self.analog_wfm_rcv_0, 0), (self.goertzel_fc_0_0, 0))
        self.connect((self.band_pass_filter_0, 0), (self.blocks_complex_to_mag_squared_0, 0))
        self.connect((self.blocks_complex_to_mag_0, 0), (self.band_pass_filter_0, 0))
        self.connect((self.blocks_complex_to_mag_0, 0), (self.goertzel_fc_0, 0))
        self.connect((self.blocks_complex_to_mag_0, 0), (self.hilbert_fc_0, 0))
        self.connect((self.blocks_complex_to_mag_squared_0, 0), (self.blocks_integrate_xx_1, 0))
        self.connect((self.blocks_integrate_xx_0, 0), (self.blocks_tcp_server_sink_0, 0))
        self.connect((self.blocks_integrate_xx_1, 0), (self.wxgui_scopesink2_1_0_0, 0))
        self.connect((self.blocks_multiply_conjugate_cc_0, 0), (self.blocks_integrate_xx_0, 0))
        self.connect((self.freq_xlating_fft_filter_ccc_0, 0), (self.analog_wfm_rcv_0, 0))
        self.connect((self.goertzel_fc_0, 0), (self.blocks_multiply_conjugate_cc_0, 0))
        self.connect((self.goertzel_fc_0_0, 0), (self.blocks_multiply_conjugate_cc_0, 1))
        self.connect((self.hilbert_fc_0, 0), (self.freq_xlating_fft_filter_ccc_0, 0))
        self.connect((self.pfb_decimator_ccf_0, 0), (self.blocks_complex_to_mag_0, 0))
        self.connect((self.rtlsdr_source_0, 0), (self.pfb_decimator_ccf_0, 0))

    def get_samp_rate(self):
        return self.samp_rate

    def set_samp_rate(self, samp_rate):
        self.samp_rate = samp_rate
        self.wxgui_scopesink2_1_0_0.set_sample_rate(self.samp_rate/(1600))
        self.rtlsdr_source_0.set_sample_rate(self.samp_rate*64)
        self.pfb_decimator_ccf_0.set_taps((firdes.low_pass(1,self.samp_rate*64,16e3,200e3)))
        self.goertzel_fc_0_0.set_rate(self.samp_rate)
        self.goertzel_fc_0.set_rate(self.samp_rate)
        self.freq_xlating_fft_filter_ccc_0.set_taps((firdes.low_pass(1,self.samp_rate,480*2,500*2)))
        self.band_pass_filter_0.set_taps(firdes.complex_band_pass(100, self.samp_rate, 1015, 1022, 500, firdes.WIN_HAMMING, 6.76))

    def get_rtl_gain(self):
        return self.rtl_gain

    def set_rtl_gain(self, rtl_gain):
        self.rtl_gain = rtl_gain
        self.rtlsdr_source_0.set_gain(self.rtl_gain, 0)

    def get_rtl_freq(self):
        return self.rtl_freq

    def set_rtl_freq(self, rtl_freq):
        self.rtl_freq = rtl_freq
        self.rtlsdr_source_0.set_center_freq(self.rtl_freq, 0)

    def get_PI(self):
        return self.PI

    def set_PI(self, PI):
        self.PI = PI


def main(top_block_cls=VOR_Receive_RealTime_Clean, options=None):

    tb = top_block_cls()
    tb.Start(True)
    tb.Wait()


if __name__ == '__main__':
    main()
