# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Set sw factor.
Modify the irradiation of mode area (W) by increasing or decreasing the solar adjustment factor.
If you use FullForcing do not use this settings.
-
EXPERT SETTINGS.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _sw_factor: Solar adjustment factor to apply. Connect a float in range (0.5, 1.50) [float].
        
    Returns:
        read_me: Message for users.
        solar_adjust: Solar adjust settings of *.simx file.
"""

ghenv.Component.Name = "Morpho SolarAdjust"
ghenv.Component.NickName = "morpho_solarAdjust"
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
    from Morpho25.Settings import SolarAdjust
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    if _sw_factor:
        
        solar_adjust = SolarAdjust(_sw_factor)
        
        return solar_adjust
    else:
        return

solar_adjust = main()
if not solar_adjust: print("Please, connect _sw_factor.")