# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
This component write simx object to run simulation. Connect it to 'Morpho Run Simulation'.
-
Full forcing not supported yet.
    Args:
        _main_settings: Inx Model.
        other_settings_: Connect other simulation output among:
        . "Morpho Simple Forcing"
        . "Morpho Thread"
        . "Morpho Timestep"
        . "Morpho Timing"
        . "Morpho SoilSettings"
        . "Morpho Pollutant"
        . "Morpho Turbolence"
        . "Morpho Output Settings"
        . "Morpho Clouds"
        . "Morpho Pollutant Concentration"
        . "Morpho SolarAdjust"
        . "Morpho Building Settings"
        . "Morpho IVS"
        . "Morpho Parallel Calculation"
        . "Morpho SOR"
        . "Morpho Averaged Inflow"
        . "Morpho Wind Resistance"
        . "Morpho Tree Settings"
        _run_it: Set it to 'True' to create SIMX Model.
        
        
    Returns:
        read_me: Message for users.
        simx: SIMX object to use for simulation.
"""

ghenv.Component.Name = "Morpho Simx SimpleForcing"
ghenv.Component.NickName = "morpho_simx_simpleforcing"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "3 || IO"
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
    clr.AddReference("Morpho25.dll")
    from Morpho25.IO import *
    from Morpho25.Settings import *
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    if _main_settings and _run_it:
        
        simx = Simx(_main_settings)
        
        if other_settings_:
            for obj in other_settings_:
                if (type(obj) == SimpleForcing):
                    simx.SimpleForcing = obj
                if (type(obj) == TThread):
                    simx.TThread = obj
                if (type(obj) == TimeSteps):
                    simx.TimeSteps = obj
                if (type(obj) == ModelTiming):
                    simx.ModelTiming = obj
                if (type(obj) == SoilSettings):
                    simx.SoilSettings = obj
                if (type(obj) == Sources):
                    simx.Sources = obj
                if (type(obj) == Turbulence):
                    simx.Turbulence = obj
                if (type(obj) == OutputSettings):
                    simx.OutputSettings = obj
                if (type(obj) == Cloud):
                    simx.Cloud = obj
                if (type(obj) == Background):
                    simx.Background = obj
                if (type(obj) == SolarAdjust):
                    simx.SolarAdjust = obj
                if (type(obj) == BuildingSettings):
                    simx.BuildingSettings = obj
                if (type(obj) == IVS):
                    simx.IVS = obj
                if (type(obj) == ParallelCPU):
                    simx.ParallelCPU = obj
                if (type(obj) == SOR):
                    simx.SOR = obj
                if (type(obj) == InflowAvg):
                    simx.InflowAvg = obj
                if (type(obj) == Facades):
                    simx.Facades = obj
                if (type(obj) == PlantSetting):
                    simx.PlantSetting = obj
                if (type(obj) == LBC):
                    simx.LBC = obj
        
        simx.WriteSimx()
        print("{0} written!".format(simx.MainSettings.Name))
        
        return simx
    else:
        return

simx = main()
if not simx: print("Please, connect _main_settings and _run_it")