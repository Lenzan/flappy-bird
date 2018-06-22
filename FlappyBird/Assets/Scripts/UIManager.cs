
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/**
 * UI管理类
 * 主要负责UI面板的打开和关闭
 */
public class UIManager  : MonoBehaviour {
    
    public Dictionary<UIPanel , BaseUIPanel> uiBase = new Dictionary<UIPanel , BaseUIPanel>();
    public static UIManager instance;
    private BaseUIPanel currentShow;
    public Image screen;
    public Image backGround;
    public void Awake() {
        instance = this;
        BaseUIPanel startPanel = transform.Find("UIPanel/StartUIPanel").GetComponent<BaseUIPanel>();
        uiBase.Add(UIPanel.StartUI, startPanel);
        BaseUIPanel playPanel = transform.Find("UIPanel/PlayUIPanel").GetComponent<BaseUIPanel>();
        uiBase.Add(UIPanel.PlayUI, playPanel);
        BaseUIPanel overPanel = transform.Find("UIPanel/OverUIPanel").GetComponent<BaseUIPanel>();
        uiBase.Add(UIPanel.OverUI, overPanel);
    }

    void Start()
    {
        Show(UIPanel.StartUI);
    }
    ///显示UI
    public void Show(UIPanel uiPanel) {
        if(currentShow != null) currentShow.OnExit();
        currentShow = uiBase[uiPanel];
        currentShow.DoScreen();
    }

    public void Close(UIPanel uiPanel) {
        if (uiBase.ContainsKey(uiPanel))
        {
            uiBase[uiPanel].gameObject.SetActive(false);
        }
    }

  
    

}