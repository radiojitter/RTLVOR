#!/usr/bin/env python2
# -*- coding: utf-8 -*-
##################################################
# GNU Radio Python Flow Graph
# Title: Vor Receive Realtime Clean
# Generated: Wed Oct 24 17:47:06 2018
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
from gnuradio import audio
from gnuradio import blocks
from gnuradio import eng_notation
from gnuradio import fft
from gnuradio import filter
from gnuradio import gr
from gnuradio import iio
from gnuradio import wxgui
from gnuradio.eng_option import eng_option
from gnuradio.filter import firdes
from gnuradio.wxgui import forms
from gnuradio.wxgui import numbersink2
from gnuradio.wxgui import scopesink2
from grc_gnuradio import wxgui as grc_wxgui
from math import pi
from optparse import OptionParser
import threading
import time
import wx


class VOR_Receive_RealTime_Clean(grc_wxgui.top_block_gui):

    def __init__(self):
        grc_wxgui.top_block_gui.__init__(self, title="Vor Receive Realtime Clean")
        _icon_path = "/usr/share/icons/hicolor/32x32/apps/gnuradio-grc.png"
        self.SetIcon(wx.Icon(_icon_path, wx.BITMAP_TYPE_ANY))

        ##################################################
        # Variables
        ##################################################
        self.morse_vol = morse_vol = 10
        self.samp_rate = samp_rate = 32000
        self.rtl_gain = rtl_gain = 42
        self.rtl_freq = rtl_freq = 115.5e6+64
        self.morse_gain = morse_gain = pow(10,morse_vol/10)
        self.morse_amp_level = morse_amp_level = 0

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
        self.wxgui_scopesink2_0 = scopesink2.scope_sink_f(
        	self.GetWin(),
        	title='Scope Plot',
        	sample_rate=samp_rate,
        	v_scale=0,
        	v_offset=0,
        	t_scale=0.02,
        	ac_couple=False,
        	xy_mode=False,
        	num_inputs=2,
        	trig_mode=wxgui.TRIG_MODE_AUTO,
        	y_axis_label='Counts',
        )
        self.Add(self.wxgui_scopesink2_0.win)
        self.wxgui_numbersink2_1 = numbersink2.number_sink_f(
        	self.GetWin(),
        	unit='Units',
        	minval=0,
        	maxval=360,
        	factor=1.0,
        	decimal_places=10,
        	ref_level=0,
        	sample_rate=1,
        	number_rate=15,
        	average=False,
        	avg_alpha=None,
        	label='Number Plot',
        	peak_hold=False,
        	show_gauge=False,
        )
        self.Add(self.wxgui_numbersink2_1.win)
        _morse_vol_sizer = wx.BoxSizer(wx.VERTICAL)
        self._morse_vol_text_box = forms.text_box(
        	parent=self.GetWin(),
        	sizer=_morse_vol_sizer,
        	value=self.morse_vol,
        	callback=self.set_morse_vol,
        	label='Morse Volume (dB):',
        	converter=forms.float_converter(),
        	proportion=0,
        )
        self._morse_vol_slider = forms.slider(
        	parent=self.GetWin(),
        	sizer=_morse_vol_sizer,
        	value=self.morse_vol,
        	callback=self.set_morse_vol,
        	minimum=-25,
        	maximum=25,
        	num_steps=1000,
        	style=wx.SL_HORIZONTAL,
        	cast=float,
        	proportion=1,
        )
        self.Add(_morse_vol_sizer)

        def _morse_amp_level_probe():
            while True:
                val = self.morse_amp.level()
                try:
                    self.set_morse_amp_level(val)
                except AttributeError:
                    pass
                time.sleep(1.0 / (25))
        _morse_amp_level_thread = threading.Thread(target=_morse_amp_level_probe)
        _morse_amp_level_thread.daemon = True
        _morse_amp_level_thread.start()

        self.low_pass_filter_0_1_0 = filter.fir_filter_fff(1, firdes.low_pass(
        	1, samp_rate, 1000, 500, firdes.WIN_HAMMING, 6.76))
        self.low_pass_filter_0_1 = filter.fir_filter_ccf(1, firdes.low_pass(
        	1, samp_rate, 1000, 500, firdes.WIN_HAMMING, 6.76))
        self.iio_modulo_const_ff_0 = iio.modulo_const_ff(360, 1)
        self.goertzel_fc_0_0 = fft.goertzel_fc(samp_rate, 3200, 30)
        self.goertzel_fc_0 = fft.goertzel_fc(samp_rate, 3200, 30)
        self.fir_filter_xxx_0_0 = filter.fir_filter_fff(1, (filter.optfir.low_pass(1, samp_rate, 100, 200, 0.1,  60)))
        self.fir_filter_xxx_0_0.declare_sample_delay(0)
        self.blocks_throttle_0 = blocks.throttle(gr.sizeof_gr_complex*1, samp_rate,True)
        self.blocks_multiply_xx_0 = blocks.multiply_vcc(1)
        self.blocks_multiply_const_vxx_1 = blocks.multiply_const_vff((-180/pi, ))
        self.blocks_multiply_const_vxx_0 = blocks.multiply_const_vff((morse_gain, ))
        self.blocks_multiply_conjugate_cc_0 = blocks.multiply_conjugate_cc(1)
        self.blocks_integrate_xx_1 = blocks.integrate_ff(1600, 1)
        self.blocks_float_to_complex_0 = blocks.float_to_complex(1)
        self.blocks_file_source_0_0_0 = blocks.file_source(gr.sizeof_gr_complex*1, '/tmp/vor_simulation.cfile', False)
        self.blocks_complex_to_mag_squared_0 = blocks.complex_to_mag_squared(1)
        self.blocks_complex_to_arg_0 = blocks.complex_to_arg(1)
        self.blocks_add_const_vxx_1 = blocks.add_const_vff((360, ))
        self.band_pass_filter_0 = filter.fir_filter_fcc(1, firdes.complex_band_pass(
        	1, samp_rate, 1010, 1030, 100, firdes.WIN_HAMMING, 6.76))
        self.audio_sink_0_0 = audio.sink(samp_rate, '', True)
        self.analog_sig_source_x_0 = analog.sig_source_c(samp_rate, analog.GR_COS_WAVE, -9960, 1, 0)
        self.analog_fm_demod_cf_0 = analog.fm_demod_cf(
        	channel_rate=samp_rate,
        	audio_decim=1,
        	deviation=1000,
        	audio_pass=100,
        	audio_stop=200,
        	gain=1.0,
        	tau=75e-6,
        )
        self.analog_am_demod_cf_0 = analog.am_demod_cf(
        	channel_rate=samp_rate,
        	audio_decim=1,
        	audio_pass=12000,
        	audio_stop=13000,
        )

        ##################################################
        # Connections
        ##################################################
        self.connect((self.analog_am_demod_cf_0, 0), (self.band_pass_filter_0, 0))
        self.connect((self.analog_am_demod_cf_0, 0), (self.blocks_float_to_complex_0, 0))
        self.connect((self.analog_am_demod_cf_0, 0), (self.low_pass_filter_0_1_0, 0))
        self.connect((self.analog_fm_demod_cf_0, 0), (self.goertzel_fc_0_0, 0))
        self.connect((self.analog_fm_demod_cf_0, 0), (self.wxgui_scopesink2_0, 1))
        self.connect((self.analog_sig_source_x_0, 0), (self.blocks_multiply_xx_0, 1))
        self.connect((self.band_pass_filter_0, 0), (self.blocks_complex_to_mag_squared_0, 0))
        self.connect((self.blocks_add_const_vxx_1, 0), (self.iio_modulo_const_ff_0, 0))
        self.connect((self.blocks_complex_to_arg_0, 0), (self.blocks_multiply_const_vxx_1, 0))
        self.connect((self.blocks_complex_to_mag_squared_0, 0), (self.blocks_integrate_xx_1, 0))
        self.connect((self.blocks_complex_to_mag_squared_0, 0), (self.blocks_multiply_const_vxx_0, 0))
        self.connect((self.blocks_file_source_0_0_0, 0), (self.blocks_throttle_0, 0))
        self.connect((self.blocks_float_to_complex_0, 0), (self.blocks_multiply_xx_0, 0))
        self.connect((self.blocks_integrate_xx_1, 0), (self.wxgui_scopesink2_1_0_0, 0))
        self.connect((self.blocks_multiply_conjugate_cc_0, 0), (self.blocks_complex_to_arg_0, 0))
        self.connect((self.blocks_multiply_const_vxx_0, 0), (self.audio_sink_0_0, 0))
        self.connect((self.blocks_multiply_const_vxx_1, 0), (self.blocks_add_const_vxx_1, 0))
        self.connect((self.blocks_multiply_xx_0, 0), (self.low_pass_filter_0_1, 0))
        self.connect((self.blocks_throttle_0, 0), (self.analog_am_demod_cf_0, 0))
        self.connect((self.fir_filter_xxx_0_0, 0), (self.goertzel_fc_0, 0))
        self.connect((self.fir_filter_xxx_0_0, 0), (self.wxgui_scopesink2_0, 0))
        self.connect((self.goertzel_fc_0, 0), (self.blocks_multiply_conjugate_cc_0, 0))
        self.connect((self.goertzel_fc_0_0, 0), (self.blocks_multiply_conjugate_cc_0, 1))
        self.connect((self.iio_modulo_const_ff_0, 0), (self.wxgui_numbersink2_1, 0))
        self.connect((self.low_pass_filter_0_1, 0), (self.analog_fm_demod_cf_0, 0))
        self.connect((self.low_pass_filter_0_1_0, 0), (self.fir_filter_xxx_0_0, 0))

    def get_morse_vol(self):
        return self.morse_vol

    def set_morse_vol(self, morse_vol):
        self.morse_vol = morse_vol
        self.set_morse_gain(pow(10,self.morse_vol/10))
        self._morse_vol_slider.set_value(self.morse_vol)
        self._morse_vol_text_box.set_value(self.morse_vol)

    def get_samp_rate(self):
        return self.samp_rate

    def set_samp_rate(self, samp_rate):
        self.samp_rate = samp_rate
        self.wxgui_scopesink2_1_0_0.set_sample_rate(self.samp_rate/(1600))
        self.wxgui_scopesink2_0.set_sample_rate(self.samp_rate)
        self.low_pass_filter_0_1_0.set_taps(firdes.low_pass(1, self.samp_rate, 1000, 500, firdes.WIN_HAMMING, 6.76))
        self.low_pass_filter_0_1.set_taps(firdes.low_pass(1, self.samp_rate, 1000, 500, firdes.WIN_HAMMING, 6.76))
        self.goertzel_fc_0_0.set_rate(self.samp_rate)
        self.goertzel_fc_0.set_rate(self.samp_rate)
        self.fir_filter_xxx_0_0.set_taps((filter.optfir.low_pass(1, self.samp_rate, 100, 200, 0.1,  60)))
        self.blocks_throttle_0.set_sample_rate(self.samp_rate)
        self.band_pass_filter_0.set_taps(firdes.complex_band_pass(1, self.samp_rate, 1010, 1030, 100, firdes.WIN_HAMMING, 6.76))
        self.analog_sig_source_x_0.set_sampling_freq(self.samp_rate)

    def get_rtl_gain(self):
        return self.rtl_gain

    def set_rtl_gain(self, rtl_gain):
        self.rtl_gain = rtl_gain

    def get_rtl_freq(self):
        return self.rtl_freq

    def set_rtl_freq(self, rtl_freq):
        self.rtl_freq = rtl_freq

    def get_morse_gain(self):
        return self.morse_gain

    def set_morse_gain(self, morse_gain):
        self.morse_gain = morse_gain
        self.blocks_multiply_const_vxx_0.set_k((self.morse_gain, ))

    def get_morse_amp_level(self):
        return self.morse_amp_level

    def set_morse_amp_level(self, morse_amp_level):
        self.morse_amp_level = morse_amp_level


def main(top_block_cls=VOR_Receive_RealTime_Clean, options=None):

    tb = top_block_cls()
    tb.Start(True)
    tb.Wait()


if __name__ == '__main__':
    main()
