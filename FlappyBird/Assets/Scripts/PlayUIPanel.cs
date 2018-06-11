
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIPanel : BaseUIPanel {
    public int step;
    public List<Trigger> channels = new List<Trigger>();
    public List<Ground> grounds = new List<Ground>();
    public Transform _parent;
    private Button runButton;
    private GameObject lead;
    private GameObject getReady;
    public override void Awake()
    {
        lead = transform.Find("Lead").gameObject;
        getReady = transform.Find("GetReady").gameObject;
        runButton = transform.Find("Panel").GetComponent<Button>();
        runButton.onClick.AddListener(RunButtonClick);
    }

    public override void OnEnter()
    {
        if(channels.Count == 0 ) return;
        BirdController.instance.SetAnimatorSpeed(State.Idle);
        ResetPlayUIPanel();
    }

    public override void OnExit() {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 小鸟碰壁
    /// </summary>
    public void Die()
    {
        SetScene(0);
        UIManager.instance.Show(UIPanel.OverUI);
        Score.instance.SaveBestScore();
    }

    void Run()
    {
        //动态实例化三个管道 循环
        for (int i = 0; i < 3; i++)
        {
            GameObject channel = Instantiate(Resources.Load<GameObject>("Trigger"));
            channel.name = "channel" + i;
            channel.transform.SetParent(_parent);
            channel.transform.localScale = Vector3.one;
            Trigger trigger = channel.GetComponent<Trigger>();
            trigger.speed = 90;
            channel.transform.localPosition = new Vector2(370 + i * step, trigger.GetY());
            if (channels.Contains(trigger)) return;
            channels.Add(trigger);
        }
    }

    public void RunButtonClick()
    {
        if(channels.Count == 0)
            Run();
        else
            SetScene(90);
        runButton.gameObject.SetActive(false);
        BirdController.instance.SetAnimatorSpeed(State.Fly);
        BirdController.instance.vecY = BirdController.instance.fixedSpeed;
        lead.gameObject.SetActive(false);
        getReady.gameObject.SetActive(false);
    }

    /// <summary>
    /// 重置场景
    /// </summary>
    public void ResetPlayUIPanel()
    {
        for (int i = 0; i < 3; i++)
        {
            Trigger trigger = channels[i].GetComponent<Trigger>();
            channels[i].transform.localPosition = new Vector2(370 + i * step, trigger.GetY());
        }
        Score.instance._Score = 0;
        SetScene(0);
        lead.gameObject.SetActive(true);
        getReady.gameObject.SetActive(true);
        BirdController.instance.InitPos();
        Score.instance.Init();
    }

    void SetScene(float speed)
    {
        foreach (Trigger trigger in channels)
        {
            trigger.speed = speed;
        }
        foreach (Ground ground in grounds)
        {
            ground.speed = speed;
        }
        runButton.gameObject.SetActive(true);
    }

}