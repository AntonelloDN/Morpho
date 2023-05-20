# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Advance radiation transfer schema to use.
IVS allows a detailed analysis and calculation of shortwave and longwave radiation fluxes with taking into account multiple interactions between surfaces.
-
EXPERT SETTINGS.
-
REMINDER: It is not supported in BASIC version.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _IVS_height_angle_high_res: Height angle for IVS calculation [int]. You can use 2, 5, 10, 15, 30, 45.
        _IVS_azimut_angle_high_res: Height angle for IVS calculation (low resolution)n [int]. You can use 2, 5, 10, 15, 30, 45.
        _radiation_height_boundary_: Height cap in meters above ground below which higher precision is used.
        low_resolution_: Raytracing precision. True is low, False is high.
        ADV_canopy_rad_transfer_: Advance canopy radiation transfer module. True is enable, False is disable.
        view_factor_interval_: Update interval for View Factor calculation of the canopy radiation transfer [int]. Default is 10.
        MRT_calc_method_: MRT Calculation method.
        0 - TwoDirectional
        1 - SixDirectional
        MRT_projection_method_: MRT Projection method to human body
        0 - Envimet
        1 - Solweig
        2 - Rayman
        3 - CityComfort
        
    Returns:
        read_me: Message for users.
        rad_scheme: rad_scheme settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Radiation"
ghenv.Component.NickName = "morpho_radiation"
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
    from Morpho25.Settings import RadScheme, Active, MRTProjectionMethod, MRTCalculationMethod
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    proj_methods = [
        MRTProjectionMethod.Envimet,
        MRTProjectionMethod.Solweig,
        MRTProjectionMethod.Rayman,
        MRTProjectionMethod.CityComfort,
    ]
    
    calc_method = [
        MRTCalculationMethod.TwoDirectional,
        MRTCalculationMethod.SixDirectional,
    ]
    
    rad_scheme = RadScheme()
    if _IVS_height_angle_high_res and _IVS_azimut_angle_high_res: rad_scheme.IVSHeightAngleHighRes = _IVS_height_angle_high_res
    if _IVS_azimut_angle_high_res and _IVS_height_angle_high_res: rad_scheme.IVSAzimutAngleHighRes = _IVS_azimut_angle_high_res
    if _radiation_height_boundary_: rad_scheme.RadiationHeightBoundary = _radiation_height_boundary_
    rad_scheme.LowResolution = Active.YES if low_resolution_ else Active.NO
    rad_scheme.AdvCanopyRadTransfer = Active.YES if ADV_canopy_rad_transfer_ else Active.NO
    if view_factor_interval_: rad_scheme.ViewFactorInterval = view_factor_interval_
    if MRT_projection_method_: rad_scheme.MRTProjectionMethod = proj_methods[MRT_projection_method_]
    if MRT_calc_method_: rad_scheme.MRTCalculationMethod = calc_method[MRT_calc_method_]
    
    return rad_scheme

rad_scheme = main()