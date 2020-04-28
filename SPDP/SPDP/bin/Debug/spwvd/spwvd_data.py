# coding=utf-8

import numpy as np
import scipy.io as scio
from spwvd import smoothed_pseudo_wigner_ville
from PIL import Image
import sys
import os

arguments = sys.argv
path = arguments[1]
picsavepath = arguments[2]
M = int(arguments[3])
if not os.path.exists(picsavepath):
    os.makedirs(picsavepath)

data = scio.loadmat(path)['data']
spwdf = smoothed_pseudo_wigner_ville(data[M], timestamps=np.arange(256), freq_bins=256)
spwdf = (spwdf-spwdf.min())/(spwdf.max()-spwdf.min())*255
spwdf = spwdf.astype('uint8')
spwdf = Image.fromarray(spwdf)
spwdf.save(picsavepath+path.split('/')[-1].split('.')[0]+'-'+str(M)+'.png',quality=95)
