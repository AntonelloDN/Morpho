# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
This component open ENVI_MET Spaces to check the model.
    Args:
        _file_path: Full path of the Model (INX).
        _run_it: Set it to 'True' to run simulation.
        
    Returns:
        read_me: Message for users.
"""

ghenv.Component.Name = "Morpho Run INX"
ghenv.Component.NickName = "morpho_run_inx"
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
ghenv.Component.Message = "1.1.0"


def main():
    
    if _file_path and _run_it:
        
        os.startfile(_file_path)
        
        return "OK"
    else:
        return

result = main()
if not result: print("Please, connect INX file address and _run_it to 'True'.")