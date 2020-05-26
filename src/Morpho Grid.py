# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Construct an Inx Grid.
    Args:
        _grid_size: Grid properties created by 'Envimet INX Grid Size'.
        _telescope_: Connect a number to change grid type from equidistant grid into telescopic grid [float].
        _telescope_start_height_: Height where to start telescoping Z grid growth [float].
        _combined_grid_: Set it to 'True' if you want to combine equidistant and telescopic grid [bool].
    
    Returns:
        read_me: Message for users.
        inx_grid: Inx Grid.
"""

ghenv.Component.Name = "Morpho Grid"
ghenv.Component.NickName = "morpho_grid"
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
    clr.AddReference("Morpho25.dll")
    from Morpho25.Geometry import Grid
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    if _grid_size:
        
        grid = Grid(_grid_size)
        if (_telescope_ > 0 and _telescope_start_height_ > 0):
            grid = Grid(_grid_size, _telescope_, _telescope_start_height_, _combined_grid_)
        
        return grid
    else:
        return

inx_grid = main()
if not _grid_size: print("Please, connect _grid_size.")