![Logo](https://github.com/AntonelloDN/Morpho/blob/master/logo/logo_morpho_32.png)
[![Version](https://img.shields.io/nuget/v/Morpho.Envimet.svg?color=royalblue)](https://www.nuget.org/packages/Morpho.Envimet)
[![Downloads](https://img.shields.io/nuget/dt/Morpho.Envimet.svg?color=green&label=package%20downloads)](https://www.nuget.org/packages/Morpho.Envimet)
[![License](https://img.shields.io/github/license/AntonelloDN/Morpho.svg?color=blue)](https://github.com/AntonelloDN/Morpho/blob/master/LICENSE)
[![Build](https://github.com/AntonelloDN/Morpho/workflows/NUGET/badge.svg?branch=package)](https://github.com/AntonelloDN/Morpho/actions/workflows/package.yml)
[![Build](https://github.com/AntonelloDN/Morpho/workflows/CD/badge.svg?branch=master)](https://github.com/AntonelloDN/Morpho/actions/workflows/release.yml)
[![Github All Releases](https://img.shields.io/github/downloads/AntonelloDN/Morpho/total.svg?color=white&label=release%20downloads)]()
![Example](https://github.com/AntonelloDN/Morpho/blob/master/images/morpho_read_results_00.PNG)

# Morpho
A plugin to create Envimet 2.5D and 3D models (INX), write configuration files (SIMX), run simulation and read results.<br>
It is based on following projects: lb_envimet, df_envimet and Envimet INX.
It contains a library of classes you can use with **Grasshopper**, **Dynamo** and other softwares.

This project is partially financed by ENVI_met GmbH
## Requirements
* [ENVI-met 5](https://www.envi-met.com/buy-now/)
## Installation:
1. Download Morpho.zip from [Releases](https://github.com/AntonelloDN/Morpho/releases)
2. Check if downloaded .zip file has been blocked: right click on it, and choose Properties. If there is an Unblock button click on it, otherwise it is OK. Unzip it.
3. Follow 'README.txt' instructions.
## Video
* [Playlist](https://www.youtube.com/playlist?list=PLVk71QLjaA6PES3DFI37t5iAUSC7lgIpJ)
## Dependencies
* [Morpho.Envimet](https://www.nuget.org/packages/Morpho.Envimet)

## Software tested
* Rhino 6, Rhino 7
* Dynamo for Revit 2021
## Features:
* Settings of Workspace, project and model
* Modeling of envimet entities for 2.5D and 3D INX. Such as Buildings, Plant2D and Soils
* Reading materials from system DB, project DB and user DB
* Settings of simulation file (SIMX) with more than 15 advanced settings. Both Simpleforcing and Fullforcing supported.
* Running envimet simulation
* Reading binary output files of envimet (EDT): Atmosphere, Soil, Surface, Buildings, Vegetation, SolarAccess, Radiation
## Improvements:
* it is possible to use class library with other softwares: only requirement is translate input geometries in Facegroup and Vector of MorphoGeometry
* integration with ShrimpGIS and Gismo
* Grid settings is based on a single point, user specify how many grid cells to use in x, y and z.
* User experiece of modeling of 3D trees. Geometries are points
* Flexible way to read EDT files
* It generates 2.5D and 3D models.
## Limits:
* 1D results reader and receptors reader need to be add
## Roadmap
- [x] Components for Grasshopper
- [x] Solve geometry3sharp intersection performance issue with big meshes - MorphoGeometry! :muscle::
- [x] 5 Example files for Grasshopper
- [x] Create DEM using directly an ASCII matrix
- [x] Added UTM for georeference.
- [x] Add DLL and components to read EDT EDX. Almost done.
- [x] Add documentation string of C# library
- [ ] Release nodes for Dynamo for geometry. Dynamo development is on pause. If you are a dynamo developer feel you free to improve Morpho
