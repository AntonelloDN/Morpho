# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Construct an Inx Soil.
-
Behavior:
a. If you connect a single inx_mesh _ID will be assigned to it
b. If you connect a list of inx_mesh component will assign automatically IDs starting from _ID connected
    Args:
        _inx_grid: Inx Grid.
        _inx_mesh: Inx Mesh.
        _ID: An integer to identify soil [integer].
        _name_: Optional name to give to soil [string].
        _code_: Code of material to apply to soil [string]. E.g. 0100LO.
        -
        Type of material to connect: profile
    
    Returns:
        read_me: Message for users.
        inx_soil: Inx Soil to use as input of 'Morpho Model' component.
"""

ghenv.Component.Name = "Morpho Soil"
ghenv.Component.NickName = "morpho_soil"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "2 || Entity"
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
    from Morpho25.Geometry import Soil
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    if _inx_grid and _inx_mesh and _ID:
        
        IDs = [i+_ID for i in xrange(len(_inx_mesh))]
        
        if (_code_ != None):
            soil = [Soil(mesh, index, _code_, _inx_grid, _name_) for mesh, index in zip(_inx_mesh, IDs)]
        else:
            soil = [Soil(mesh, index, _inx_grid, _name_) for mesh, index in zip(_inx_mesh, IDs)]
        
        return soil
    
    else:
        return

inx_soil = main()
if not inx_soil: print("Please, connect _inx_grid, _inx_mesh, _ID.")# Envimet INX: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Construct an Inx Soil.
-
Behavior:
a. If you connect a single inx_mesh _ID will be assigned to it
b. If you connect a list of inx_mesh component will assign automatically IDs starting from _ID connected
    Args:
        _inx_grid: Inx Grid.
        _inx_mesh: Inx Mesh.
        _ID: An integer to identify soil [integer].
        _name_: Optional name to give to soil [string].
        _code_: Code of material to apply to soil [string]. E.g. 0100LO.
        -
        Type of material to connect: profile
    
    Returns:
        read_me: Message for users.
        inx_soil: Inx Soil to use as input of 'Morpho Model' component.
"""

ghenv.Component.Name = "Morpho Soil"
ghenv.Component.NickName = "morpho_soil"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "2 || Entity"
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
    from Morpho25.Geometry import Soil
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    if _inx_grid and _inx_mesh and _ID:
        
        IDs = [i+_ID for i in xrange(len(_inx_mesh))]
        
        if (_code_ != None):
            soil = [Soil(mesh, index, _code_, _inx_grid, _name_) for mesh, index in zip(_inx_mesh, IDs)]
        else:
            soil = [Soil(mesh, index, _inx_grid, _name_) for mesh, index in zip(_inx_mesh, IDs)]
        
        return soil
    
    else:
        return

inx_soil = main()
if not inx_soil: print("Please, connect _inx_grid, _inx_mesh, _ID.")