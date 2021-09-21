 
# Experimenting the mod for cities skylines

In this mode the goal was just exploring what can be done with mods for the game, so expect to have random stuff all over, 


# Download and run

- You can clone the repo or just download it as ZIP, open the ExtendedBuildings.sln with VS2019
- Hit build or CTRL+SHIFT+B this will place the built .dll into the game mods folder so the game would compile it in the run time `most of the time you woun't need to restart the game`
- the build should be automatically placed inside 
		`%LOCALAPPDATA%\Colossal Order\Cities_Skylines\Addons\Mods`

## How its created

Following the guide [here](https://magickaichen.com/create-your-first-mod-in-cities-skylines/)

#### Project Hierarchy

Generally, in Cities: Skylines, all mods will be placed at  `%LOCALAPPDATA%\Colossal Order\Cities_Skylines\Addons\Mods`  with a folder structure like following:

```
\Mods
└-- ModName
    |-- ModName.dll(Automatically Compiled)
    └-- \source
        └-- ModName.cs(source code)
```

#### For Autocompletion on VS2019
we need to add all of supported Cities: Skylines APIs to our dependency list:
-   ICities (`ICities.dll`)
-   UnityEngine (`UnityEngine.dll`)
-   ColossalFramework (`ColossalManaged.dll`)
-   Assembly-CSharp(`Assembly-CSharp.dll`)

Let's do it by right click the reference in Solution Manager (The tab on the upper right corner), then select "Add Reference", on the popup menu, use "Browse..." button to select those  `.dll`  files, which located at  
`C:\Program Files (x86)\Steam\steamapps\common\Cities_Skylines\Cities_Data\Managed`

 


## Register the mod in game
```c#
using UnityEngine;
using ICities;

namespace CitiesSkyLineModExperiment
{
	public class RCWDemandMod : IUserMod
	{
		public string Name => "Here goes the Mod name";

		public string Description => "Here goes the Mod description";
	}
	/// <summary>This method Is called to create the mod setting UI 
	/// Options -> Mods Settings
	/// here is an example for creating a chechbox 
	/// </summary>
	public void OnSettingsUI(UIHelperBase helper)
    {
            helper.AddCheckbox("Some bool to be used in something", defaultValue, CallbackFunction);          
    }
	private void CallbackFunction(bool newValue)
	{
		// this function will be called once the check box is clicked
	}
}
```

doing so then build will add a mod into the game under  **Content** **Manager** **->** **Mods**

## Do something once the level is loaded 

```c#
	public class ToBeExecutedOnLeveLLoad : LoadingExtensionBase
	{
		public override void OnLevelLoaded(LoadMode mode)
		{
			base.OnLevelLoaded(mode);
			Debug.Log(this.ToString());
		}
	}
```

## To be called OnUpdate


```c#
	public class ShowDemand : ThreadingExtensionBase
	{	 
		public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
		{
			 // this will be called every frame 
		}
	}
```
## Listen to building creation

```c#
public class MyBuildingExtension : IBuildingExtension
{
		public MyBuildingExtension() 
        {
		}
		public  void OnBuildingCreated(ushort id)
        {		 
        }

        public void OnBuildingReleased(ushort id)
        {			 
        }

        public void OnCreated(IBuilding building)
        {             		 
		}

        public SpawnData OnCalculateSpawn(Vector3 location, SpawnData spawn)
        {		
        }

        public void OnReleased()
        {		 
		}

		public void OnBuildingRelocated(ushort id)
        {			 
		}
}
```


# Add other plugins 
Given our experiment The Mod is used as .dll file, we had trouble including any pre-compled .dll into the game that does not contain the game base classes from `ICities` name space. so the way we can use another plugin into the mod is to add it's .cs scripts into the solution then package it with the mod .dll 

just like we included the `TotalJSON` [asset store](https://assetstore.unity.com/packages/tools/input-management/total-json-130344) into this mod .
