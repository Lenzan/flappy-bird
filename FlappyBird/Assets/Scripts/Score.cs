using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public static Score instance;
    private int score = 0;
    private int bestScore;
    public List<Sprite> numbers;
    private List<GameObject> scores = new List<GameObject>();
    public int _Score
    {
        get {
            return score;
        }
        set
        {
            score = value;
            UpdateScore();
        }
    }

    void Start () {
        instance = this;
        Init();
        string path = Application.streamingAssetsPath + "/score.txt";
        SetScore(path);
    }

    public void Init()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] != null)
                Destroy(scores[i]);
        }
        scores.Clear();
        instantiateScore();
        scores[0].GetComponent<Image>().sprite = numbers[0];
        UpdateScore();
    }
	
    void instantiateScore()
    {
        GameObject score = Instantiate(Resources.Load<GameObject>("Score"));
        score.transform.parent = transform;
        score.transform.localScale = Vector3.one;
        scores.Add(score);
    }

    /// <summary>
    /// 更新分数
    /// </summary>
    public void UpdateScore()
    {
        int length = _Score.ToString().Length;
        if (scores.Count < length)
        {
            instantiateScore();
        }
        char[] num = _Score.ToString().ToCharArray();
        for (int i = 0; i < num.Length; i++)
        {
            int index = int.Parse(num[i].ToString());
            scores[i].GetComponent<Image>().sprite = numbers[index];
        }
        SaveBestScore();
    }

    /// <summary>
    /// 保存历史最高分
    /// </summary>
    public void SaveBestScore()
    {
        //float best = PlayerPrefs.GetFloat("best");
        //if (_Score > best)
        //{
        //    PlayerPrefs.SetFloat("best" , _Score);
        //}
        string path = Application.streamingAssetsPath + "/score.txt";
        int best = GetScore(path);
        if (_Score > best)
        {
            SetScore(path);
        }

    }

    public int GetScore(string path)
    {
        var file = new FileInfo(path);
        //if (!file.Exists) return 0;
        StreamReader sr;
        var score = "";
        sr = file.OpenText();
        score = sr.ReadToEnd();
        sr.Close();
        return int.Parse(score);
    }

    void SetScore(string path)
    {
        var file = new FileInfo(path);
        StreamWriter sw;
        sw = File.CreateText(path);//打开现有 UTF-8 编码文本文件以进行读取  
        sw.WriteLine(_Score.ToString());//以行为单位写入字符串  
        sw.Close();
        //sw.Dispose();//文件流释放  
    }
}
