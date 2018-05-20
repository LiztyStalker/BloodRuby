using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManagerBuffDataClass : BuffDataClass
{

	List<BuildingObjectClass> m_buildingList = new List<BuildingObjectClass>();

	public override bool buffEnd ()
	{

		BuildingObjectClass[] builds = m_buildingList.ToArray ();

		foreach (BuildingObjectClass building in builds) {
			building.removeObject (building.gameObject);
		}

		m_buildingList.Clear ();

		return base.buffEnd ();

	}

	public void addBuilding(BuildingObjectClass building){
		m_buildingList.Add (building);
		building.setBuildManager (this);
	}


	public bool removeBuilding(BuildingObjectClass building){
		if (m_buildingList.Contains (building))
			return m_buildingList.Remove (building);
		return false;
	}

	/// <summary>
	/// 해당 건설된 건물 개수 가져오기
	/// </summary>
	/// <returns>The building count.</returns>
	/// <param name="building">Building.</param>
	public int getBuildingCount(BuildingObjectClass building){
		int cnt = 0;
		foreach (BuildingObjectClass build in m_buildingList) {
			if (building.GetType () == build.GetType ()) {
				cnt++;
			}
		}
		return cnt;
	}

	/// <summary>
	/// 전체 건설된 건물 개수 가져오기
	/// </summary>
	/// <returns>The building count.</returns>
	public int getBuildingCount(){
		return m_buildingList.Count;
	}

}


