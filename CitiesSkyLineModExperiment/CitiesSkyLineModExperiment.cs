using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ICities;
using ColossalFramework.UI;
using Leguar.TotalJSON;
using Leguar.TotalJSON.Examples;
using ColossalFramework.Plugins;

/*
this project was made following the guide here
 https://magickaichen.com/create-your-first-mod-in-cities-skylines/
 the skyline APIs dependecy list should be at 
        C:\Program Files (x86)\Steam\steamapps\common\Cities_Skylines\Cities_Data\Managed
after the build the mod should be automatically placed inside 
		%LOCALAPPDATA%\Colossal Order\Cities_Skylines\Addons\Mods

 */
namespace CitiesSkyLineModExperiment
{
	public class RCWDemandMod : IUserMod
	{
		public string Name => "RCW Demand";

		public string Description => "Show actual RCW Demands in numbers.";

		public void f()
		{
			 
		}

	}

	public class ToBeExecutedOnLeveLLoad : LoadingExtensionBase
	{
		public override void OnLevelLoaded(LoadMode mode)
		{
			base.OnLevelLoaded(mode);
			Debug.Log(this.ToString());
		}
	}

	public class ShowDemand : ThreadingExtensionBase
	{
		private string SerializePlayerObjectToString()
		{

			// Create example player (c# object)
			ExamplePlayerObject examplePlayer = new ExamplePlayerObject();
			examplePlayer.SetTestValues();

			// Print out current player data
			Debug.Log("Original player: " + examplePlayer);

			// Serialize ExamplePlayerObject to JSON object
			JSON json = JSON.Serialize(examplePlayer);

			// Output JSON
			string jsonString = json.CreateString();
			Debug.Log(jsonString);

			// Content of 'jsonString' will be:
			// {"name":"Test player","position":{"x":1.0,"y":2.0,"z":3.0},
			// "playerColor":{"r":0.0,"g":1.0,"b":0.1,"a":0.9},"score":42000,"levelTimes":[31.41,42.0,12.3],
			// "playerBackPack":[{"name":"axe","uses":99},{"name":"coin","uses":1}],"charClass":{"value__":1}}

			return jsonString;

		}


		private bool _processed = false;
		public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
		{
			Debug.Log(_processed);
            if (Input.GetKeyDown(KeyCode.F11))
            {
				Debug.Log("---> Running SerializeAndDeserialize.SerializePlayerObjectToString()");
				DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "---> Running SerializeAndDeserialize.SerializePlayerObjectToString()");
				string jsonString = SerializePlayerObjectToString();
				DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, jsonString);

				GameObject _buildingManagerGO = GameObject.Find("BuildingManager");
                if (_buildingManagerGO)
                {
					Debug.Log("bingo _buildingManagerGO");
					BuildingManager _bm = _buildingManagerGO.GetComponent<BuildingManager>();
                    if (_bm)
                    {
						Debug.Log("bingo _bm");
						Debug.Log(_bm.m_buildings.m_buffer.ToString());
                    }
                    else
                    {
						Debug.Log("_bm = null");
					}
				}
                else
                {
					Debug.Log("_buildingManagerGO = null");
				}
				Debug.Log("f12 is pressed");
            }

			if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.D))
			{
				if (_processed)
					return;

				_processed = true;
				int rDemand = ZoneManager.instance.m_actualResidentialDemand;
				int cDemand = ZoneManager.instance.m_actualCommercialDemand;
				int wDemand = ZoneManager.instance.m_actualWorkplaceDemand;

				string message = $"Residential: {rDemand}\nCommercial: {cDemand}\nWorkplace: {wDemand}";

				ExceptionPanel panel = UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel");
				panel.SetMessage("Mnns Dialog", message, false);
			}

			else
			{
				_processed = false;
			}
		}
	}


    public class BuildingInfoPlooling : BuildingWorldInfoPanel
	{
        /*
        Ctrl + M
		select a building
		BuildingManager -> Array16`1[Building] m_building s = Array16`1[Building]
			-> m_buffer
				m_buffer.[building id] 
					info
						

          building info are placed in 
			
         
         */
    }

}
