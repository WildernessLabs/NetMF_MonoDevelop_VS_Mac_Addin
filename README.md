This is the add-in for Visual Studio Mac, Xamarin Studio, and MonoDevelop that enables [.Net MicroFramework](http://www.netmf.com/) projects to be built on Mac and Linux. 

## Installation 

To install the add-in:

 1. Install the latest version of either Visual Studio for Mac, or [Xamarin Studio/MonoDevelop](http://www.monodevelop.com/download/)
 2. Install the add-in from the extensions/add-ins manager:
 3. Open VS Mac/Xamarin Studio and open the **Visual Studio/Xamarin Studio** > **Extensions/Add-ins** menu and select the **Gallery** tab.
 4. Select **All repositories** and Search for `Micro` (you may have to click the **Refresh** button.
 5. Select the MicroFramework add-in and then click **install** on the right.
 6. Follow installation instructions.


## Building the Add-in from Source

 1. Install the latest version of either Visual Studio for Mac, or [Xamarin Studio/MonoDevelop](http://www.monodevelop.com/download/)
 2. Install the **AddinMaker** maker from the extensions/add-ins manager (follow installation instructions 3-6 above, but search for `AddinMaker` instead).
 3. Clone project from GitHub and open the `MonoDevelop.MicroFramework.sln` solution with VS Mac/Xamarin Studio.
 4. Make sure all the nuget packages are restored and then start debugging, which will open a new instance of VS Mac/Xamarin Studio that has the MicroFramework Add-in that just built enabled. 

## Authors

The add-in was originally authored by David Karlas. The original codebase can be found [here](https://github.com/davidkarlas/MonoDevelop.MicroFramework).

## Contributors

Jeremie Laval

