# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Read EDT grid slice in X, Y or Z direction.
-
Compatible edt files come from
. Atmosphere -> slice dir X, Y, Z
. Surface -> slice dir Z only z0
. Soil -> slice dir Z
. Radiation -> slice dir X, Y, Z
. SolarAccess SA -> slice dir Z only z0
. Comfort (UTCI, PET, SET, PMV) -> slice dir X, Y, Z
-
You can use native component of Grasshopper to set the color of the pixels
OR you can use 'recolor mesh' component of Ladybug[+] or Ladybug Legacy to use lots of additional features for visualization.
    Args:
        _edt: File path of EDT binary file [string].
        _variable_: Index of the variable to read [integer].
        -
        Connect a panel to 'variables' output to see all available variables
        AND a panel to 'selected_variable' to see the current one.
        _dir_: Connect an integer to set the direction of the section plane [integer]. Default is 0.
        0 = X
        1 = Y
        2 = Z
        _index_: Index of the slice to read in X or Y or Z [integer].
        -
        Connect a panel to read_me to check how many cells there are.
        min_: A number representing the lower boundary to filter data [float].
        max_: A number representing the upper boundary to filter data [float].
        base_point_: Lower left corner of the grid. Default value is point at 0,0,0. Connect a Rhino point to change it [Point3d].
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

ghenv.Component.Name = "Morpho Read Grid Slice"
ghenv.Component.NickName = "morpho_read_grid_slice"
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
    clr.AddReferenceToFile("MorphoReader.dll")
    clr.AddReferenceToFile("MorphoRhino.dll")
    from MorphoReader import GridOutput, Direction, Facade
    from MorphoRhino.RhinoAdapter import RhinoConvert

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D/3D"

def get_file_path(path):

    file, extension = os.path.splitext(path)

    return path, file + ".EDX"


def main():

    if _edt:

        edt, edx = get_file_path(_edt)

        variable = 1 if _variable_ == None else _variable_
        index = 0 if _index_ == None else _index_

        if _dir_ == 1:
            dir = Direction.Y
        elif _dir_ == 2:
            dir = Direction.Z
        else:
            dir = Direction.X

        if base_point_:
            origin = RhinoConvert.FromRhPointToVector(base_point_)
            output = GridOutput(edx, origin)
        else:
            output = GridOutput(edx)

        project_name = output.ProjectName
        date = output.SimulationDate
        time = output.SimulationTime
        variables = output.VariableName
        selected_variable = output.VariableName[variable]

        print("Grid: {0}, {1}, {2}".format(output.NumX, output.NumY, output.NumZ))

        if output.DataContent not in [1, 2, 3, 5, 8, 11]:
            print("Please, connect an edt file.\nYou can read Atmosphere, Surface, Soil, Radiation, SolarAccess, Comfort")
            return [None] * 7

        if _run_it:

            # Surface
            if output.DataContent in [2, 8]:
                index = 0
                dir = Direction.Z
            elif output.DataContent == 3:
                dir = Direction.Z

            facades = output.GetFacades(dir)
            output.SetValuesFromBinary(edt, facades, variable)

            # filter
            facades = Facade.GetSliceByPixelCoordinate(facades, index, dir)
            if min_ != None and max_ != None:
                facades = Facade.GetFacadesByThreshold(facades, min_, max_)

            # get values from facades
            values = Facade.GetValueZFromFacades(facades)

            # get geometry
            face = Facade.GetFacesFromFacades(facades)

            return variables, project_name, date, time, selected_variable, face, values

        return variables, project_name, date, time, selected_variable, None, None
    else:
        return [None] * 7


if not _edt:
    print("Please, connect a compatible _edt and check available outputs using panels.")
else:
    variables, project_name, date, time, selected_variable, face, values = main()
