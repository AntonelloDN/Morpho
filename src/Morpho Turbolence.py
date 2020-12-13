# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Set Turbolence model.
-
EXPERT SETTINGS.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _model_type: Connect an integer to select turbolence model.
        0 = MellorAndYamada
        1 = KatoAndLaunder
        2 = Lopez
        3 = Bruse ENVI_MET

    Returns:
        read_me: Message for users.
        turbolence: Turbolence model to use in *.simx file.
"""

ghenv.Component.Name = "Morpho Turbolence"
ghenv.Component.NickName = "morpho_turbolence"
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
    from Morpho25.Settings import Turbulence, TurbolenceType

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    model = {
        0:TurbolenceType.MellorAndYamada,
        1:TurbolenceType.KatoAndLaunder,
        2:TurbolenceType.Lopez,
        3:TurbolenceType.Bruse
    }

    if _model_type >= 0:

        turbolence = Turbulence(model[_model_type])

        return turbolence
    else:
        return

turbolence = main()
if not turbolence: print("Please, connect _model_type.")
