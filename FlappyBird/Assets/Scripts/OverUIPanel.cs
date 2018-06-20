
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

    void OnEnable()
    {
        score.text = Score.instance._Score.ToString();
        bestScore.text = Score.instance.BestScore.ToString();
        SetIcon();
    }
    /// <summary>
    /// ÃÌº”œ‘ æ∂Øª≠
    /// </summary>
    public override void OnEnter() {
       
    }

    public override void OnExit() {
        gameObject.SetActive(false);
        medalImage.sprite = null;
    }

    public void SetIcon()
    {
        int index = 1;
        int scores = Score.instance._Score;
        if (scores < 10 && scores > 5) index = 1;
        else if (scores < 30 && scores >= 10) index = 2;
        else if(scores >= 30)
            index = 3;
        Sprite icon = Resources.Load<Sprite>("medals_" + index);
        medalImage.sprite = icon;
    }

    void RestartButtonClick()
    {
        UIManager.instance.Show(UIPanel.PlayUI);
    }
   

}