# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Read and sort all comfort metric binaries of an output folder of envimet.
-
You have to use Biomet to generate UTCI, PMV, SET and PET output.
    Args:
        _output_folder: File path of the output folder of envimet [string].
    
    Returns:
        read_me: Message for users.
        atmosphere: Atmosphere binaries (edt).
        UTCI: Universal Thermal Climate Index in degrees Celcius. 
        PET: Physiological Equivalent Temperature in degrees Celcius.
        SET: Standard Effective Temperature (SET) in degrees Celcius.
        PMV: Predicted Mean Vote.
"""

ghenv.Component.Name = "Morpho Output Comfort Folder"
ghenv.Component.NickName = "morpho_output_comfort_folder"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "3 || IO"
try: ghenv.Component.AdditionalHelpFromDocStrings = "1"
except: pass
ghenv.Component.Message = "1.1.0"

import os

if _output_folder:
    directory = os.listdir(_output_folder)
    
    folder_to_search = ['atmosphere', 'biomet']
    
    path = [os.path.join(_output_folder, f) for f in directory 
            if str.lower(f) in folder_to_search]
    
    result = {}
    for p in path:
        
        if os.path.basename(p) == 'biomet':
            subdir = os.listdir(p)
            if 'PMV' in subdir:
                path.append(p + '\\PMV')
            if 'PET' in subdir:
                path.append(p + '\\PET')
            if 'SET' in subdir:
                path.append(p + '\\SET')
            if 'UTCI' in subdir:
                path.append(p + '\\UTCI')
        
        result[os.path.basename(p)] = sorted([os.path.join(p, f) for f in os.listdir(p) 
                                             if f.endswith(".EDT") 
                                             and 'First Flow Field'not in f
                                             and 'initialisation' not in f])
    
    if 'atmosphere' in result:
        atmosphere = result['atmosphere']
    if 'PET' in result:
        PET = result['PET']
    if 'PMV' in result:
        PMV = result['PMV']
    if 'SET' in result:
        SET = result['SET']
    if 'UTCI' in result:
        UTCI = result['UTCI']
else:
    print 'Please connect the full path of an envimet output folder'