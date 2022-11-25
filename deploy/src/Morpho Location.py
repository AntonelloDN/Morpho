# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Construct a Inx Location.
-
You can decompose location data from other plugin, such as Ladybug and Gismo!
    Args:
        _name_: Name of Location.
        _latitude: Latitude of Location WGS84 [float].
        _longitude_: Longitude of Location WGS84 [float].
        _time_zone_: UTC timezone of location [integer].
        _model_rotation_: Rotation angle of your model [float].
        utm_: UTM object from "Morpho UTM" [UTM].
        _longitude_reference_: Longitude reference of your Location. Default is 15.0. [float].

    Returns:
        read_me: Message for users.
        inx_location: Inx Location.
"""

ghenv.Component.Name = "Morpho Location"
ghenv.Component.NickName = "morpho_location"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "1 || Settings"
try: ghenv.Component.AdditionalHelpFromDocStrings = "1"
except: pass

import scriptcontext as sc
import os
import sys
import clr
################### Morpho #####################
try:
    user_path = os.getenv("APPDATA")
    sys.path.append(os.path.join(user_path, "Morpho"))
    clr.AddReferenceToFile("Morpho25.dll")
    from Morpho25.Settings import Location

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():

    if _latitude and _longitude:

        location = Location(_latitude, _longitude)

        if _name_:
            location = Location(_latitude, _longitude, _name_)
        if _name_ and _time_zone_:
            location = Location(_latitude, _longitude, _name_, str(_time_zone_))
        if _name_ and _time_zone_ and _model_rotation_:
            location = Location(_latitude, _longitude, _name_, str(_time_zone_), _model_rotation_)

        if _model_rotation_: location.ModelRotation = _model_rotation_
        if utm_: location.UTM = utm_
        if _longitude_reference_: location.TimezoneReference = _longitude_reference_

        return location
    else:
        return

inx_location = main()
if not inx_location: print("Please, connect latitude and longitude.")
