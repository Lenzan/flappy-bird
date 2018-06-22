
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIPanel : BaseUIPanel {
    public int step;
    public List<Trigger> channels = new List<Trigger>();
    public GroundPanel grounds;
    public Transform _parent;
    private Button runButton;
    private Image lead;
    private Image getReady;
    public float speed;
    public DOTweenVisualManager manager;
    private GameObject bird;
    private Transform birdParent;
    public override void Awake()
    {
        base.Awake();
        birdParent = transform.Find("birdParent");
        lead = transform.Find("Lead").GetComponent<Image>();
        getReady = transform.Find("GetReady").GetComponent<Image>();
        runButton = transform.Find("Panel").GetComponent<Button>();
        runButton.onClick.AddListener(RunButtonClick);
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
        //随机小鸟
        RandomBird();
        runButton.interactable = true;
        manager.enabled = true;
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
        BirdController.instance.speed = 50;
        bird.transform.SetParent(transform);
        lead.DOColor(new Color(1,1,1,0), 0.5f);
        getReady.DOColor(new Color(1, 1, 1, 0), 0.5f);
        BirdController.instance.GetComponent<CircleCollider2D>().enabled = true;
        BirdController.instance.isDie = false;
        BirdController.instance.localRotateTarget = new Vector3(0,0,40);
        manager.enabled = false;

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
        lead.DOColor(new Color(1, 1, 1, 1), 0);
        getReady.DOColor(new Color(1, 1, 1, 1), 0);
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

    void RandomBird()
    {
        if (bird != null)
        {
            Destroy(bird);
        }
        int index = UnityEngine.Random.Range(1, 4);
        GameObject randombird;
        switch (index)
        {
            case 1:
                randombird =  Instantiate(Resources.Load<GameObject>("bird"));
                UIManager.instance.backGround.sprite = Resources.Load<Sprite>("bg_day");
                break;
            case 2:
                randombird = Instantiate(Resources.Load<GameObject>("redbird"));
                UIManager.instance.backGround.sprite = Resources.Load<Sprite>("bg_night");
                break;
            case 3:
                randombird = Instantiate(Resources.Load<GameObject>("bluebird"));
                UIManager.instance.backGround.sprite = Resources.Load<Sprite>("bg_day");
                break;
            default:
                randombird = Instantiate(Resources.Load<GameObject>("bird"));
                UIManager.instance.backGround.sprite = Resources.Load<Sprite>("bg_night");
                break;
        }
        randombird.transform.SetParent(birdParent);
        randombird.transform.localScale *= 0.8f;
        randombird.transform.localPosition = Vector3.zero;
        bird = randombird;
        manager = bird.GetComponent<DOTweenVisualManager>();
        randombird.transform.SetParent(this.transform);
    }

}