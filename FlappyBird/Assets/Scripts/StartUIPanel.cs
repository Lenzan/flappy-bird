
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StartUIPanel : BaseUIPanel{
    private Button startButton;
    public override void Awake() {
        startButton = transform.Find("Start").GetComponent<Button>();
        startButton.onClick.AddListener(delegate() {
            UIManager.instance.Show(UIPanel.PlayUI);
        });
    }

    public override void OnEnter() {
        // TODO implement here

    }

    
    public override void OnExit() {
        gameObject.SetActive(false);
    }

    

}