# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2023, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Force threading.
-
EXPERT SETTINGS. It can cause instabilities.
-
Icon made by Freepik <https://www.flaticon.com/authors/freepik>.
See license for more details.
    Args:
        _thread_priority_: A number from 1 to 31 [int].
        -
        Warning.
        It can cause instabilities in case of many simulations.
    Returns:
        read_me: Message for users.
        t_thread: Threading settings of *.simx file.
"""

ghenv.Component.Name = "Morpho Thread"
ghenv.Component.NickName = "morpho_thread"
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
    from Morpho25.Settings import TThread, Active
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.1"

def main():
    t_thread = TThread()
    t_thread.UseTreading = Active.YES
    if _thread_priority_: t_thread.TThreadpriority = _thread_priority_
    
    return t_thread

t_thread = main()