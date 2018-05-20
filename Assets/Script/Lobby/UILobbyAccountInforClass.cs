using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyAccountInforClass : MonoBehaviour
{
    [SerializeField] Text m_nameText;
    [SerializeField] Image m_iconImage;
    [SerializeField] Text m_battlePointText;
    [SerializeField] Text m_bloodRubyText;
    [SerializeField] Text m_levelText;
    [SerializeField] Slider m_expSlider;
    [SerializeField] Text m_expText;

    [SerializeField] Text m_kdaText;
    [SerializeField] Text m_gameTime;
    [SerializeField] Text m_damageText;
    [SerializeField] Text m_healthText;
    [SerializeField] Text m_captureText;



    void OnEnable()
    {
        m_nameText.text = AccountClass.GetInstance.accountReport.name;
		m_iconImage.sprite = AccountClass.GetInstance.accountReport.icon;
        m_battlePointText.text = string.Format("BP {0}", AccountClass.GetInstance.accountReport.battlePoint);
        m_bloodRubyText.text = string.Format("BR {0}", AccountClass.GetInstance.accountReport.bloodRuby);
		m_levelText.text = string.Format("Lv{0}", AccountClass.GetInstance.accountReport.level);

		m_expText.text = AccountClass.GetInstance.accountReport.experianceValue;
        m_expSlider.value = AccountClass.GetInstance.accountReport.experianceRate;

        m_kdaText.text = AccountClass.GetInstance.gameReport.totalKDA;
        m_gameTime.text = AccountClass.GetInstance.gameReport.totalTime.ToString();
        m_damageText.text = AccountClass.GetInstance.gameReport.totalDamage;
        m_healthText.text = AccountClass.GetInstance.gameReport.totalHealth;
        m_captureText.text = AccountClass.GetInstance.gameReport.totalFlag;

    }

}

