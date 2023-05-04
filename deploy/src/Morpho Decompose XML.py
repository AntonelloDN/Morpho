# Morpho: A plugin to write Envimet models.
# This file is part of Morpho project.
#
# Copyright (c) 2022, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license AGPL-3.0-or-later <https://spdx.org/licenses/AGPL-3.0-or-later>

"""
Read XML material and get values to check them.
-
This component parses XML from strings and show text by keywords.
    Args:
        _XML: XML content from Library component.
        _keyword: A list of tag of the XML file you are reading.\nKeyword is a word of the XML file between <>.
    
    Returns:
        read_me: Message for users.
        results: InnerText of selected keyword.
"""

ghenv.Component.Name = "Morpho Decompose XML"
ghenv.Component.NickName = "morpho_decompose_xml"
ghenv.Component.Category = "Morpho"
ghenv.Component.SubCategory = "5 || Util"
try: ghenv.Component.AdditionalHelpFromDocStrings = "1"
except: pass

import System
from Grasshopper.Kernel.Data import GH_Path
from Grasshopper import DataTree
import re
import clr
clr.AddReference("System.Linq")
clr.AddReference("System.XML.Linq")
from System.Xml.Linq import XDocument

ghenv.Component.Message = "1.1.0"

def main():
    
    if _XML and _keyword:
        
        xdocs = [XDocument.Parse(el) for el in _XML]
        results = DataTree[System.Object]()
        
        for i, key in enumerate(_keyword):
            path = GH_Path(0, i)
            results.AddRange( [element.Value for xdoc in xdocs for element in xdoc.Descendants(key) ] ,path )
        
        return results
    else:
        return

results = main()
if not results: print("Please, connect _XML and _keyword.")