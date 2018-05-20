using System;
using System.Collections;
public class MapReportClass
{



    public string m_mapKey;
    public TYPE_MODE m_mode;
    public int m_time;
    public int m_population;
    public int m_ticket;
    public bool m_capture;
    public bool m_item;
    public int m_level = 1;
    public int m_respawn;

  

    

    public void setMapReport(string mapKey, int[] values)
    {

        m_mapKey = mapKey;

        for (int i = 0; i < values.Length; i++)
        {
            switch((TYPE_MAP_PANEL)i){
                case TYPE_MAP_PANEL.MODE:
                    m_mode = ((TYPE_MODE[])Enum.GetValues(typeof(TYPE_MODE)))[values[i]];
                    break;
                case TYPE_MAP_PANEL.POPULATION:
                    m_population = PrepClass.m_populations[values[i]];
                    break;
                case TYPE_MAP_PANEL.RESPAWN:
                    m_respawn = PrepClass.m_respawns[values[i]];
                    break;
                case TYPE_MAP_PANEL.TICKET:
                    m_ticket = PrepClass.m_tickets[values[i]];
                    break;
                case TYPE_MAP_PANEL.TIME:
                    m_time = PrepClass.m_times[values[i]];
                    break;
                case TYPE_MAP_PANEL.CAPTURE:
                    m_capture = PrepClass.m_captures[values[i]];
                    break;
                case TYPE_MAP_PANEL.ITEM:
                    m_item = PrepClass.m_items[values[i]];
                    break;
                case TYPE_MAP_PANEL.LEVEL:
                    m_level = PrepClass.m_levels[values[i]];
                    break;
                default:
                    break;
            }
        }
    }
}

