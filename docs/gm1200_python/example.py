#!/bin/env python3

import sb9600
from gm1200 import *

bus = sb9600.Serial("/dev/ttyUSB0")
gm1200 = GM1200(bus)

gm1200.CSQ()
bus.wait_for_quiet()

gm1200.Lamp("L2RED", LAMP_FLASH)
gm1200.Lamp("L8", LAMP_ON)

gm1200.Illumination(ILLUM_DISPLAY, 0xd4)
gm1200.Illumination(ILLUM_BUTTONS, 0xd4)

gm1200.Display(" !! EXAMPLE !!")

gm1200.SetRXFrequency(433.5) # MHz
gm1200.SetTXFrequency(433.5) # MHz

