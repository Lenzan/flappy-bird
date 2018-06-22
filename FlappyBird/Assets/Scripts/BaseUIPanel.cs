using System;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/**
 * UI面板的基类
 * 负责面板的初始化，打开或者退出时的一些动作
 */
public class BaseUIPanel : MonoBehaviour
{
    public Image screen;
    //初始化
    public virtual void Awake() {
        
        screen = UIManager.instance.screen;
    }
    //面板进入
    public virtual void OnEnter() {
        
    }

    //面板退出
    public virtual void OnExit() {
        
    }

    public virtual void DoScreen(){
        screen.DOColor(Color.black, 0.5f).OnComplete(() =>
        {
            OnEnter();
            screen.DOColor(new Color(0, 0, 0, 0), 2);
        });
    }
}