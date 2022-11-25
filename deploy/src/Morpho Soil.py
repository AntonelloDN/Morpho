# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Construct an Inx Soil.
-
Behavior:
The component will assign automatically IDs starting from _ID connected
    Args:
        _inx_grid: Inx Grid.
        _inx_facegroup: Inx Facegroup.
        _ID: An integer to identify soil [integer].
        _name_: Optional name to give to soil [string].
        _code_: Code of material to apply to soil [string]. E.g. 0100LO.
        -
        Type of material to connect: profile
    
    Returns:
        read_me: Message for users.
        inx_soil: Inx Soil to use as input of 'Morpho Model' component.
        id_count: Connect this number to the _ID of another 'morpho_soil' component to keep the correct ID order
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
    clr.AddReferenceToFile("Morpho25.dll")
    from Morpho25.Geometry import Soil
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    if _inx_grid and _inx_facegroup and _ID:
        
        IDs = [i+_ID for i in xrange(len(_inx_facegroup))]
        
        soil = [Soil(_inx_grid, mesh, index, _code_, _name_) for mesh, index in zip(_inx_facegroup, IDs)]
        
        return soil
    
    else:
        return

inx_soil = main()
if not inx_soil: print("Please, connect _inx_grid, _inx_facegroup, _ID.")
if inx_soil:
    id_count = len(inx_soil) + 1