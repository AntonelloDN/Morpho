# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Construct an Inx Building.
-
Behavior:
The component will assign automatically IDs starting from _ID connected
    Args:
        _inx_grid: Inx Grid.
        _inx_facegroup: Inx Facegroup that represents a building <Item or List>.
        -
        If you connect a list of inx_facegroup ID will be added automatically.
        _ID: An integer to identify a building [integer].
        _name_: Optional name to give to building [string].
        _material_: Material to apply to buildings [Material]. Default material is 000000.
        -
        Use output of 'Morpho Building Material' component to change it.
        enable_BPS_: It enables Building Performance Simulation output [bool]. For business version only.
        _run: Set it to True to run it.
    
    Returns:
        read_me: Message for users.
        inx_building: Inx Building to use as input of 'Morpho Model' component.
        id_count: Connect this number to the _ID of another 'morpho_building' component to keep the correct ID order
"""

ghenv.Component.Name = "Morpho Building"
ghenv.Component.NickName = "morpho_building"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "2 || Entity"
try: ghenv.Component.AdditionalHelpFromDocStrings = "1"
except: pass


import scriptcontext as sc
import os
import sys
import clr
import Grasshopper.Kernel as gh
##################Envimet INX####################
try:
    user_path = os.getenv("APPDATA")
    sys.path.append(os.path.join(user_path, "Morpho"))
    clr.AddReferenceToFile("Morpho25.dll")
    from Morpho25.Geometry import Building
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.1"

def main():
    
    if _run and _inx_grid and _inx_facegroup and _ID:
        IDs = [i+_ID for i in xrange(len(_inx_facegroup))]
        
        building = [Building(_inx_grid, mesh, index, 
            _material_, _name_, enable_BPS_) for mesh, index in zip(_inx_facegroup, IDs)]
        
        return building
    else:
        return

inx_building = main()
if not inx_building:
    w = gh.GH_RuntimeMessageLevel.Warning
    msg = "Please, connect _inx_grid, _inx_facegroup, _ID, _run."
    ghenv.Component.AddRuntimeMessage(w, msg)
    print(msg)
if inx_building:
    id_count = len(inx_building) + 1