# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Set the pollutant concentration.
Similar to the CO2 background concentration in the atmosphere (400 ppm), there might be a specific pollutant background concentration for your simulated area.
-
EXPERT SETTINGS.
    Args:
        _NO_: NO concentration in ppm [float].
        _NO2_: NO2 concentration in ppm [float].
        _ozone_: Ozone concentration in ppm [float].
        _PM10_: PM 10 concentration in ppm [float].
        _PM25_: PM 2.5 concentration in ppm [float].
        _user_pollutant_: User defined pollutant concentration in ppm [float].

    Returns:
        read_me: Message for users.
        pollutant_concentration: Pollutant concentration settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Pollutant Concentration"
ghenv.Component.NickName = "morpho_pollutant_concentration"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "4 || Simulation"
try: ghenv.Component.AdditionalHelpFromDocStrings = "2"
except: pass

import scriptcontext as sc
import os
import sys
import clr
##################Envimet INX####################
try:
    user_path = os.getenv("APPDATA")
    sys.path.append(os.path.join(user_path, "Morpho"))
    clr.AddReferenceToFile("Morpho25.dll")
    from Morpho25.Settings import Background

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    pollutant_concentration = Background()

    if _user_pollutant_: pollutant_concentration.UserSpec = _user_pollutant_
    if _NO_: pollutant_concentration.No = _NO_
    if _NO2_: pollutant_concentration.No2 = _NO2_
    if _ozone_: pollutant_concentration.O3 = _ozone_
    if _PM10_: pollutant_concentration.Pm10 = _PM10_
    if _PM25_: pollutant_concentration.Pm25 = _PM25_

    return pollutant_concentration

pollutant_concentration = main()
