
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/**
 * 碰撞检测通过管道加分
 */
public class Trigger : MonoBehaviour{
    //移动速度
    public float speed;

    void Update() {
        if (transform.localPosition.x < -600)
            transform.localPosition = new Vector2(1200, GetY());
        transform.Translate( - transform.right * speed * Time.deltaTime);
        //transform.localPosition += -Vector3.right * speed * Time.deltaTime;
    }
    //碰撞检测
    private void OnTriggerExit2D(Collider2D collider) {
        if (BirdController.instance.currentState == State.Die) return;
        AudioManager.instance.AudioScorePlay("sfx_point");
        Score.instance._Score++;
    }

    /// <summary>
    /// 随机Y轴的位置
    /// </summary>
    /// <returns></returns>
    public float GetY()
    {
        return UnityEngine.Random.Range(-200 , 200);
    }
    

}