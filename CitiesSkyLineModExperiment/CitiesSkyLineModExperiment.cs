using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ICities;
using ColossalFramework.UI;

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
		
		private bool _processed = false;
		public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
		{
			Debug.Log(_processed);
            if (Input.GetKeyDown(KeyCode.F12))
            {
				
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
