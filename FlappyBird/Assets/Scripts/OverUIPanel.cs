
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
/**
 * 
 */
public class OverUIPanel : BaseUIPanel{
    public Text score;
    public Text bestScore;
    private Button restartButton;
    private Image medalImage;
    public override void Awake() {
        restartButton = transform.Find("ReStart/ReStartButton").GetComponent<Button>();
        medalImage = transform.Find("MedalPanel/Medal").GetComponent<Image>();
        restartButton.onClick.AddListener(RestartButtonClick);
      
    }

    public override void OnEnter() {
        score.text = Score.instance._Score.ToString();
        bestScore.text = Score.instance.GetScore(Application.streamingAssetsPath + "/score.txt").ToString();
        SetIcon();
    }

    public override void OnExit() {
        gameObject.SetActive(false);

    }

    public void SetIcon()
    {
        int index = 1;
        int scores = Score.instance._Score;
        if (scores < 10) index = 1;
        else if (scores < 30) index = 2;
        else index = 3;
        Sprite icon = Resources.Load<Sprite>("medals_" + index);
        medalImage.sprite = icon;
    }

    void RestartButtonClick()
    {
        UIManager.instance.Show(UIPanel.PlayUI);
    }
   

}