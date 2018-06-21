
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Ground : MonoBehaviour{

  
    private PlayUIPanel playUI;

    private void OnTriggerExit2D(Collider2D collider) {
        if (BirdController.instance.currentState != State.Die)
        {
            playUI = UIManager.instance.uiBase[UIPanel.PlayUI] as PlayUIPanel;
            BirdController.instance.smooth = 0;
            playUI.Die();
        }
    }
}