
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Ground : MonoBehaviour{
    //地板移动速度
    public float speed;
  
    private PlayUIPanel playUI;

    private void Update() {
        if (transform.localPosition.x < -1078)
            transform.localPosition = new Vector2(1078, -800 );
        //transform.Translate(-transform.right * speed * Time.deltaTime);
        transform.localPosition += -Vector3.right * speed *2 * Time.deltaTime;
    }
    
    private void OnTriggerExit2D(Collider2D collider) {
        BirdController.instance.vecY = 0;
        BirdController.instance.speed = 0;
        if (BirdController.instance.currentState != State.Die)
        {
            playUI = UIManager.instance.uiBase[UIPanel.PlayUI] as PlayUIPanel;
            playUI.Die();
            BirdController.instance.SetAnimatorSpeed(State.Die);
            AudioManager.instance.AudioShotPlay("sfx_hit");
        }
    }

    

}