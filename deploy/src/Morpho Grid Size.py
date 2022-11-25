# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Set Inx Grid properties.
    Args:
        _point: Reference point of the grid [Point3d].
        _dim_x_: X dimension of the grid cell [float].
        _dim_y_: Y dimension of the grid cell [float].
        _dim_z_: Z dimension of the grid cell [float].
        _num_x_: Number of grid cells in X [integer].
        _num_y_: Number of grid cells in Y [integer].
        _num_y_: Number of grid cells in Z [integer].
    
    Returns:
        read_me: Message for users.
        grid_size: Inx Grid attributes. Connect it to Grid component.
"""

ghenv.Component.Name = "Morpho Grid Size"
ghenv.Component.NickName = "morpho_grid_size"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "1 || Settings"
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
    clr.AddReferenceToFile("MorphoRhino.dll")
    from Morpho25.Geometry import CellDimension, Size
    from MorphoRhino.RhinoAdapter import RhinoConvert
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    if _point:
        
        origin = RhinoConvert.FromRhPointToVector(_point)
        dim_x = _dim_x_ if _dim_x_ else 3.0
        dim_y = _dim_y_ if _dim_y_ else 3.0
        dim_z = _dim_z_ if _dim_z_ else 3.0
        num_x = _num_x_ if _num_x_ else 50
        num_y = _num_y_ if _num_y_ else 50
        num_z = _num_z_ if _num_z_ else 40
        
        dimension = CellDimension(dim_x, dim_y, dim_z)
        size = Size(origin, dimension, num_x, num_y, num_z)
        
        return size
    else:
        return

grid_size = main()
if not _point: print("Please, connect _point.")