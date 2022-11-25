# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Read and sort all binaries of an output folder of envimet
    Args:
        _output_folder: File path of the output folder of envimet [string].
    
    Returns:
        read_me: Message for users.
        atmosphere: Atmosphere binaries (edt).
        buildings: Building binaries (edt).
        radiation: Radiation binaries (edt).
        soil: Soil binaries (edt).
        solaraccess: Solaraccess binaries (safac).
        surface: Surface binaries (edt).
        vegetation: Vegetation binaries (edt).
"""

ghenv.Component.Name = "Morpho Output Folder"
ghenv.Component.NickName = "morpho_output_folder"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "3 || IO"
try: ghenv.Component.AdditionalHelpFromDocStrings = "1"
except: pass
ghenv.Component.Message = "1.0.1 2.5D/3D"

import os

if _output_folder:
    directory = os.listdir(_output_folder)
    
    folder_to_search = ['atmosphere', 'buildings', 'radiation', 'soil', 'solaraccess', 'surface', 'vegetation']
    
    path = [os.path.join(_output_folder, f) for f in directory 
            if str.lower(f) in folder_to_search]
    
    result = {}
    for p in path:
        
        if os.path.basename(p) == 'buildings':
            subdir = os.listdir(p)
            if 'dynamic' in subdir:
                p += '\\dynamic'
        
        result[os.path.basename(p)] = sorted([os.path.join(p, f) for f in os.listdir(p) 
                                             if f.endswith(".EDT") 
                                             and 'First Flow Field'not in f
                                             and 'initialisation' not in f])
    
    if 'atmosphere' in result:
        atmosphere = result['atmosphere']
    if 'dynamic' in result:
        buildings = result['dynamic']
    if 'radiation' in result:
        radiation = result['radiation']
    if 'soil' in result:
        soil = result['soil']
    if 'solaraccess' in result:
        solaraccess = result['solaraccess']
    if 'surface' in result:
        surface = result['surface']
    if 'vegetation' in result:
        vegetation = result['vegetation']
else:
    print 'Please connect the full path of an envimet output folder'