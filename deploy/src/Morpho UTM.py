# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Construct UTM coordinate to add the georeference to your envimet model.
-
You can use ShrimpGIS to get it!
    Args:
        _utm_zone: UTM zone number (e.g. 32) [string].
        _utm_easting: Easting value of UTM coordinate (or X) [float].
        _utm_northing: Northing value of UTM coordinate (or Y) [float].
    
    Returns:
        read_me: Message for users.
        utm: UTM object.
"""

ghenv.Component.Name = "Morpho UTM"
ghenv.Component.NickName = "morpho_utm"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "1 || Settings"
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
    clr.AddReferenceToFile("Morpho25.dll")
    from Morpho25.Settings import UTM
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    if _utm_zone and _utm_easting and _utm_northing:
        
        utm = UTM(_utm_easting, _utm_northing, _utm_zone)
        
        return utm
    else:
        return

utm = main()
if not utm: print("Please, connect utm_zone, utm_easting and utm_northing")