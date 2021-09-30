using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class Mechanics : MonoBehaviour
{
    public GameObject Timer;//720s
    // Start is called before the first frame update
    public Slider Sleepy_Slider;
    public float Sleepy_Start;
    public Text Sleepy_text;

    public float MaxSleepy;

    public AudioSource Yawn1,Yawn2,Yawn3;
    //sudden die
    //public Slider SuddenDie_Slider;
    //public float SuddenDie_Start;
    //public Text SuddenDie_text;



    //work  //工作说明
    public GameObject Work;
    public Slider Work_Slider;
    public float Work_Start;
    public Text Work_text;

    public float MaxWork;

    //bool Working=false;
    //public Text WorkBtn_text;

    //Start Button
    //public GameObject StartBtn;

    //Sleep/Slack Function
    //SleepType说明：0=none sleep，1=short sleep, 2=long sleep;
    public Flowchart FungusFlowchart;
    public float SleepTime;
    public float TotalSleepTime;
    //SlackOff说明：
    public float SlackOffTime;
    //增益说明
  

    void Start()
    {
        //initialization
        //sleepy
        //Sleepy_text.text = Sleepy_Start.ToString();
        Sleepy_Slider.value = Sleepy_Start;
        Sleepy_Slider.maxValue = MaxSleepy;

        //Sudden die
        //SuddenDie_text.text = SuddenDie_Start.ToString();
        //SuddenDie_Slider.value = SuddenDie_Start;
        //SuddenDie_Slider.maxValue = Timer.GetComponent<Timer>().MaxTime - 2*30;


        //Work
        //Work_text.text = Work_Start.ToString();
        Work_Slider.value = Work_Start;
        Work_Slider.maxValue = MaxWork;
    }

    // Update is called once per frame
    void Update()
    {
        //检测timer的bool值
        if (FungusFlowchart.GetBooleanVariable("TimerActive"))
        {
            //sleepy 每30秒（游戏里1h）满
            if(Sleepy_Start<= MaxSleepy)
            {
                Sleepy_Start += Time.deltaTime;
                //Sleepy_text.text = (Sleepy_Start / 30).ToString("00.");
                FungusFlowchart.SetBooleanVariable("SleepyNotice", false);


                if (Sleepy_Start > 0 && Sleepy_Start < 15)
                {
                    Debug.Log("Played");
                    Yawn1.Play();
                }
            }

            //work 240 秒满 （游戏里8h）
            if(FungusFlowchart.GetBooleanVariable("Working"))
            {
                Work_Start += Time.deltaTime;
                //Work_text.text = Work_Start.ToString();
            }

            //sudden die 醒着的时间/现在已经过的时间（
            //SuddenDie_Start = Timer.GetComponent<Timer>().timeStart - TotalSleepTime;
            
            //SuddenDie_text.text = (SuddenDie_Start/360).ToString("00.");
        }

        
        if (Sleepy_Start >= MaxSleepy)
        {
            //太困了干不了工作 需要减少困倦值
            Sleepy_Start = MaxSleepy;
            FungusFlowchart.SetBooleanVariable("Working", false);
            FungusFlowchart.SetBooleanVariable("SleepyNotice", true);
            FungusFlowchart.SetBooleanVariable("ClickAccess", true);
            Work.SetActive(false);
            Yawn2.Play(); Yawn3.Play();
        }
        //改workbtn 按键，停止工作
        //WorkBtn_text.text = FungusFlowchart.GetBooleanVariable("Working") ? "Pause" : "Work";

        if (Work_Start>= MaxWork)
        {
            //任务完成
        }

        if(Timer.GetComponent<Timer>().timeStart>= Timer.GetComponent<Timer>().MaxTime&& Work_Start< MaxWork)
        {
            //任务失败
        }

        //显示slider
        Sleepy_Slider.value = Sleepy_Start;
        Work_Slider.value = Work_Start;
        //SuddenDie_Slider.value = SuddenDie_Start;

        //检测睡觉操作....................................................................................
        //short Sleep
        if(FungusFlowchart.GetIntegerVariable("SleepType")==0)
        {
            //没睡
        }
        else if (FungusFlowchart.GetIntegerVariable("SleepType") == 1)
        {
            FungusFlowchart.SetBooleanVariable("Working", false);
            //short sleep 小睡：耗时短有闹铃(30min-60min)（困倦值-30%）工作效率+20，有很大机率会再次睡着（50%），可以强行起床（70%）但会增加困倦值（5）

            SleepTime = Random.Range(15f,30f); //生成睡眠时间       
            Sleepy_Start -= 9;//困倦条
            if(Sleepy_Start<0)
            {
                Sleepy_Start = 0;
            }


            Timer.GetComponent<Timer>().timeStart += SleepTime;//睡眠时间加到总时间

            //初始化SleepTime
            TotalSleepTime += SleepTime;
            SleepTime = 0;
            FungusFlowchart.SetIntegerVariable("SleepType", 0);
        }
        else if(FungusFlowchart.GetIntegerVariable("SleepType") == 2)
        {
            FungusFlowchart.SetBooleanVariable("Working", false);
            //long sleep 睡觉：耗时长（6h~9h），清空困倦条，工作效率加满，30%会小睡（30min~50min）
            SleepTime = Random.Range(180f, 270f);//生成睡眠时间       
            Sleepy_Start = 0;//困倦条

            Timer.GetComponent<Timer>().timeStart += SleepTime;//睡眠时间加到总时间
            //初始化SleepTime
            TotalSleepTime += SleepTime;
            SleepTime = 0;
            FungusFlowchart.SetIntegerVariable("SleepType",0);
        }
        //检测工作按键

        //检测slack off 操作..................................................................................
        if (FungusFlowchart.GetIntegerVariable("SlackType")==0)
        {
            //没摸鱼
        }
        
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 1)
        {
            //玩steam
            PlaySteamGame();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 2)
        {
            //玩手机
            PlayMobileGame();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 3)
        {
            //拉屎
            Shit();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 4)
        {
            //点外卖
            OrderMealOnline();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 5)
        {
            //吃饭
            TakeFood();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 6)
        {
            //洗澡
            TakeShower();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 7)
        {
            //喝咖啡
            TakeCoffee();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 8)
        {
            //喝monster
            TakeMonster();
        }
    }

    //slackoff 1 
    public void PlaySteamGame()
    {
        Debug.Log("Play steam game");
        FungusFlowchart.SetBooleanVariable("Working", false);
        //上steam玩游戏：时间（1h~5h）困倦值 -50% 工作效率-50
        SlackOffTime = Random.Range(30f, 150f);
        Sleepy_Start -= 20;//困倦条
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//摸鱼时间加到总时间
        //初始化SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);

    }
    public void PlayMobileGame()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //玩手机：玩游戏 困倦值 -90（30min~2h），工作效率 - 80 %
        SlackOffTime = Random.Range(15f, 60f);
        Sleepy_Start -= 10;//困倦条
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//摸鱼时间加到总时间
        //初始化SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void OrderMealOnline()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //点外卖：选择恐惧症 （10~40min）
        SlackOffTime = Random.Range(1f, 20f);

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//摸鱼时间加到总时间
        //初始化SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void Shit()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //上厕所（大）：玩手机（看视频，刷微博 twitter）困倦 - 50
        SlackOffTime = Random.Range(5f, 30f);
        Sleepy_Start -= 15;//困倦条
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//摸鱼时间加到总时间
        //初始化SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void TakeShower()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //洗澡：困倦值 - 70（5min~10min）只能洗两次
        SlackOffTime = Random.Range(1f, 5f);
        Sleepy_Start -= 21;//困倦条
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//摸鱼时间加到总时间
        //初始化SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void TakeCoffee()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //喝咖啡：困倦值 -10 （5 min）
        SlackOffTime = 3f;
        Sleepy_Start -= 3;//困倦条
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//摸鱼时间加到总时间
        //初始化SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void TakeMonster()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //喝怪兽饮料：困倦值 -20 （5 min）
        SlackOffTime = 3f;
        Sleepy_Start -= 6;//困倦条
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//摸鱼时间加到总时间
        //初始化SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void TakeFood()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //喝怪兽饮料：困倦值 -20 （5 min）
        SlackOffTime = 30f;
        Sleepy_Start -= 15;//困倦条
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//摸鱼时间加到总时间
        //初始化SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
}
