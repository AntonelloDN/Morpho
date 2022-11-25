# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Set indoor building temperature to use for simulation. Unit is °C.
    Args:
        _indoor_temperature: Indoor temperature to apply to buildings (°C) [float].
        _constant_temperature: Set it to 'True' to use constant temperature or 'False' to keep it variable.

    Returns:
        read_me: Message for users.
        building_settings: Building settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Building Settings"
ghenv.Component.NickName = "morpho_building_settings"
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
    from Morpho25.Settings import BuildingSettings, Active

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    if _indoor_temperature:

        constant = Active.YES if _constant_temperature else Active.NO

        building_settings = BuildingSettings(_indoor_temperature, constant)

        return building_settings
    else:
        return

building_settings = main()
if not building_settings: print("Please, connect _indoor_temperature.")
