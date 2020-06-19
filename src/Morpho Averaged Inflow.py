# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Active averaged inflow, the air temperature change will be calculated with the average inflow values instead of avg values of each grid cell.
-
EXPERT SETTINGS.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _active: Set active to 'True' for avg inflow [bool].
        
    Returns:
        read_me: Message for users.
        parallel_cpu: Avg inflow calculation settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Averaged Inflow"
ghenv.Component.NickName = "morpho_avg_inflow"
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
    from Morpho25.Settings import InflowAvg, Active
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    if _active:
        
        avg_inflow = InflowAvg(Active.YES)
        
        return avg_inflow
    else:
        return

avg_inflow = main()
if not avg_inflow: print("Please, connect _active.")