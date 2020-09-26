# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Read EDT files of dynamic output of buildings
-
Compatible edt files come from
. Building\dynamic -> facade dir X, Y, Z
. SolarAccess SAFAC -> facade dir X, Y, Z
-
You can use native component of Grasshopper to set the color of the pixels 
OR you can use 'recolor mesh' component of Ladybug[+] or Ladybug Legacy to use lots of additional features for visualization.
    Args:
        _edt: File path of EDT binary file [string].
        _variable_: Index of the variable to read [integer].
        -
        Connect a panel to 'variables' output to see all available variables
        AND a panel to 'selected_variable' to see the current one.
        _dir_: Connect an integer to set the direction of the facades to read [integer]. Default is 0.
        0 = X
        1 = Y
        2 = Z
        base_point_: Lower left corner of the grid. Default value is point at 0,0,0. Connect a Rhino point to change it [Point3d].
        min_: A number representing the lower boundary to filter data [float]. 
        max_: A number representing the upper boundary to filter data [float]. 
        _run_it: Set it to 'True' to get face and values.
    
    Returns:
        read_me: Message for users.
        variables: All available variable you can read. Check the index to use with _variable_ input.
        project_name: Name of the current envimet project.
        date: Simulation date.
        time: Simulation time.
        selected_variable: Name of the current variable.
        face: List of face object to parse. It contains geometry information.
        values: Values for each face.
"""

ghenv.Component.Name = "Morpho Read Building Facades"
ghenv.Component.NickName = "morpho_read_building_facades"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "1 || IO"
try: ghenv.Component.AdditionalHelpFromDocStrings = "3"
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
    clr.AddReference("MorphoReader.dll")
    from MorphoReader import GridOutput, BuildingOutput, Direction, Facade
    from MorphoRhino.RhinoAdapter import RhinoConvert
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.0 2.5D/3D"

def get_file_path(path):
    
    file, extension = os.path.splitext(path)
    
    return path, file + ".EDX"


def main():
    
    if _edt:
        
        edt, edx = get_file_path(_edt)
        
        variable = 1 if _variable_ == None else _variable_
        
        if _dir_ == 1:
            dir = Direction.Y
        elif _dir_ == 2:
            dir = Direction.Z
        else:
            dir = Direction.X
        
        if base_point_:
            origin = RhinoConvert.FromRhPointToVector(base_point_)
            output = BuildingOutput(edx, origin)
        else:
            output = BuildingOutput(edx)
        
        project_name = output.ProjectName
        date = output.SimulationDate
        time = output.SimulationTime
        variables = output.VariableName
        selected_variable = output.VariableName[variable]
        
        print("Grid: {0}, {1}, {2}".format(output.NumX, output.NumY, output.NumZ))
        
        if output.DataContent not in [7, 10]:
            print("Please, connect a file of dynamic output of buildings or solaraccess SAFAC.")
            return [None] * 7
        
        if _run_it:
            
            facades = output.GetFacades(dir)
            output.SetValuesFromBinary(edt, facades, variable)
            
            # filter
            facades = Facade.GetFacadesByDirection(facades, dir)
            if min_ != None and max_ != None:
                facades = Facade.GetFacadesByThreshold(facades, min_, max_, dir)
            
            # get values from facades
            if _dir_ == 1:
                values = Facade.GetValueYFromFacades(facades)
            elif _dir_ == 2:
                values = Facade.GetValueZFromFacades(facades)
            else:
                values = Facade.GetValueXFromFacades(facades)
            # get geometry
            face = Facade.GetFacesFromFacades(facades)
            
            return variables, project_name, date, time, selected_variable, face, values
            
        return variables, project_name, date, time, selected_variable, None, None
    else:
        return [None] * 7


if not _edt:
    print("Please, connect _edt and check available outputs using panels.")
else:
    variables, project_name, date, time, selected_variable, face, values = main()

