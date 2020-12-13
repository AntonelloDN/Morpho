# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Construct material code to create a building material.
    Args:
        _wall_code_: Code of wall material [string]. E.g. 000000.
        -
        Type of material to connect: wall
        _roof_code_: Code of roof material [string]. E.g. 000000.
        -
        Type of material to connect: wall
        _green_wall_code_: Code of green wall material [string].
        -
        Type of material to connect: greening
        _green_roof_code_: Code of green roof material [string].
        -
        Type of material to connect: greening

    Returns:
        read_me: Message for users.
        material: Inx Building material.
"""

ghenv.Component.Name = "Morpho Building Material"
ghenv.Component.NickName = "morpho_building_mat"
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
    from Morpho25.Geometry import Building

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    material = Building.CreateMaterial(_wall_code_, _roof_code_, _green_wall_code_, _green_roof_code_)

    return material

material = main()
