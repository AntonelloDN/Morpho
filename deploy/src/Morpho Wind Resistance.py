# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Set wind resistance model at facede.
DIN 6946 includes a higher Z0 value.
-
EXPERT SETTINGS.
    Args:
        _model_type: Connect an integer to select wind resistance model.
        0 = MO
        1 = DIN6946
        
    Returns:
        read_me: Message for users.
        wind_resistance: Wind resistance model to use in *.simx file.
"""

ghenv.Component.Name = "Morpho Wind Resistance"
ghenv.Component.NickName = "morpho_wind_resistance"
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
    from Morpho25.Settings import FacadeMod, Facades
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    model = {
        0:FacadeMod.MO,
        1:FacadeMod.DIN6946
    }
    
    if _model_type >= 0:
        
        turbolence = Facades(model[_model_type])
        
        return turbolence
    else:
        return

wind_resistance = main()
if not wind_resistance: print("Please, connect _model_type.")