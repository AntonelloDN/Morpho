# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Force boundary condition of air temperature and relative humidity.
-
You need to provide list of value to fit all simulation duration.
    Args:
        _air_temperature: List of temperature values to use as boundary condition (°C) [float].
        _relative_humidity: List of relative humidity values to use as boundary condition (°C) [float].

    Returns:
        read_me: Message for users.
        simple_forcing: Simple Forcing settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Simple Forcing"
ghenv.Component.NickName = "morpho_simple_forcing"
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
    clr.AddReferenceToFile("Morpho25.dll")
    from Morpho25.Settings import SimpleForcing

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    if _air_temperature and _relative_humidity:

        air_temperature = List[float](_air_temperature)
        relative_humidity = List[float](_relative_humidity)

        simple_forcing = SimpleForcing(air_temperature, relative_humidity)

        return simple_forcing
    else:
        return

simple_forcing = main()
if not simple_forcing: print("Please, connect _air_temperature and _relative_humidity.")
