# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Construct a Inx Mesh to use with Envimet Entities.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _mesh: Rhino mesh [List or Item or Tree of Mesh]
    
    Returns:
        read_me: Message for users.
        inx_mesh: Inx Mesh to use with Envimet Entities.
        mesh: Rhino mesh for checking.
"""

ghenv.Component.Name = "Morpho Mesh"
ghenv.Component.NickName = "morpho_mesh"
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
    clr.AddReference("MorphoRhino.dll")
    from MorphoRhino.RhinoAdapter import RhinoConvert
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    if _mesh != None:
        
        rh_mesh = RhinoConvert.CreateMeshFromMeshes(_mesh)
        inx_mesh = RhinoConvert.ConvertToMesh(rh_mesh)
        
        return inx_mesh, rh_mesh
    else:
        return

inx_mesh, mesh = main()
if not _mesh: print("Please, connect _mesh.")