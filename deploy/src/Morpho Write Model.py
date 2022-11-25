# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Write an Inx Model. You must use it before running simulation.
-
Use this component to save INX file on your machine.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _inx_model: Inx Model.
        _write_it: Set it to 'True' to write model on your machine.
    
    Returns:
        read_me: Message for users.
        file_path: Path where inx file is on your machine.
"""

ghenv.Component.Name = "Morpho Write Model"
ghenv.Component.NickName = "morpho_write_model"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "3 || IO"
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
    from Morpho25.IO import Inx
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    if _inx_model and _write_it:
        
        inx = Inx(_inx_model)
        inx.WriteInx()
        
        return inx.Model.Workspace.ModelPath
    else:
        return

file_path = main()
if not file_path: print("Please, connect _inx_model and set _write_it to 'True'.")