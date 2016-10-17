# Ignite 2016
This repository contains all documentation and code samples referenced by and used in the "Building, Testing and Deploying a Browser Extension in Microsoft Edge" (M357) presentation given on October 27, 2016 by Scott Low at Ignite NZ 2016

For any specific questions, feel free to reach out to [@MSEdgeDev](https://twitter.com/MSEdgeDev) or [@_scottlow](https://twitter.com/_scottlow) on Twitter.

## Repository Index
This repository contains several directories and files that correspond to various portions of the presentation:

1. [Ignite 2016 Extension Demo](./Ignite 2016 Extension Demo): This directory contains the **unconverted** Chrome extension with all request counting code commented out. To add the request counting code, please add `"default_popup": "popup/popup.html"` to the `"browserAction"` section of [manifest.json](./Ignite 2016 Extension Demo/manifest.json) and uncomment the commented lines of code in [background.js](./Ignite 2016 Extension Demo/background.js)
2. [Ignite 2016 Extension Demo Fixed](./Ignite 2016 Extension Demo Fixed): This directory contains the **converted** Edge extension that has been fixed to include the workaround for the [Edge bug](https://developer.microsoft.com/en-us/microsoft-edge/platform/issues/8473140/) this extension exposed.
3. [Ignite 2016 WebDriver Demo](./Ignite 2016 WebDriver Demo): This directory contains a Visual Studio project that demonstrates how an extension can be sideloaded in Microsoft Edge using Microsoft WebDriver.
4. [manifold.txt](./manifold.txt): This is a text file that contains the scaffolding and packaging commands necessary to create an extension AppX using manifoldjs
5. [Scott Low - Building Extensions in Microsoft Edge.pptx](./Scott Low - Building Extensions in Microsoft Edge.pptx): This is slide deck that was presented at Ignite NZ.

## Building/Porting an Extension
The following links will be useful for getting started with building an extension and porting it to Edge:

1. [Google's getting started with extensions in Chrome documentation](https://developer.chrome.com/extensions/getstarted)
2. [Microsoft's Edge specific extension documentation](http://aka.ms/ext-docs)
3. [Microsoft Edge Extension Toolkit](http://aka.ms/ext-porting): A Windows Store application that automatically fixes manifest files and polyfills APIs to convert Chrome extensions to Edge extensions.
4. [Microsoft Edge Issue Tracker](http://aka.ms/edge-issues): Allows developers to file bugs (now including extension bugs) against Microsoft Edge that are sent directly to engineers. 

## Loading Extensions in WebDriver
The following section briefly describes how to sideload an extension in Microsoft Edge with Microsoft WebDriver.
### Prerequesites
In order to load an extension in Microsoft Edge with WebDriver, you will need to download and install the following:

1. The correct [Selenium language binding](http://www.seleniumhq.org/download/) for your test harness 
2. The [latest version of Microsoft WebDriver](https://www.microsoft.com/en-us/download/details.aspx?id=48212). Please ensure that this is located in a directory included in your `PATH` environment variable.

### Sideloading
As discussed in the presentation, while this feature is available on Anniversary Update builds, it is currently undocumented. Please refer to [Program.cs](./Ignite 2016 WebDriver Demo/IgniteWebDriverDemo/Program.cs) in the included Visual Studio project for an example of how to sideload an extension in Microsoft Edge with WebDriver. Note that you will need to change the `extensionPath` variable in that code sample to point to an extension directory on your machine. 

## Packaging an Extension
The following section describes how to package an extension regardless of platform using manifoldjs.

### Prerequesites
1. Install the latest version of [Node.js](https://nodejs.org/en/). Ensure that the option to install NPM is checked as well.
2. Install the latest version of [manifoldjs](http://manifoldjs.com/) by running `npm install -g manifoldjs`

## Packaging
To package an extension and create an AppX, please follow the steps below (make sure to run any commands from within a directory where you want the AppX to be created):

1. Run the scaffolding command: `manifoldjs -l debug -p edgeextension -f edgeextension -m "<EXTENSION LOCATION>\manifest.json`
2. Edit the generated `appxmanifest.xml` file inside the created `manifest` directory and replace any generated values with their actual values in DevCenter
3. Run the packaging command: `manifoldjs -l debug -p edgeextension package "<GENERATED FOLDER>\edgeextension\manifest\"`

Note: If you are packaging an extension for internal (Enterprise) distribution only, you do not need to fill in the `Identity` section with values from DevCenter. The only requirement is that the `Publisher` property in your `AppXManifest.xml` be the same as the `Publisher` property used to generate the certificate that will be used to sign the AppX after its creation. For more information, see the "Testing the Generated AppX (with signing)" section of this document below.

### Testing the Generated AppX (without signing)
To unpack and test your generated AppX without signing it, please follow the steps below:

1. Run `makeappx unpack /v /p edgeExtension.appx /d "<UNPACK DIR>"`
2. `cd` to `<UNPACK DIR>`
3. Run `powershell "Add-AppxPackage -register AppXManifest.xml"`

### Testing the Generated AppX (with signing)
To test your generated AppX by signing and loading it into Edge, please follow the steps below:

Refer to MSDN articles on [How to create an app package signing certificate](https://msdn.microsoft.com/en-us/library/windows/desktop/jj835832(v=vs.85).aspx) and [How to sign an app package using SignTool](https://msdn.microsoft.com/en-us/library/windows/desktop/jj835835(v=vs.85).aspx)

Once you’ve created the Edge Extension AppX package, you may want to side load the package for testing on Microsoft Edge. Sideloading the Edge Extension AppX package is similar to side loading a Universal Windows app. There two steps required – creating a certificate for signing the package and adding the package to Windows. 

**To install certificates with WindowsCertutil.exe**

1. Run `Cmd.exe` as administrator. 
2. Run `Certutil -addStore TrustedPeople MyKey.cer` (To remove the cert, run `Certutil -delStore TrustedPeople certID` where `certID` is the serial number of the certificate. To determine the certificate serial number, run `Certutil -store TrustedPeople`
3. You can deploy the Edge Extension AppX package by running the following command in PowerShell (as administrator): `Add-AppxPackage <path to AppX>`
