# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
#
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Construct an Inx Receptor.
    Args:
        _inx_grid: Inx Grid.
        _inx_point: Inx Point.
        _name_: Optional name to give to receptor group [string].

    Returns:
        read_me: Message for users.
        inx_receptor: Inx Receptor to use as input of 'Morpho Model' component.
"""

ghenv.Component.Name = "Morpho Receptor"
ghenv.Component.NickName = "morpho_receptor"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "2 || Entity"
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
    from Morpho25.Geometry import Receptor

except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.0.1 2.5D"

def main():
    receptors = []
    if _inx_grid and _inx_point:

        receptors = [Receptor(_inx_grid, vec, _name_) for vec in _inx_point]

        return receptors
    else:
        return

inx_receptor = main()
if not inx_receptor: print("Please, connect _inx_grid, _inx_point.")
