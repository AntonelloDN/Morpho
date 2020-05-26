# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Add cloud to your model. The default setting is with no clouds.
-
EXPERT SETTINGS.
-
Icon made by Good Ware <https://www.flaticon.com/free-icon/sun_861076?term=cloud&page=1&position=48>.
See license for more details.
    Args:
        _low_clouds_: Fraction of LOW clouds (x/8). Default value is 0 (no clouds).
        _middle_clouds_: Fraction of MIDDLE clouds (x/8). Default value is 0 (no clouds).
        _high_clouds_: Fraction of HIGH clouds (x/8). Default value is 0 (no clouds).
        
    Returns:
        read_me: Message for users.
        clouds: Cloud settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Clouds"
ghenv.Component.NickName = "morpho_clouds"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "4 || Simulation"
try: ghenv.Component.AdditionalHelpFromDocStrings = "2"
except: pass

import scriptcontext as sc
import os
import sys
import clr
##################Envimet INX####################
try:
    user_path = os.getenv("APPDATA")
    sys.path.append(os.path.join(user_path, "Morpho"))
    clr.AddReference("Morpho25.dll")
    from Morpho25.Settings import Cloud
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    clouds = Cloud()
    
    if _low_clouds_: clouds.LowClouds = _low_clouds_
    if _middle_clouds_: clouds.MiddleClouds = _middle_clouds_
    if _high_clouds_: clouds.HighClouds = _high_clouds_
    
    return clouds

clouds = main()