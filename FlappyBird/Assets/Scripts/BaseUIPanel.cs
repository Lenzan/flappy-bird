using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/**
 * UI面板的基类
 * 负责面板的初始化，打开或者退出时的一些动作
 */
public class BaseUIPanel : MonoBehaviour
{
    //初始化
    public virtual void Awake() {
        // TODO implement here
    }
    //面板进入
    public virtual void OnEnter() {
        // TODO implement here
    }

    //面板退出
    public virtual void OnExit() {
        // TODO implement here
    }
}