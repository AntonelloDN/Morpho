# Morpho: A plugin to write Envimet 2.5D models.
# This file is part of Morpho project.
#
# Copyright (c) 2020, Antonello Di Nunzio <antonellodinunzio@gmail.com>.
# You should have received a copy of the GNU General Public License
# along with Morpho project; If not, see <http://www.gnu.org/licenses/>.
# 
# @license GPL-3.0+ <http://spdx.org/licenses/GPL-3.0+>

"""
Read XML material and get values to check them.
-
This component parses XML from strings and show text by keywords.
    Args:
        _XML: XML content from Library component.
        _write_it: A list of tag of the XML file you are reading.\nKeyword is a word of the XML file between <>.
    
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
import xml.etree.ElementTree as ET
from Grasshopper.Kernel.Data import GH_Path
from Grasshopper import DataTree

ghenv.Component.Message = "1.0.0 2.5D"

def main():
    
    if _XML and _keyword:
        
        roots = [ET.fromstring(el) for el in _XML]
        results = DataTree[System.Object]()
        
        for i, key in enumerate(_keyword):
            path = GH_Path(0, i)
            results.AddRange( [root.find(key).text for root in roots if root.find(key) != None] ,path )
        
        return results
    else:
        return

results = main()
if not results: print("Please, connect _XML and _keyword.")