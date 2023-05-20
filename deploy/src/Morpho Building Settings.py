# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2023, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Set indoor building temperature to use for simulation. Unit is °C.
    Args:
        _indoor_temperature: Indoor temperature to apply to buildings (°C) [float].
        _constant_temperature: Set it to 'True' to use constant temperature or 'False' to keep it variable.
        _surface_temperature: Indoor surface temperature to apply to buildings (°C) [float].
        _air_conditioning: Set it to 'True' to use air conditioning or 'False' to keep it variable.
        
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
ghenv.Component.Message = "1.1.1"

def main():
    building_settings = BuildingSettings()
    
    indoor_const = Active.YES if _constant_temperature else Active.NO
    air_cond = Active.YES if _air_conditioning else Active.NO
    building_settings.AirCondHeat = air_cond
    building_settings.IndoorConst = indoor_const
    
    if _indoor_temperature: building_settings.IndoorTemp = _indoor_temperature
    if _surface_temperature: building_settings.SurfaceTemp = _surface_temperature
    
    return building_settings

building_settings = main()