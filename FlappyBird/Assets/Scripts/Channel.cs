
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/**
 * 碰撞检测失败
 */
public class Channel : MonoBehaviour{

    private PlayUIPanel playUI;
    private void Awake()
    {
        playUI = GameObject.Find("PlayUIPanel").GetComponent<PlayUIPanel>();
    }
    /// <summary>
    ///  碰撞检测
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collider) {
  
        if (BirdController.instance.currentState == State.Die) return;
        BirdController.instance.smooth = 0.5f;
        playUI.Die();
    }

}