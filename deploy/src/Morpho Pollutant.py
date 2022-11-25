# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Set pollutant.
-
EXPERT SETTINGS.
    Args:
        _pollutant_name: Name of pollutant source [string].
        _pollutant_type: Connect an integer to set pollutant type
        0 = PM
        1 = CO
        2 = CO2
        3 = NO
        4 = NO2
        5 = SO2
        6 = NH3
        7 = H2O2
        8 = SPRAY
        _multiple_sources_: Set it to 'True' to set multi pollutant. [bool] Default value is single pollutant.
        -
        The pollutant emission rates are taken from the emission profiles defined in the Database Manager.
        In Single pollutant mode, only the user-defined pollutant is used.
        _active_chemistry_:  Set it to 'True' to set dispersion and active chemistry. [bool] Default value is dispersion only.
        _diameter_: Particle diameter (μm) [float]. Default is 10.00.
        -
        Define it if you select SPRAY or PM.
        _density_:  Particle density (g/cm3) [float]. Default is 1.00.
        -
        Define it if you select SPRAY or PM.
        
    Returns:
        read_me: Message for users.
        source_settings: Source settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Pollutant"
ghenv.Component.NickName = "morpho_pollutant"
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
    clr.AddReferenceToFile("Morpho25.dll")
    from Morpho25.Settings import Pollutant, Sources, Active
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    pollutant = {
        0:Pollutant.PM,
        1:Pollutant.CO,
        2:Pollutant.CO2,
        3:Pollutant.NO,
        4:Pollutant.NO2,
        5:Pollutant.SO2,
        6:Pollutant.NH3,
        7:Pollutant.H2O2,
        8:Pollutant.SPRAY
    }
    
    if _pollutant_name and _pollutant_type >= 0:
        
        multiple = Active.YES if _multiple_sources_ else Active.NO
        chemistry = Active.YES if _active_chemistry_ else Active.NO
        
        source = Sources(_pollutant_name, pollutant[_pollutant_type], multiple, chemistry)
        
        if _diameter_: source.UserPartDiameter = _diameter_
        if _density_: source.UserPartDensity = _density_
        
        return source
    else:
        return

source_settings = main()
if not source_settings: print("Please, connect _pollutant_name and _pollutant_type.")