
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIPanel : BaseUIPanel {
    public int step;
    public List<Trigger> channels = new List<Trigger>();
    public GroundPanel grounds;
    public Transform _parent;
    private Button runButton;
    private GameObject lead;
    private GameObject getReady;
    public float speed;
    public override void Awake()
    {
        lead = transform.Find("Lead").gameObject;
        getReady = transform.Find("GetReady").gameObject;
        runButton = transform.Find("Panel").GetComponent<Button>();
        runButton.onClick.AddListener(RunButtonClick);
    }

    public override void OnEnter()
    {
        runButton.interactable = true;
        if (channels.Count == 0 ) return;
        ResetPlayUIPanel();
    }

    public override void OnExit() {
        //gameObject.SetActive(false);
        runButton.interactable = false;
    }

    /// <summary>
    /// 小鸟碰壁
    /// </summary>
    public void Die()
    {
        Score.instance.SaveBestScore();
        SetScene(0);
        UIManager.instance.Show(UIPanel.OverUI);
        BirdController.instance.GetComponent<CircleCollider2D>().enabled = false;
        BirdController.instance.SetAnimatorSpeed(State.Die);
        AudioManager.instance.AudioShotPlay("sfx_hit");
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
            trigger.speed = speed ;
            channel.transform.localPosition = new Vector2(1500 + i * step, trigger.GetY());
            if (channels.Contains(trigger)) return;
            channels.Add(trigger);
        }
    }
     
    public void RunButtonClick()
    {
        if(channels.Count == 0)
            Run();
        else
            SetScene(speed);
        runButton.gameObject.SetActive(false);
        BirdController.instance.SetAnimatorSpeed(State.Fly);
        BirdController.instance.vecY = BirdController.instance.fixedSpeed;
        BirdController.instance.speed = 20;
        lead.gameObject.SetActive(false);
        getReady.gameObject.SetActive(false);
        BirdController.instance.GetComponent<CircleCollider2D>().enabled = true;
        BirdController.instance.isDie = false;
        BirdController.instance.localRotateTarget = new Vector3(0,0,40);
    }

    /// <summary>
    /// 重置场景
    /// </summary>
    public void ResetPlayUIPanel()
    {
        for (int i = 0; i < 3; i++)
        {
            Trigger trigger = channels[i].GetComponent<Trigger>();
            channels[i].transform.localPosition = new Vector2(1500 + i * step, trigger.GetY());
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
        grounds.speed = speed;
        runButton.gameObject.SetActive(true);
    }

}