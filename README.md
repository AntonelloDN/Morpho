![Logo](https://github.com/AntonelloDN/Morpho/blob/master/logo/logo_morpho_32.png)
# Morpho
A plugin to create Envimet 2.5D models (INX), write configuration files (SIMX) and run simulation.<br>
It is based on following projects: lb_envimet, df_envimet and Envimet INX.
It contains a library of classes you can use with **Grasshopper**, **Dynamo** and other softwares.
## Requirements
* ENVI-met 4.4.5
## Installation:
1. Download Morpho.
2. Check if downloaded .zip file has been blocked: right click on it, and choose Properties. If there is an Unblock button click on it, otherwise it is OK. Unzip it.
3. Follow 'README.txt' instructions.
## Dependencies:
* Morpho25.dll (for Rhino and Dynamo)
* MorphoRhino.dll (for Rhino only)
* geometry3sharp
## Software tested
* Rhino 6
* Dynamo for Revit 2021
## Features:
* Settings of Workspace, project and model
* Modeling of envimet entities for 2.5D INX. Such as Buildings, Plant2D and Soils
* Reading materials from system DB, project DB and user DB
* Settings of simulation file (SIMX) with more than 15 advanced settings. Both Simpleforcing and Fullforcing supported.
* Running envimet simulation :warning: there is a small defect in envimet_console of ENVI-met 4.4.5. If simulation does not start correctly  use envimet ui to run simulation.
## Improvements
* it is possible to use class library with other softwares: only requirement is translate input geometries in DMesh3 and Vector3d of geometry3sharp
* integration with ShrimpGIS and Gismo
* Grid settings is based on a single point, user specify how many grid cells to use in x, y and z.
* User experiece of modeling of 3D trees. Geometries are points
## Limits v.1.0.0:
* Reading part not added (I was planning to release it in another plugin)
* It is like Monde of ENVI-met: it does not use detail 3D mode
## To do:
* Add documentation string of C# library
* Release scripts for Dynamo to simplify DMesh3 creation
