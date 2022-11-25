# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Set plant settings such as tree calendar and CO2.
-
EXPERT SETTINGS.
    Args:
        _CO2: CO2 background level (ppm).
        -
        The default value of the CO2 background concentration is set to 400 ppm without this settings.
        _leaf_transmittance: Set it to 'True' to use old calculation model. 'False' to use user defined value.
        _tree_calendar: Set it to 'True' to use tree calendar, 'False' to disable tree calendar.
        -
        Tree calendar = foliage of your tree will be calculated depending on the month your simulation is set and the hemisphere where location is.
        
    Returns:
        read_me: Message for users.
        plant_settings: Plant calculation model to use in *.simx file.
"""

ghenv.Component.Name = "Morpho Tree Settings"
ghenv.Component.NickName = "morpho_tree_settings"
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
    from Morpho25.Settings import Active, PlantSetting
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    if _CO2 > 0 and _leaf_transmittance != None and _tree_calendar != None:
        
        transmittance = Active.YES if _leaf_transmittance else Active.NO
        calendar = Active.YES if _tree_calendar else Active.NO
        
        plant_settings = PlantSetting(transmittance, calendar, _CO2)
        
        return plant_settings
    else:
        return

plant_settings = main()
if not plant_settings: print("Please, connect _CO2, _leaf_transmittance, _tree_calendar.")