# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
From Morpho Face to Rhino mesh.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _face: Morpho Face [list Face]

    Returns:
        read_me: Message for users.
        rh_mesh: Rhino mesh.
"""

ghenv.Component.Name = "Morpho From Face To RhinoMesh"
ghenv.Component.NickName = "morpho_from_face_to_rhinomesh"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "0 || Geometry"
try: ghenv.Component.AdditionalHelpFromDocStrings = "1"
except: pass

import scriptcontext as sc
import os
import sys
import clr
################### Morpho #####################
try:
    user_path = os.getenv("APPDATA")
    sys.path.append(os.path.join(user_path, "Morpho"))
    clr.AddReferenceToFile("MorphoGeometry.dll")
    clr.AddReferenceToFile("MorphoRhino.dll")
    from MorphoRhino.RhinoAdapter import RhinoConvert
    from MorphoGeometry import Face

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

from System.Collections.Generic import *

def main():
    return RhinoConvert.FromFacesToMesh(List[Face](_face))

if _face == [None] or _face == []:
    print("Please, connect _face.")
else:
    rh_mesh = main()
