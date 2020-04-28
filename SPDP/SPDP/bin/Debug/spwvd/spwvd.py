import numpy as np
from tftb.processing.linear import ShortTimeFourierTransform
from tftb.processing.base import BaseTFRepresentation


def smoothed_pseudo_wigner_ville(signal, timestamps=None, freq_bins=None,
                                 twindow=None, fwindow=None):
    """Smoothed Pseudo Wigner-Ville time-frequency distribution.
    :param signal: signal to be analyzed
    :param timestamps: time instants of the signal
    :param freq_bins: number of frequency bins
    :param twindow: time smoothing window
    :param fwindow: frequency smoothing window
    :type signal: array-like
    :type timestamps: array-like
    :type freq_bins: int
    :type twindow: array-like
    :type fwindow: array-like
    :return: Smoothed pseudo Wigner Ville distribution
    :rtype: array-like
    """
    if timestamps is None:
        timestamps = np.arange(signal.shape[0])
    if freq_bins is None:
        freq_bins = signal.shape[0]

    if fwindow is None:
        winlength = np.floor(freq_bins / 4.0)
        winlength = winlength + 1 - np.remainder(winlength, 2)
        from scipy.signal import hamming
        fwindow = hamming(int(winlength))
    elif fwindow.shape[0] % 2 == 0:
        raise ValueError('The smoothing fwindow must have an odd length.')

    if twindow is None:
        timelength = np.floor(freq_bins / 10.0)
        timelength += 1 - np.remainder(timelength, 2)
        from scipy.signal import hamming
        twindow = hamming(int(timelength))
    elif twindow.shape[0] % 2 == 0:
        raise ValueError('The smoothing fwindow must have an odd length.')

    tfr = np.zeros((freq_bins, timestamps.shape[0]), dtype=complex)
    lg = (twindow.shape[0] - 1) // 2
    lh = (fwindow.shape[0] - 1) // 2
    for icol in range(timestamps.shape[0]):
        ti = timestamps[icol]
        taumax = min([ti + lg - 1, signal.shape[0] - ti + lg,
                      np.round(freq_bins / 2.0) - 1, lh])
        points = np.arange(-min([lg, signal.shape[0] - ti]),
                           min([lg, ti - 1]) + 1)
        g2 = twindow[lg + points]
        g2 = g2 / np.sum(g2)
        tfr[0, icol] = np.sum(g2 * signal[ti - points - 1] * np.conj(signal[ti - points - 1]))
        for tau in range(int(taumax)):
            points = np.arange(-min([lg, signal.shape[0] - ti - tau]),
                               min([lg, ti - 1 - tau]) + 1)
            g2 = twindow[lg + points]
            g2 = g2 / np.sum(g2)
            R = np.sum(g2 * signal[ti + tau - points - 1] * np.conj(signal[ti - tau - points - 1]))
            tfr[1 + tau, icol] = fwindow[lh + tau + 1] * R
            R = np.sum(g2 * signal[ti - tau - points - 1] * np.conj(signal[ti + tau - points - 1]))
            tfr[freq_bins - tau - 1, icol] = fwindow[lh - tau + 1] * R
        tau = np.round(freq_bins / 2.0)
        if (ti <= signal.shape[0] - tau) and (ti >= tau + 1) and (tau <= lh):
            points = np.arange(-min([lg, signal.shape[0] - ti - tau]),
                               min([lg, ti - 1 - tau]) + 1)
            g2 = twindow[lg + 1 + points]
            g2 = g2 / np.sum(g2)
            _x = np.sum(g2 * signal[ti + tau - points] * np.conj(signal[ti - tau - points]))
            _x *= fwindow[lh + tau + 1]
            _y = np.sum(g2 * signal[ti - tau - points] * np.conj(signal[ti + tau - points]))
            _y *= fwindow[lh - tau + 1]
            tfr[tau, icol] = (_x + _y) * 0.5
    tfr = np.fft.fft(tfr, axis=0)
    return np.real(tfr)