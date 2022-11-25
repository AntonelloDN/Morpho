# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Set Envimet Model.
-
Model structure of ENVI-Met.
. Workspace -> Project -> Model (inx)
    Args:
        _workspace_folder: Main folder where to save an Envimet project (e.g. C:\Example) [string].
        _project_name_: Name of the current project [string].
        _model_name_: Name of the model to write [string].
        _userDB_: Set it 'True' to user user database instead of project database.
        -
        Database structure of ENVI-Met.
        1. System database is part of envimet software. It contains standard materials.
        2. Project database is part of the project and it is custom.
        3. User database is shared among all projects that do not use project database.
        -
        If you use project database you cannot use user database and vice-versa.
        envimetFolder_: Envimet folder on your machine. If it does not recognize Envimet connect it (e.g. C:\ENVImet445).
    
    Returns:
        read_me: Message for users.
        inx_workspace: Inx Workspace.
"""

ghenv.Component.Name = "Morpho Workspace"
ghenv.Component.NickName = "morpho_workspace"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "1 || Settings"
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
    from Morpho25.Management import Workspace, DatabaseSource
    
except ImportError as e:
    raise ImportError("\nFailed to import Morpho: {0}\n\nCheck your 'Morpho' folder in {1}".format(e, os.getenv("APPDATA")))
################################################
ghenv.Component.Message = "1.1.0"

def main():
    
    db_type = DatabaseSource.Project if not _userDB_ else DatabaseSource.User
    
    if _workspace_folder:
        
        if _project_name_ and _model_name_:
            workspace = Workspace(_workspace_folder, db_type, _project_name_, _model_name_, envimetFolder_)
        else:
            workspace = Workspace(_workspace_folder, db_type, envimetFolder_)
        
        return workspace
    else:
        return

inx_workspace = main()
if not inx_workspace: print("Please, connect _workspace_folder.")