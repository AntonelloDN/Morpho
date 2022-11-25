# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2021, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Set Nesting Grids properties.
    Args:
        _soil_profile_a: First profile material to use [str].
        _soil_profile_b: Second profile material to use [str].
        _number_of_cells: Number of nesting cells to add [int].
    
    Returns:
        read_me: Message for users.
        nesting_grids: Nesting Grids attributes. Connect it to Grid component.
"""

ghenv.Component.Name = "Morpho Nesting Grids"
ghenv.Component.NickName = "morpho_nesting_grids"
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
    from Morpho25.Geometry import NestingGrids
    from Morpho25.Geometry import Material
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    soil_profile_a = _soil_profile_a if _soil_profile_a else Material.DEFAULT_SOIL
    soil_profile_b = _soil_profile_b if _soil_profile_b else Material.DEFAULT_SOIL
    
    num_of_cells = _number_of_cells if _number_of_cells else 0
    
    nesting_grids = NestingGrids(num_of_cells, soil_profile_a, soil_profile_b)
    
    return nesting_grids

nesting_grids = main()