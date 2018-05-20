using System;
using System.Collections;
using UnityEngine;

public class AccountReportClass
{


    const int c_expOffset = 100;


	Sprite m_icon;
    string m_name;

    int m_level = 1;
    int m_experiance = 0;
    int m_battlePoint = 0;
    int m_bloodRuby = 0;

    public string name { get { return m_name; } set { m_name = value; } }
	public Sprite icon { get { return m_icon; } set { m_icon = value; } }

    public int level { get { return m_level; } }
    public int experiance { get { return m_experiance; }
       
    }
    public int experianceMax { get { return m_level * c_expOffset; } }
    public int battlePoint { get { return m_battlePoint; } }
    public int bloodRuby { get { return m_bloodRuby; }  }

	/// <summary>
	/// 경험치 비율 0~1
	/// </summary>
	/// <value>The experiance rate.</value>
    public float experianceRate { get { return PrepClass.ratioCalculator(experiance, experianceMax); } }

	/// <summary>
	/// 경험치 퍼센트 0/0(0.0~100.0%)
	/// </summary>
	/// <value>The experiance percent.</value>
	public string experianceValue { get { return string.Format("{0}/{1}({2:f1})%", experiance, experianceMax, experianceRate * 100f); } }

    public AccountReportClass()
    {
		m_name = "플레이어";
        m_level = 1;
        m_experiance = 0;
        m_battlePoint = 1000;
        m_bloodRuby = 10;
    }


	public int addExperiance(int experiance){

		m_experiance += experiance;
		while (m_experiance >= c_expOffset * m_level)
		{
			m_level++;
			m_experiance -= c_expOffset * m_level;
		}
		return m_experiance;
	}

	public int addBattlePoint(int battlePoint){
		m_battlePoint += battlePoint;
		return m_battlePoint;
	}

	public int addBloodRuby(int ruby){
		m_bloodRuby += ruby;
		return m_bloodRuby;
	}

}

