using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum TYPE_MAP_PANEL { MAP, MODE, TIME, POPULATION, TICKET, CAPTURE, ITEM, LEVEL, RESPAWN }


public class UILobbyBattleClass : UILobbyParentClass
{


	[SerializeField] Image m_minimapImage;
	[SerializeField] Text m_contentsText;
    [SerializeField] Dropdown[] m_dropdowns;

    MapFactoryClass factory;

    void Start()
    {
		factory = MapFactoryClass.GetInstance;// GameObject.Find("Game@MapFactory").GetComponent<MapFactoryClass>();

        m_dropdowns[(int)TYPE_MAP_PANEL.MAP].ClearOptions();


        foreach (Dropdown drop in m_dropdowns)
        {
            drop.onValueChanged.AddListener((int index) => OnValueChange(index));
        }


        foreach (MapDataClass map in factory.mapList)
        {
//            Dropdown.OptionData optData = new Dropdown.OptionData(map.mapKey);

            m_dropdowns[(int)TYPE_MAP_PANEL.MAP].options.Add(new Dropdown.OptionData(TranslatorClass.GetInstance.mapTranslator(map.mapKey)));

            //            m_dropdowns[(int)TYPE_MAP.MAP]
        }

        m_dropdowns[(int)TYPE_MAP_PANEL.MAP].captionText.text = m_dropdowns[(int)TYPE_MAP_PANEL.MAP].options[0].text;


        for (int i = 1; i < m_dropdowns.Length; i++)
        {

            int length = PrepClass.length(i);

            for (int j = 0; j < length; j++)
            {

                switch ((TYPE_MAP_PANEL)i)
                {
                    case TYPE_MAP_PANEL.MODE:
                        m_dropdowns[i].options.Add(new Dropdown.OptionData(TranslatorClass.GetInstance.modeTranslator((TYPE_MODE)j)));
                        break;
                    case TYPE_MAP_PANEL.POPULATION:
                        m_dropdowns[i].options.Add(new Dropdown.OptionData(PrepClass.m_populations[j].ToString()));
                        break;
                    //case TYPE_MAP_PANEL.RESPAWN:
                    //    m_respawn = m_respawns[values[i]];
                    //    break;
                    case TYPE_MAP_PANEL.TICKET:
                        m_dropdowns[i].options.Add(new Dropdown.OptionData(PrepClass.m_tickets[j].ToString()));
                        break;
                    case TYPE_MAP_PANEL.TIME:
                        m_dropdowns[i].options.Add(new Dropdown.OptionData(PrepClass.m_times[j].ToString()));
                        break;
                    //case TYPE_MAP_PANEL.CAPTURE:
                    //    m_capture = m_captures[values[i]];
                    //    break;
                    //case TYPE_MAP_PANEL.ITEM:
                    //    m_item = m_items[values[i]];
                    //    break;
                    //case TYPE_MAP_PANEL.LEVEL:
                    //    m_level = m_levels[values[i]];
                    //    break;
                    //default:
                    //    break;
                }

                if (j == 0)
                    m_dropdowns[i].captionText.text = m_dropdowns[i].options[0].text;

            }
            
        }


		mapInformationView ();

    }

    void initPanel()
    {

    }


    public void leftMapBtnClicked()
    {
        //인덱스 위 가리키기 - 맨 처음이면 마지막으로
//        m_dropdowns[(int)TYPE_MAP.MAP]
        //mapChanged();

    }

    public void rightMapBtnClicked()
    {
        //인덱스 아래 가리키기 - 맨 마지막이면 처음으로
        //        m_dropdowns[(int)TYPE_MAP.MAP]
       // mapChanged();
    }

    public void gameStart()
    {
        //현재 기록된 데이터를 가지고 게임 데이터 만들기
        AccountClass.GetInstance.playPanel.nextPanel = PrepClass.c_GamePanelScene;
//		AccountClass.GetInstance.playPanel.level = 1;
        AccountClass.GetInstance.playPanel.mapReport.setMapReport(factory.mapList[m_dropdowns[(int)TYPE_MAP_PANEL.MAP].value].mapKey, m_dropdowns.Select(drop => drop.value).ToArray<int>());



        //MapReportClass mapReport = new MapReportClass();
//        mapReport();
        //mapReport.m_mode;
        //mapReport.m_mapKey;
        //mapReport.m_level;
        //mapReport.m_item;
        //mapReport.m_capture;
        //mapReport.m_population;
        //mapReport.m_respawn;
        //mapReport.m_ticket;
        //mapReport.m_time;

		SceneManager.LoadScene (PrepClass.c_LoadPanelScene);
//        Application.LoadLevel(PrepClass.c_LoadPanelScene);
    }


    void OnValueChange(int index)
    {
		mapInformationView ();
//        m_dropdowns.GetValue(index);
//        Debug.Log(m_dropdowns[index].itemText.text);
//        Debug.Log("event : " + EventSystem.current.currentSelectedGameObject.name);
//        Debug.Log("index : " + index);
    }

	void mapInformationView(){
		m_minimapImage.sprite = factory.mapList [m_dropdowns [(int)TYPE_MAP_PANEL.MAP].value].minimapSprite;
		m_contentsText.text = factory.mapList [m_dropdowns [(int)TYPE_MAP_PANEL.MAP].value].contents;
	}

}

