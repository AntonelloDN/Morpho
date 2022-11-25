# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Construct an Inx Entity to view its geometry and ID.
    Args:
        _entity: Entity that comes from the sub-category 2||Entity. E.g. Morpho Building.
    
    Returns:
        read_me: Message for users.
        geometry: Morpho geometry.
        rh_geometry: Rhino/GH geometry.
        ID: Inx ID of the entity.
"""

ghenv.Component.Name = "Morpho Decompose Entity"
ghenv.Component.NickName = "morpho_decompose_entity"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "5 || Utility"
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
    clr.AddReferenceToFile("MorphoGeometry.dll")
    clr.AddReferenceToFile("Morpho25.dll")
    clr.AddReferenceToFile("MorphoRhino.dll")
    from Morpho25.Geometry import Entity
    from MorphoRhino.RhinoAdapter import RhinoConvert
    from MorphoGeometry import Vector, FaceGroup
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    if _entity:
        
        geometry = []
        rh_geometry = []
        ID = []
        
        for el in _entity:
            geometry.append(el.Geometry)
            ID.append(el.ID)
            
            if isinstance(el.Geometry, Vector):
                m = RhinoConvert.FromVectorToRhPoint(el.Geometry)
            else:
                m = RhinoConvert.FromFacesToMesh(el.Geometry.Faces)
            rh_geometry.append(m)
        
        return geometry, rh_geometry, ID
    else:
        return [None] * 3

geometry, rh_geometry, ID = main()
if not _entity: print("Please, connect an output of '2||Entity' sub-category.")