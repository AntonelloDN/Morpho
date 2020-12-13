# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Construct an Inx Terrain.
-
If you have a single mesh or surface split it in many parts to speed up calculation.
-
Behavior:
The component will assign automatically IDs starting from _ID connected
    Args:
        _inx_grid: Inx Grid.
        _inx_facegroup: Inx Facegroup.
        _ID: An integer to identify terrain group [integer].
        _name_: Optional name to give to terrain group [string].

    Returns:
        read_me: Message for users.
        inx_terrain: Inx Terrain to use as input of 'Envimet INX Model' component.
"""

ghenv.Component.Name = "Morpho Terrain"
ghenv.Component.NickName = "morpho_terrain"
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
    from Morpho25.Geometry import Terrain

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    if _inx_grid and _inx_facegroup and _ID:

        IDs = [i+_ID for i in xrange(len(_inx_facegroup))]

        terrain = [Terrain(mesh, index, _inx_grid, _name_) for mesh, index in zip(_inx_facegroup, IDs)]

        return terrain

    else:
        return

inx_terrain = main()
if not inx_terrain: print("Please, connect _inx_grid, _inx_facegroup, _ID.")
