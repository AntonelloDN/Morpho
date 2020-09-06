# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
This component calculate specific humidity of the atmosphere. Specific humidity at 2500 is lower if temperature is lower and higher when temperature is higher.
Old ENVI-Met versions used 7 g/kg as value because is a mean common value compatible with almost all climates.
-
NOTE: It is an important parameter, use it to have accurate results.
-
Thanks to ENVI-Met team for the algorithm.
-
Icons made by <a href="https://icon54.com/" title="Pixel perfect">Pixel perfect</a> from <a href="https://www.flaticon.com/" title="Flaticon"> www.flaticon.com</a>
    Args:
        _air_temperature: List of temperature values to use as boundary condition (°C) [float].
        _relative_humidity: List of relative humidity values to use as boundary condition (°C) [float].
        
    Returns:
        read_me: Message for users.
        specific_humidity: Specific humidity (g Water/kg air) at 2500m that envimet uses during the simulation. Connect it to "Morpho Main Settings".
"""

ghenv.Component.Name = "Morpho Atmosphere Specific Humidity"
ghenv.Component.NickName = "morpho_atmosphere_specific_humidity"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "4 || Simulation"
try: ghenv.Component.AdditionalHelpFromDocStrings = "1"
except: pass

import scriptcontext as sc
import os
import sys
import clr
##################Envimet INX####################
from System.Collections.Generic import *
try:
    user_path = os.getenv("APPDATA")
    sys.path.append(os.path.join(user_path, "Morpho"))
    clr.AddReference("Morpho25.dll")
    from Morpho25.Utility import EnvimetUtility, Util
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    if _air_temperature and _relative_humidity:
        
        air_temperature = List[float](_air_temperature)
        relative_humidity = List[float](_relative_humidity)
        
        specific_humidity = EnvimetUtility.GetAtmosphereSpecificHumidity(air_temperature, relative_humidity)
        
        return specific_humidity
    else:
        return

specific_humidity = main()
if not specific_humidity: print("Please, connect _air_temperature and _relative_humidity.")