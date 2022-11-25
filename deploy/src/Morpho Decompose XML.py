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
import xml.etree.ElementTree as ET
from Grasshopper.Kernel.Data import GH_Path
from Grasshopper import DataTree
import re

ghenv.Component.Message = "1.1.0"



def get_clean_xml(text):
    
    characters = '[^\s()_<>/,\.A-Za-z0-9=""]+'
    text = re.sub(characters, '', text)
    return text


def main():
    
    if _XML and _keyword:
        
        roots = [ET.fromstring(get_clean_xml(el)) for el in _XML]
        results = DataTree[System.Object]()
        
        for i, key in enumerate(_keyword):
            path = GH_Path(0, i)
            results.AddRange( [root.find(key).text for root in roots if root.find(key) != None] ,path )
        
        return results
    else:
        return

results = main()
if not results: print("Please, connect _XML and _keyword.")