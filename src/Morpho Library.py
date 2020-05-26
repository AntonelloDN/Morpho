# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Read Envimet materials.
-
You can connect 'code' to customize inx object materials.
    Args:
        _inx_workspace:  Workspace of your current project.
        _type_: Type of material to read. Write one of following string
        profile
        soil
        material
        wall
        source
        plant
        plant3d
        greening
        -
        Default value is 'profile'.
        _keyword_: Connect a string to filter library. E.g. asphalt [string].
        systemDB_: Set it 'True' to use system database.
        If you set 'False' user or profile library will be used. Default is 'True' [bool].
    
    Returns:
        read_me: Message for users.
        code: Material code to use with inx_objects.
        description: Material description.
        XML: XML detail of material.
"""

ghenv.Component.Name = "Morpho Library"
ghenv.Component.NickName = "morpho_library"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "1 || Settings"
try: ghenv.Component.AdditionalHelpFromDocStrings = "1"
except: pass

import scriptcontext as sc
import os
import sys
import clr
################### Morpho #####################
try:
    user_path = os.getenv("APPDATA")
    sys.path.append(os.path.join(user_path, "Morpho"))
    clr.AddReference("Morpho25.dll")
    from Morpho25.Management import Workspace
    from Morpho25.IO import Library
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    if systemDB_ == None:
        systemDB = True
    else:
        systemDB = systemDB_
    
    type = _type_.upper() if _type_ else Library.PROFILE
    
    alternative_lib = _inx_workspace.UserDB if _inx_workspace.UserDB else _inx_workspace.ProjectDB
    lib = _inx_workspace.SystemDB if systemDB else alternative_lib
    
    library = Library(lib, type, _keyword_)
    
    return library.Code, library.Description, library.Detail

if not _inx_workspace:
    print("Please, connect _inx_workspace.")
else:
    code, description, XML = main()