# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Set output interval.
There is also a new functionality, BPS (Building Performance Simulation): integration with EnergyPlus and TRNSYS.
-
EXPERT SETTINGS.
-
REMINDER: BPS is not supported in BASIC and STUDENT version.
    Args:
        _interval_main_file_: Decide in which output interval save output files (e.g. atmoshere) [float]. Default is 60 minutes.
        _interval_receptor_building_: Decide in which output interval save output of receptor and building [float]. Default is 60 minutes.
        _netCDF_: Set it to 'True' if you want to write output in NetCDF format [bool].
        -
        If you have issue with it check you have Microsoft Visual C++ 2010 SP1 Redistributable Package on your machine.
        You can download it for free.
        _netCDF_in_one_file_: Set it to 'True' if you want store output interval in a single NetCDF file [bool]. Default value is for every output inteval (e.g. every virtual hour).
        _BPS_: Write detailed building data to use with EnergyPlus and TRNSYS. Set it to 'True' to active it.
        -
        It works building by building. Once you have activated you must connect inx building you want to calculate.
        -
        REMINDER It is not supported in LITE and STUDENT version. 
        inx_building_: Connect inx buildings to calculate with BPS.
        
    Returns:
        read_me: Message for users.
        output_settings: Output settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Output Settings"
ghenv.Component.NickName = "morpho_output_settings"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "4 || Simulation"
try: ghenv.Component.AdditionalHelpFromDocStrings = "2"
except: pass

import Grasshopper.Kernel as gh
import scriptcontext as sc
import os
import sys
import clr
##################Envimet INX####################
from System.Collections.Generic import *
try:
    user_path = os.getenv("APPDATA")
    sys.path.append(os.path.join(user_path, "Morpho"))
    clr.AddReference("Morpho25.dll")
    from Morpho25.Settings import OutputSettings, Active
    from Morpho25.Geometry import Building
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    ouput_setting = OutputSettings()
    
    if _netCDF_: ouput_setting.NetCDF = int(Active.YES)
    if _netCDF_in_one_file_: ouput_setting.NetCDFAllDataInOneFile = int(Active.YES)
    if _BPS_: ouput_setting.WriteBPS = int(Active.YES)
    if _interval_main_file_: ouput_setting.MainFiles = _interval_main_file_
    if _interval_receptor_building_: ouput_setting.TextFiles = _interval_receptor_building_
    if inx_building_ and _BPS_:
        buildings = List[Building](inx_building_)
        ouput_setting.SetBuildingNumber(buildings)
    
    if (ouput_setting.BuildingCnt == 0 and _BPS_):
        w = gh.GH_RuntimeMessageLevel.Warning
        message = "Please provide inx_building_."
        ghenv.Component.AddRuntimeMessage(w, message)
    
    return ouput_setting

output_settings = main()