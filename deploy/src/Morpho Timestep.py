# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Force timestep conditions.
-
EXPERT SETTINGS. If you exceed with it results could be inaccurate.
    Args:
        _sun_height_step01: Sun height for switching dt(0) [float]. Defualt vale is 40.00 deg.
        -
        meaning = From 0 deg to 40.00 deg the time step of sun will be _timeStepInterval1_.
        _sun_height_step02: Sun height for switching dt(1) [float]. Defualt vale is 50.00 deg.
        -
        meaning = From 40.00 deg to 50.00 deg the time step of sun will be _timeStepInterval2_.
        _dt_step00: Time step (s) for interval 1 dt(0) [float]. Default value is 2 seconds.
        _dt_step01: Time step (s) for interval 1 dt(0) [float]. Default value is 2 seconds.
        _dt_step02: Time step (s) for interval 1 dt(0) [float]. Default value is 1 seconds.
        -
        You have much more radiation when the sun is high in the sky, for this reason ENVI_MET apply timestep 1 s when elevation degree is greater than 50.00 deg.
        
    Returns:
        read_me: Message for users.
        timestep: Timestep settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Timestep"
ghenv.Component.NickName = "morpho_timestep"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "4 || Simulation"
try: ghenv.Component.AdditionalHelpFromDocStrings = "2"
except: pass

import scriptcontext as sc
import os
import sys
import clr
##################Envimet INX####################
from System.Collections.Generic import *
try:
    user_path = os.getenv("APPDATA")
    sys.path.append(os.path.join(user_path, "Morpho"))
    clr.AddReferenceToFile("Morpho25.dll")
    from Morpho25.Settings import TimeSteps
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    timestep = TimeSteps()
    
    if _sun_height_step01_: timestep.SunheightStep01 = _sun_height_step01_
    if _sun_height_step02_: timestep.SunheightStep02 = _sun_height_step02_
    if _dt_step00_: timestep.DtStep00 = _dt_step00_
    if _dt_step01_: timestep.DtStep01 = _dt_step01_
    if _dt_step02_: timestep.DtStep02 = _dt_step02_
    
    return timestep

timestep = main()