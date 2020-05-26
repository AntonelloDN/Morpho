# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

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
    clr.AddReference("Morpho25.dll")
    from Morpho25.IO import Inx
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    if _inx_model and _write_it:
        
        Inx.WriteInx(_inx_model)
        
        return _inx_model.Workspace.ModelPath
    else:
        return

file_path = main()
if not file_path: print("Please, connect _inx_model, _project_path and set _write_it to 'True'.")