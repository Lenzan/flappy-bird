
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
/**
 * 
 */
public class OverUIPanel : BaseUIPanel{
    public Transform score;
    public Transform bestScore;
    private Button restartButton;
    private Image medalImage;
    private List<GameObject> currentScores= new List<GameObject>();
    private List<GameObject> bestScores = new List<GameObject>();
    private Sprite defaultSprite;
    public override void Awake()
    {
        restartButton = transform.Find("ReStart/ReStartButton").GetComponent<Button>();
        medalImage = transform.Find("MedalPanel/Medal").GetComponent<Image>();
        restartButton.onClick.AddListener(RestartButtonClick);
        defaultSprite = medalImage.sprite;
    }

    void OnEnable()
    {
        SetScores(Score.instance._Score, currentScores , score);
        SetScores(Score.instance.BestScore, currentScores, bestScore);
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
        RestList(currentScores);
        RestList(bestScores);
        medalImage.sprite = defaultSprite;
    }

    public void SetIcon()
    {
        int index = 1;
        int scores = Score.instance._Score;
        if (scores < 10 ) return;
        else if (scores > 10 && scores < 20) index = 1;
        else if (scores < 30 && scores >= 20) index = 2;
        else if(scores >= 30)index = 3;


        Sprite icon = Resources.Load<Sprite>("medals_" + index);
        medalImage.sprite = icon;
    }

    void RestartButtonClick()
    {
        UIManager.instance.Show(UIPanel.PlayUI);
    }

    void RestList(List<GameObject> list)
    {
        foreach (var item in list)
        {
           Destroy(item);
        }
        list.Clear();
    }

    void SetScores(int score , List<GameObject> list , Transform parent)
    {
        int length = score.ToString().Length;
        char[] num = score.ToString().ToCharArray();

        for (int i = 0; i < num.Length; i++)
        {
            int index = int.Parse(num[i].ToString());
            GameObject go = Instantiate(Resources.Load<GameObject>("overScore"));
            go.transform.SetParent(parent);
            //go.transform.localScale *= 0.7f;
            go.GetComponent<Image>().sprite = Score.instance.numbers[index];
            list.Add(go);
        }
    }
}