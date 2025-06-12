# Excel Dynamic Case

This is a C# .NET Framework VSTO Excel Workbook and joint Unity project that is a basic demo for an Excel battle case with an overworld that you can navigate in, and where you can unlock the use of new functions as you go.

## 🚀 Quick Start (Run the App)

If you just want to run the pre-built version of this tool without building the solution yourself:

0. If you have installed a previous version of the project, go to 'Add or remove programs > ExcelDynamicCase' and uninstall it.
1. **Download the repository (Code > Download ZIP).
2. Right click on the downloaded zip file, select 'Properties' and then select 'Unblock'
3. Unzip the zip file to a folder (to a non-cloud location, cloud locations like places automatically backup up to onedrive will not work)
4. Open a blank instance of Excel
5. File > Options > Trust Centre > Trust Centre Settings > Trusted Locations
6. Add the 'publish' subfolder (ExcelDynamicCase/publish) to your trusted locations
7. Close all your Excel instances
8. Go to the publish subfolder and open the xlsx
9. Click 'Install'
10. Enjoy!
11. (Optionally when you are done, you can remove the installation from 'add or remove programs > ExcelDynamicCase' remove

## 🔧⚙ Troubleshoot Installation
'Deployment and application do not have matching security zones.' - This occurs if the zip was not unblocked correctly in step 2.
'This document contains custom code that cannot be loaded because the location is not in your trusted locations list.' - If you did add it to your trusted locations, this may be because it is a cloud folder (see step 3).
'This document might not function as expected because the following control is missing.' - Close Excel instances, run 'setup.exe' in the publish folder and reopen the xlsx. 

---

## 🛠️ Developer Setup (Build from Source)

If you want to build or modify the source code:

### Requirements

- Visual Studio 2022 or later (with .NET 4.8 targeting pack, .net2.0 targeting pack and appropriate VSTO bulid tools and configuration)
- Git (optional but recommended)
- Unity (version # to be specified)
- The PipelineToUnity project debug dll and dependency Newtonsoft dll need to copied over the corresponding asset plugin
