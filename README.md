# trashcat

This repository contains a modified version (without the tutorial) of the TrashCat Unity project for the [Endless Runner - Sample Game](https://assetstore.unity.com/packages/templates/tutorials/endless-runner-sample-game-87901)

## Adding the AltTester Unity SDK submodule to the project
- use ``git submodule update --init`` command to pull the git submodule;
- make sure that the submodule added is on the master branch (you can use the following command ``git checkout master`` in the <i>Assets/AltTester-Unity-SDK</i> folder);
- also, if you already have the project, you should make a ``git pull`` on the master branch, in order to ensure that you are using the latest version of AltTester.

## Before creating a build instrumented with AltTester Unity SDK or playing in the AltTester Editor window

### Build addressables

The assets of this project are structured as an [Addressable Asset System](https://docs.unity3d.com/Manual/com.unity.addressables.html). This calls for a separate Addressable build, before building the application itself. Just select ``Default Build Script`` to start it.

- Unity Editor -> ``Window`` -> ``Asset Management`` -> ``Addressables`` -> ``Groups``
- Select from ``Build`` -> ``New Build`` -> ``Default Build Script``

#### Notes

There might be a situation when building on iOS that in XCode there is an error for ``Redefinition of 'AllocCString'``.This is not related to AltTester though. 

- In this case the solution is to comment the line ``#import <Unity/UnityInterface.h>`` at the beggining of these files generated in the build folder
    - ``../YourAppFolder/YourAppName/Libraries/com.unity.ads/Plugins/iOS/UnityAdsUnityWrapper.m``
    - ``../YourAppFolder/YourAppName/Libraries/com.unity.ads/Plugins/iOS/UnityMonetizationDecisionWrapper.m``