using System;
using System.Collections;
public class PlayPanelClass
{
    string m_nextPanel = null;
    MapReportClass m_mapReport = new MapReportClass();
//	int m_level = 1;


	public string nextPanel { get { return m_nextPanel; } set { m_nextPanel = value; } }
    public MapReportClass mapReport { get { return m_mapReport; } }
//	public int level{ get { return m_level; } set { m_level = value; } }


}

