# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Set main part of simulation file.
-
Use this component to set basic properties of the simulation file.
    Args:
        _inx_model: Inx Model.
        _sim_name: Name of simulation file (*.simx).
        _start_date_: It sets when your simulation starts [string]. Format DD.MM.YYYY.
        -
        Default value is 23.06.2018
        _start_time_: It sets at what time your simulation starts [string]. Format HH:MM:SS
        -
        Default value is 06:00:00
        _duration_: Duration of simulation in hours [integer]. Default value is 24.
        wind_speed_: Initial wind speed (m/s) [float]. Default value is 2.5.
        wind_direction_: Initial wind direction (°dec) [float]. Default value is 0.
        roughness_: Roughness [float]. Default value is 0.01.
        initial_temperature_: Initial temperature of the air (°C) [float]. Default value is 19.
        specific_humidity_: Initial specific humidity of the air in 2500 m (g Water/kg air). Default value is 7.0.
        relative_humidity_: Initial relative humidity of the air in 2m (%) [float]. Default value is 50%.
        
    Returns:
        read_me: Message for users.
        main_settings: Basic settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Main Settings"
ghenv.Component.NickName = "morpho_main_settings"
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
    from Morpho25.Settings import MainSettings
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    if _inx_model and _sim_name:
        
        main_settings = MainSettings(_sim_name, _inx_model)
        if _start_date_: main_settings.StartDate = _start_date_
        if _start_time_: main_settings.StartTime = _start_time_
        if _duration_: main_settings.SimDuration = _duration_
        if wind_speed_: main_settings.WindSpeed = wind_speed_
        if wind_direction_: main_settings.WindDir = wind_direction_
        if roughness_: main_settings.Roughness = roughness_
        if initial_temperature_: main_settings.InitialTemperature = initial_temperature_
        if specific_humidity_: main_settings.SpecificHumidity = specific_humidity_
        if relative_humidity_: main_settings.RelativeHumidity = relative_humidity_
        
        return main_settings
    else:
        return

main_settings = main()
if not main_settings: print("Please, connect _inx_workspace and _sim_name.")