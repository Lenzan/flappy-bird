
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartUIPanel : BaseUIPanel{
    private Button startButton;
   
    public override void Awake() {
        base.Awake();
        startButton = transform.Find("Start").GetComponent<Button>();
        startButton.onClick.AddListener(delegate() {
            UIManager.instance.Show(UIPanel.PlayUI);
        });
    }

    public override void OnEnter() {
       gameObject.SetActive(true);
     
    }

    
    public override void OnExit() {
        gameObject.SetActive(false);
    }

}