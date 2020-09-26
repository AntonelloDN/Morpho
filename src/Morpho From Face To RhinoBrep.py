# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
From Morpho Face to Rhino brep.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _face: Morpho Face [list Face]
    
    Returns:
        read_me: Message for users.
        rh_brep: Rhino brep.
"""

ghenv.Component.Name = "Morpho From Face To RhinoBrep"
ghenv.Component.NickName = "morpho_from_face_to_rhinobrep"
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
    clr.AddReference("MorphoGeometry.dll")
    clr.AddReference("MorphoRhino.dll")
    from MorphoRhino.RhinoAdapter import RhinoConvert
    from MorphoGeometry import Face
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

import Rhino as rc

def main():
    
    surface = []
    
    pt1 = [RhinoConvert.FromVectorToRhPoint(face.A) for face in _face]
    pt2 = [RhinoConvert.FromVectorToRhPoint(face.B) for face in _face]
    pt3 = [RhinoConvert.FromVectorToRhPoint(face.C) for face in _face]
    pt4 = [RhinoConvert.FromVectorToRhPoint(face.D) for face in _face]
    
    
    for p1, p2, p3, p4 in zip(pt1, pt2, pt3, pt4):
        surface.append(rc.Geometry.Brep.CreateFromCornerPoints(p1, p2, p3, p4, 0.01))
    
    return surface

if _face == [None] or _face == []: 
    print("Please, connect _face.")
else:
    rh_brep = main()