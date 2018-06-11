
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/**
 * UI管理类
 * 主要负责UI面板的打开和关闭
 */
public class UIManager  : MonoBehaviour {
    
    public Dictionary<UIPanel , BaseUIPanel> uiBase = new Dictionary<UIPanel , BaseUIPanel>();
    public static UIManager instance;
    private BaseUIPanel currentShow;
    public void Awake() {
        instance = this;
        BaseUIPanel startPanel = transform.Find("UIPanel/StartUIPanel").GetComponent<BaseUIPanel>();
        uiBase.Add(UIPanel.StartUI, startPanel);
        BaseUIPanel playPanel = transform.Find("UIPanel/PlayUIPanel").GetComponent<BaseUIPanel>();
        uiBase.Add(UIPanel.PlayUI, playPanel);
        BaseUIPanel overPanel = transform.Find("UIPanel/OverUIPanel").GetComponent<BaseUIPanel>();
        uiBase.Add(UIPanel.OverUI, overPanel);
    }
    
    ///显示UI
    public void Show(UIPanel uiPanel) {
        if(currentShow != null) currentShow.OnExit();
        if(uiBase[uiPanel].gameObject.activeSelf == false)
            uiBase[uiPanel].gameObject.SetActive(true);
        currentShow = uiBase[uiPanel];
        currentShow.OnEnter();
    }

    public void Close(UIPanel uiPanel) {
        if (uiBase.ContainsKey(uiPanel))
        {
            uiBase[uiPanel].gameObject.SetActive(false);
        }
    }

    

}