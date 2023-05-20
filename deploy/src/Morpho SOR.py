# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2023, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Active SOR mode. If you active it pressure field is calculated via red-black-tree algorithm which allows parallel computation of pressure field.
-
EXPERT SETTINGS.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _active: Set it to True [bool].
        
    Returns:
        read_me: Message for users.
        SOR: SOR settings of *.simx file.
"""

ghenv.Component.Name = "Morpho SOR"
ghenv.Component.NickName = "morpho_sor"
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
    from Morpho25.Settings import SOR, Active
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.1"

def main():
    sor = SOR()
    sor.SORMode = Active.YES if _active else Active.NO 
    
    return sor

SOR = main()