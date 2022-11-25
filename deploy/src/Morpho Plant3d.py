# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Construct an Inx Plant3d.
    Args:
        _inx_grid: Inx Grid.
        _inx_point: Inx Point.
        _name_: Optional name to give to plant3d group [string].
        _code_: Code of material to apply to plant3d [string]. E.g. 0000C2.
        -
        Type of material to connect: plant3d
    
    Returns:
        read_me: Message for users.
        inx_plant3d: Inx Plant3d to use as input of 'Morpho Model' component.
"""

ghenv.Component.Name = "Morpho Plant3d"
ghenv.Component.NickName = "morpho_plant3d"
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
    from Morpho25.Geometry import Plant3d
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    trees = []
    if _inx_grid and _inx_point:
        
        trees = [Plant3d(_inx_grid, vec, _code_, _name_) for vec in _inx_point]
        
        return trees
    else:
        return

inx_plant3d = main()
if not inx_plant3d: print("Please, connect _inx_grid, _inx_point.")