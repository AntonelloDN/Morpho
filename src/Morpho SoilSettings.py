# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Set boundary condition of soil at different layers.
    Args:
        _temperature_upper_layer_: Initial Temperature Upper Layer (0-20 cm) [°C]. Defualt vale is 19.85 °C.
        _temperature_middle_layer_: Initial Temperature Middle Layer (20-50 cm) [°C]. Defualt vale is 19.85 °C.
        _temperature_deep_layer_: Initial Temperature Deep Layer (below 50-200 cm) [°C]. Defualt vale is 19.85 °C.
        _temperature_bedrock_layer_: Initial Temperature Bedrock Layer (200 cm) [°C]. Defualt vale is 19.85 °C.
        _rh_upper_layer_: Relative Humidity Upper Layer (0-20 cm). Default value is 50.00%.
        _rh_middle_layer_: Relative Humidity Middle Layer (20-50 cm). Default value is 60.00%.
        _rh_deep_layer: Relative Humidity Deep Layer (50-200 cm). Default value is 60.00%.
        _rh_bedrock_layer_: Relative Humidity Bedrock (below 200 cm). Default value is 60.00%.
        
    Returns:
        read_me: Message for users.
        soil_settings: Soil boundary condition of *.simx file.
"""

ghenv.Component.Name = "Morpho SoilSettings"
ghenv.Component.NickName = "morpho_soilsettings"
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
    from Morpho25.Settings import SoilSettings
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    soil = SoilSettings()
    
    if _temperature_upper_layer_: soil.TempUpperlayer = _temperature_upper_layer_
    if _temperature_middle_layer_: soil.TempMiddlelayer = _temperature_middle_layer_
    if _temperature_deep_layer_: soil.TempDeeplayer = _temperature_deep_layer_
    if _temperature_bedrock_layer_: soil.TempBedrockLayer = _temperature_bedrock_layer_
    if _rh_upper_layer_: soil.WaterUpperlayer = _rh_upper_layer_
    if _rh_middle_layer_: soil.WaterMiddlelayer = _rh_middle_layer_
    if _rh_deep_layer: soil.WaterDeeplayer = _rh_deep_layer
    if _rh_bedrock_layer_: soil.WaterBedrockLayer = _rh_bedrock_layer_
    
    return soil

soil_settings = main()