# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Force timing conditions of envimet objects.
-
EXPERT SETTINGS.
    Args:
        _surface_step_: Update Surface Data each ? sec. [float] Default value is 30.00 seconds.
        _flow_step_: Update Wind field each ? sec. [float] Default value is 900.00 seconds.
        _radiation_step_: Update Radiation and Shadows each ? sec. [float] Default value is 600.00 seconds.
        _plant_step_: Update Plant Data each ? sec. [float] Default value is 600.00 seconds.
        _source_step_: Update Emmission Data each ? sec. [float] Default value is 600.00 seconds.

    Returns:
        read_me: Message for users.
        model_timing: Update timing settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Timing"
ghenv.Component.NickName = "morpho_timing"
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
    from Morpho25.Settings import ModelTiming

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    model_timing = ModelTiming()

    if _surface_step_: model_timing.SurfaceSteps = _surface_step_
    if _flow_step_: model_timing.FlowSteps = _flow_step_
    if _radiation_step_: model_timing.RadiationSteps = _radiation_step_
    if _plant_step_: model_timing.PlantSteps = _plant_step_
    if _source_step_: model_timing.SourcesSteps = _source_step_

    return model_timing

model_timing = main()
