# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
EXPERT ONLY - Write an Inx Model adding custom ASCII terrain matrix. You must use it before running simulation.
-
Use this component to save INX file on your machine and only if you want to use a DEM matrix from custom text, otherwise you can use Morpho Terrain to generate terrain from Rhino geometries.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _inx_model: Inx Model.
        _ASCII_DEM_file: absolute path of text file that contain only ASCII matrix of terrain.E.g. C:\Example\dem.txt
        -
        Terrain ASCII matrix is made by integers divided by comma and each row is divided by newline. E.g.
        2,2,3,4,5,6,6
        2,2,3,4,5,6,6
        ...
        Number of row must be equal to numY of Grid. Number of integers for each row must be equal to numX of Grid.
        _write_it: Set it to 'True' to write model on your machine.

    Returns:
        read_me: Message for users.
        file_path: Path where inx file is on your machine.
"""

ghenv.Component.Name = "Morpho Write Model ASCII DEM"
ghenv.Component.NickName = "morpho_write_model_ASCII_DEM"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "3 || IO"
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
    from Morpho25.IO import Inx

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    if _inx_model and _ASCII_DEM_file and _write_it:

        with open(_ASCII_DEM_file, 'r') as f:
            text = f.read()

        inx = Inx(_inx_model, text)

        inx.WriteInx()

        return inx.Model.Workspace.ModelPath
    else:
        return

file_path = main()
if not file_path: print("Please, connect _inx_model, _ASCII_DEM_file and set _write_it to 'True'.")
