# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
This component write a batch and run simulation using envimet_console.exe.
    Args:
        _simx: Simulation (SIMX) to run.
        _run_it: Set it to 'True' to run simulation.

    Returns:
        read_me: Message for users.
"""

ghenv.Component.Name = "Morpho Run Simulation"
ghenv.Component.NickName = "morpho_run_simulation"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "5 || Util"
try: ghenv.Component.AdditionalHelpFromDocStrings = "1"
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
    from Morpho25.IO import SimulationBatch

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    if _simx and _run_it:

        SimulationBatch.RunSimulation(_simx)

        return "OK"
    else:
        return

result = main()
if not result: print("Please, connect _main_settings and set _run_it to 'True'.")
