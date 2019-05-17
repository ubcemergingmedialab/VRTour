# VRTour
To make a new tour, please use the [VRTour Builder](https://github.com/ubcemergingmedialab/VRTourBuilder).
This project allows users to quickly and easily import premade VR tours of a space into Unity. Using this, in conjunction with the VRTour Builder, users can make custom tours and choose-your-own adventure exploration games.

# Setting up a Tour
## Prerequisites
* Unity, Tested with version 2017.4
* Vive Input Utility

## Installing
1. Download the latest release from our github. 
2. In Unity, go to `Assets > Import New Asset > Custom Package`

## Importing a Tour
If you've used the VRTour Builder, all you need is the unique code your tour was saved under. Otherwise, you'll need a text file with the configured tour. 
1. If you are using our premade tour settings, select the premade `Tour1` asset in the project window. Otherwise, see the below section on making a custom tour.
2. In the inspector window, ensure "Load from Online ID?" is checked. Put in your unique tour code in the "Tour ID" box.
2.a. Otherwise, check the "Load from static file?" box and drag in the file to use.
3. Click Load Tour. You should see confirmation that the tour was loaded.
4. Click Build Tour to have the loaded tour stored in the scene. 

## Building a Custom Tour - TODO
