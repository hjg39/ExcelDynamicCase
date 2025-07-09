# Excel Dynamic Case

This is a C# .NET Framework VSTO Excel Workbook and joint Unity project for Windows.
It is a basic demo for an Excel battle case with an overworld that you can navigate in, and where you can unlock the use of new functions as you go.

## 🚀 Quick Start (Run the App)

If you want to run the pre-built version of this tool without building the solution yourself (i.e. just play the game without editing it):

0. If you have installed a previous version of the project, go to 'Add or remove programs > ExcelDynamicCase' and uninstall it.  (This will not remove any save data.)
1. **Download the repository (Code > Download ZIP).
2. Right click on the downloaded zip file, select 'Properties' and then select 'Unblock'
3. Unzip the zip file to a folder (to a non-cloud location, cloud locations like places automatically backup up to onedrive will not work)
4. Open a blank instance of Excel
5. File > Options > Trust Centre > Trust Centre Settings > Trusted Locations
6. Add the 'publish' subfolder (ExcelDynamicCase/publish) to your trusted locations
7. Close all your Excel instances
8. Go to the publish subfolder
9. Right click on ExcelDynamicCase.vsto -> properties -> unblock (if available)
10. Right click on ExcelDynamicCase.xlsx -> properties -> unblock (if available)
11. Open the xlsx
12. Click 'Install'
13. Enjoy!
14. (Optionally when you are done, you can remove the installation from 'add or remove programs > ExcelDynamicCase' remove

## 🔧⚙ Troubleshoot Installation

1. 'Deployment and application do not have matching security zones.' - This occurs if the zip was not unblocked correctly in step 2.  If you fix this and it still fails, restarting and/or extracting to an already Trusted location has been known to work although I'm not sure why.
2. 'This document contains custom code that cannot be loaded because the location is not in your trusted locations list.' - If you did add it to your trusted locations, this may be because it is a cloud folder (see step 3).
3. 'This document might not function as expected because the following control is missing.' - Close Excel instances, run 'setup.exe' in the publish folder and reopen the xlsx.
4. In some cases, if you host the application in a location with a long directory/folder path, the unzipping may drop some files when extraction is happening.  If this happens, try moving the application to a shorter folder location.  If you  need to you can also move just the 'publish' folder out to a higher level with a shorter folder path also.

## ⟳ Starting A New Game/Save File 

Navigate to '%AppData%/LocalLow/HarryGross' in File Explorer and delete the 'Excelopolis' folder.  You may need to enable 'Hidden items' in File Explorer to find the AppData folder, but it should be a subfolder of your user folder.

## 🛠️ Developer Setup (Build from Source)

If you want to build or modify the source code:

### Requirements

- Visual Studio 2022 or later (with .NET 4.8 targeting pack, .net2.0 targeting pack and appropriate VSTO build tools, Unity build tools and configuration)
- Git (optional but recommended)
- Unity with editor 2021.3.11f1 (Open RPGTemplate folder)
- The PipelineToUnity project debug dll and dependency Newtonsoft dll need to copied over the corresponding asset plugin when updated.

## 𝄠 Music Credits

Zame (Twinleaf Town: Remastered), (Battle! Mercury (Barry's Ancestor))  
https://www.youtube.com/@The_Zame  
https://www.patreon.com/join/TheZame  
https://www.youtube.com/watch?v=j9iqC5-xAAM  
https://www.youtube.com/watch?v=Qsk0rCuuRso  

Kevin Grim (BATTLE! Cynthia)  
https://www.youtube.com/@kevingrim  
https://soundcloud.com/kevin_grim  
https://www.youtube.com/watch?v=YtB8HTv6xTI
