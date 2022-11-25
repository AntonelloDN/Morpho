# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Advance radiation transfer schema to use.
IVS allows a detailed analysis and calculation of shortwave and longwave radiation fluxes with taking into account multiple interactions between surfaces.
-
EXPERT SETTINGS.
-
REMINDER: It is not supported in BASIC version.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _IVS_on: Use Index View Sphere (IVS) for radiation transfer [bool].
        _IVS_memory: Do you want to store the values in the memory? [bool]
        -
        Storing the IVS values of every grid cell in your computer memory makes the simulation faster, but it require higher RAM demand.
    Returns:
        read_me: Message for users.
        IVS: IVS settings of *.simx file.
"""

ghenv.Component.Name = "Morpho IVS"
ghenv.Component.NickName = "morpho_ivs"
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
    from Morpho25.Settings import IVS, Active
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    if _IVS_on:
        
        ivs = Active.YES if _IVS_on else Active.NO
        ivs_memory = Active.YES if _IVS_memory else Active.NO
        
        ivs_setting = IVS(ivs, ivs_memory)
        
        return ivs_setting
    else:
        return

IVS = main()
if not IVS: print("Please, connect _IVS_on.")