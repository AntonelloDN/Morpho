# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Set lateral boundary condition.
-
'Forced' is used when simpleforcing is activate.
'Open' copy the values of the next grid point close to the border back to the border each timestep which mean overstimate the influence of environment near border.
'Cyclic' describes the process of copying values of the downstream boarder to the upstream boarder.
-
EXPERT SETTINGS.
    Args:
        _LBC_temperature_humidity: Connect an integer to select LBC model.
        0 = Open
        1 = Forced
        2 = Cyclic
        _LBC_turbolence: Connect an integer to select LBC model.
        0 = Open
        1 = Forced
        2 = Cyclic

    Returns:
        read_me: Message for users.
        LBC: Lateral boundary condition to use in *.simx file.
"""

ghenv.Component.Name = "Morpho LBC"
ghenv.Component.NickName = "morpho_LBC"
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
    from Morpho25.Settings import LBC, BoundaryCondition

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    model = {
        0:BoundaryCondition.Open,
        1:BoundaryCondition.Forced,
        2:BoundaryCondition.Cyclic
    }

    if _LBC_temperature_humidity >= 0 and _LBC_turbolence >= 0:

        lbc = LBC(model[_LBC_temperature_humidity], model[_LBC_turbolence])

        return lbc
    else:
        return

LBC = main()
if not LBC: print("Please, connect _LBC_temperature_humidity and _LBC_turbolence.")
