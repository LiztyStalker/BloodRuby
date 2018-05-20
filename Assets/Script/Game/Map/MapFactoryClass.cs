using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MapFactoryClass : SingletonClass<MapFactoryClass>
{
    //[SerializeField] MapDataClass[] m_mapList;

	readonly string path = "Prefebs/Factory/Map/MapData";

	List<MapDataClass> m_mapList = new List<MapDataClass>();



	public MapDataClass[] mapList { 
		get { 
			return m_mapList.Where(mapData => mapData.isSelect).ToArray(); 
		} 
	}


	public MapFactoryClass(){
		Start();
	}


	void Start(){

		MapDataClass[] maps = Resources.LoadAll <MapDataClass> (string.Format("{0}", path));

		if (maps.Length > 0) {
			m_mapList.AddRange (maps.ToList<MapDataClass> ());
		}
		Debug.Log ("MapCount : " + m_mapList.Count);
	}



	/// <summary>
	/// 드롭다운 인덱스로 맵 가져오기
	/// </summary>
	/// <returns>The map.</returns>
	/// <param name="slot">Slot.</param>
    public MapDataClass getMap(int slot)
    {
        //Debug.Log ("슬롯 : " + slot);
		if (m_mapList.Count > slot)
			return m_mapList[m_mapList.Count - 1];
        
        if(slot < 0)
            return m_mapList[0];

        return m_mapList[slot];
    }

	/// <summary>
	/// 키로 맵 가져오기
	/// </summary>
	/// <returns>The map.</returns>
	/// <param name="key">Key.</param>
    public MapDataClass getMap(string key)
    {
        return m_mapList.Where(map => map.mapKey == key).SingleOrDefault<MapDataClass>();
    }
}

