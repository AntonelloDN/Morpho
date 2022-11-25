# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Force boundary condition using EPW file.
-
NOTE. Some EPW could not be compatible with Envimet FOX Manager. You get an error in case of incompatibility.
    Args:
        _epw: Absolute path of Epw file to use to create FOX file. E.g. C:\MyEpw\Example.epw
        _inx_workspace: Inx Workspace object of your current project.
        _force_temperature_: Set it to 'True' to use temperature values of EPW as boundary condition [bool]. Default value is 'True'.
        _force_wind_: Set it to 'True' to use wind speed and direction values of EPW as boundary condition [bool]. Default value is 'True'.
        _force_relative_humidity_: Set it to 'True' to use relative humidity values of EPW as boundary condition [bool]. Default value is 'True'.
        _force_precipitation_: Set it to 'True' to use precipitation values of EPW as boundary condition [bool]. Default value is 'False'.
        _force_radiation_clouds_: Set it to 'True' to use radiation and cloudiness values of EPW as boundary condition [bool]. Default value is 'True'.
        min_flow_steps_: FOR EXPERT ONLY. Adjust the minimum interal for updating the Full Forcing inflow. Default value is 50.
        limit_wind_2500_: FOR EXPERT ONLY. Limit of wind speed at 2500 meter. Default is 0.
        max_wind_2500_: FOR EXPERT ONLY. Max wind speed at 2500 meter. Default is 20 m/s.
        _run_it: Set it to 'True' to create fox file.
        
    Returns:
        read_me: Message for users.
        full_forcing: Full forcing settings of *.simx file.
        
"""

ghenv.Component.Name = "Morpho Full Forcing"
ghenv.Component.NickName = "morpho_full_forcing"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "4 || Simulation"
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
    clr.AddReferenceToFile("Morpho25.dll")
    from Morpho25.Settings import FullForcing, Active
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    if _epw and _inx_workspace:
        
        full_forcing = FullForcing(_epw, _inx_workspace)
        
        if _force_temperature_ != None: 
            full_forcing.ForceTemperature = int(Active.YES) if _force_temperature_ == True else int(Active.NO)
        if _force_wind_ != None: 
            full_forcing.ForceWind = int(Active.YES) if _force_wind_ == True else int(Active.NO)
        if _force_relative_humidity_ != None: 
            full_forcing.ForceRelativeHumidity = int(Active.YES) if _force_relative_humidity_ == True else int(Active.NO)
        if _force_precipitation_ != None: 
            full_forcing.ForcePrecipitation = int(Active.YES) if _force_precipitation_ == True else int(Active.NO)
        if _force_radiation_clouds_ != None: 
            full_forcing.ForceRadClouds = int(Active.YES) if _force_radiation_clouds_ == True else int(Active.NO)
        if min_flow_steps_: full_forcing.MinFlowsteps = min_flow_steps_
        if limit_wind_2500_: full_forcing.LimitWind2500 = limit_wind_2500_
        if max_wind_2500_: full_forcing.MaxWind2500 = max_wind_2500_
        
        return full_forcing
    else:
        return

if _run_it:
    full_forcing = main()
    if not full_forcing: print("Please, connect _epw and _inx_workspace.")