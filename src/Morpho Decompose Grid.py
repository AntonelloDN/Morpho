# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Decompose Inx Grid to visualize its points (centroids).
    Args:
        _inx_grid: Inx Grid.
        _dir_: Connect an number [integer].
        -
        0 = XY
        1 = XZ
        2 = YZ

    Returns:
        read_me: Message for users.
        points: Preview of Envimet Grid.
"""

ghenv.Component.Name = "Morpho Decompose Grid"
ghenv.Component.NickName = "morpho_decomp_grid"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "1 || Settings"
try: ghenv.Component.AdditionalHelpFromDocStrings = "2"
except: pass

import scriptcontext as sc
import os
import sys
import clr
import Rhino
##################Envimet INX####################
try:
    user_path = os.getenv("APPDATA")
    sys.path.append(os.path.join(user_path, "Morpho"))
    clr.AddReferenceToFile("Morpho25.dll")
    from Morpho25.Geometry import Grid

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    if _inx_grid:

        points = []

        x_axis = _inx_grid.Xaxis;
        y_axis = _inx_grid.Yaxis;
        z_axis = _inx_grid.Zaxis;

        dir = 0 if _dir_ == None else _dir_

        if dir == 0:
            for x in x_axis:
                for y in y_axis:
                    points.append(Rhino.Geometry.Point3d(x, y, 0))

        if dir == 1:
            for x in x_axis:
                for z in z_axis:
                    points.append(Rhino.Geometry.Point3d(x, y_axis[0], z))

        if dir == 2:
            for y in y_axis:
                for z in z_axis:
                    points.append(Rhino.Geometry.Point3d(x_axis[0], y, z))

        return points
    else:
        return

points = main()
if not points: print("Please, connect _inx_grid.")
