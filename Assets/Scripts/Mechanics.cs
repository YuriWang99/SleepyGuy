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



    //work  //����˵��
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
    //SleepType˵����0=none sleep��1=short sleep, 2=long sleep;
    public Flowchart FungusFlowchart;
    public float SleepTime;
    public float TotalSleepTime;
    //SlackOff˵����
    public float SlackOffTime;
    //����˵��
  

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
        //���timer��boolֵ
        if (FungusFlowchart.GetBooleanVariable("TimerActive"))
        {
            //sleepy ÿ30�루��Ϸ��1h����
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

            //work 240 ���� ����Ϸ��8h��
            if(FungusFlowchart.GetBooleanVariable("Working"))
            {
                Work_Start += Time.deltaTime;
                //Work_text.text = Work_Start.ToString();
            }

            //sudden die ���ŵ�ʱ��/�����Ѿ�����ʱ�䣨
            //SuddenDie_Start = Timer.GetComponent<Timer>().timeStart - TotalSleepTime;
            
            //SuddenDie_text.text = (SuddenDie_Start/360).ToString("00.");
        }

        
        if (Sleepy_Start >= MaxSleepy)
        {
            //̫���˸ɲ��˹��� ��Ҫ��������ֵ
            Sleepy_Start = MaxSleepy;
            FungusFlowchart.SetBooleanVariable("Working", false);
            FungusFlowchart.SetBooleanVariable("SleepyNotice", true);
            FungusFlowchart.SetBooleanVariable("ClickAccess", true);
            Work.SetActive(false);
            Yawn2.Play(); Yawn3.Play();
        }
        //��workbtn ������ֹͣ����
        //WorkBtn_text.text = FungusFlowchart.GetBooleanVariable("Working") ? "Pause" : "Work";

        if (Work_Start>= MaxWork)
        {
            //�������
        }

        if(Timer.GetComponent<Timer>().timeStart>= Timer.GetComponent<Timer>().MaxTime&& Work_Start< MaxWork)
        {
            //����ʧ��
        }

        //��ʾslider
        Sleepy_Slider.value = Sleepy_Start;
        Work_Slider.value = Work_Start;
        //SuddenDie_Slider.value = SuddenDie_Start;

        //���˯������....................................................................................
        //short Sleep
        if(FungusFlowchart.GetIntegerVariable("SleepType")==0)
        {
            //û˯
        }
        else if (FungusFlowchart.GetIntegerVariable("SleepType") == 1)
        {
            FungusFlowchart.SetBooleanVariable("Working", false);
            //short sleep С˯����ʱ��������(30min-60min)������ֵ-30%������Ч��+20���кܴ���ʻ��ٴ�˯�ţ�50%��������ǿ���𴲣�70%��������������ֵ��5��

            SleepTime = Random.Range(15f,30f); //����˯��ʱ��       
            Sleepy_Start -= 9;//������
            if(Sleepy_Start<0)
            {
                Sleepy_Start = 0;
            }


            Timer.GetComponent<Timer>().timeStart += SleepTime;//˯��ʱ��ӵ���ʱ��

            //��ʼ��SleepTime
            TotalSleepTime += SleepTime;
            SleepTime = 0;
            FungusFlowchart.SetIntegerVariable("SleepType", 0);
        }
        else if(FungusFlowchart.GetIntegerVariable("SleepType") == 2)
        {
            FungusFlowchart.SetBooleanVariable("Working", false);
            //long sleep ˯������ʱ����6h~9h�������������������Ч�ʼ�����30%��С˯��30min~50min��
            SleepTime = Random.Range(180f, 270f);//����˯��ʱ��       
            Sleepy_Start = 0;//������

            Timer.GetComponent<Timer>().timeStart += SleepTime;//˯��ʱ��ӵ���ʱ��
            //��ʼ��SleepTime
            TotalSleepTime += SleepTime;
            SleepTime = 0;
            FungusFlowchart.SetIntegerVariable("SleepType",0);
        }
        //��⹤������

        //���slack off ����..................................................................................
        if (FungusFlowchart.GetIntegerVariable("SlackType")==0)
        {
            //û����
        }
        
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 1)
        {
            //��steam
            PlaySteamGame();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 2)
        {
            //���ֻ�
            PlayMobileGame();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 3)
        {
            //��ʺ
            Shit();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 4)
        {
            //������
            OrderMealOnline();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 5)
        {
            //�Է�
            TakeFood();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 6)
        {
            //ϴ��
            TakeShower();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 7)
        {
            //�ȿ���
            TakeCoffee();
        }
        else if (FungusFlowchart.GetIntegerVariable("SlackType") == 8)
        {
            //��monster
            TakeMonster();
        }
    }

    //slackoff 1 
    public void PlaySteamGame()
    {
        Debug.Log("Play steam game");
        FungusFlowchart.SetBooleanVariable("Working", false);
        //��steam����Ϸ��ʱ�䣨1h~5h������ֵ -50% ����Ч��-50
        SlackOffTime = Random.Range(30f, 150f);
        Sleepy_Start -= 20;//������
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//����ʱ��ӵ���ʱ��
        //��ʼ��SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);

    }
    public void PlayMobileGame()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //���ֻ�������Ϸ ����ֵ -90��30min~2h��������Ч�� - 80 %
        SlackOffTime = Random.Range(15f, 60f);
        Sleepy_Start -= 10;//������
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//����ʱ��ӵ���ʱ��
        //��ʼ��SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void OrderMealOnline()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //��������ѡ��־�֢ ��10~40min��
        SlackOffTime = Random.Range(1f, 20f);

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//����ʱ��ӵ���ʱ��
        //��ʼ��SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void Shit()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //�ϲ������󣩣����ֻ�������Ƶ��ˢ΢�� twitter������ - 50
        SlackOffTime = Random.Range(5f, 30f);
        Sleepy_Start -= 15;//������
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//����ʱ��ӵ���ʱ��
        //��ʼ��SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void TakeShower()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //ϴ�裺����ֵ - 70��5min~10min��ֻ��ϴ����
        SlackOffTime = Random.Range(1f, 5f);
        Sleepy_Start -= 21;//������
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//����ʱ��ӵ���ʱ��
        //��ʼ��SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void TakeCoffee()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //�ȿ��ȣ�����ֵ -10 ��5 min��
        SlackOffTime = 3f;
        Sleepy_Start -= 3;//������
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//����ʱ��ӵ���ʱ��
        //��ʼ��SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void TakeMonster()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //�ȹ������ϣ�����ֵ -20 ��5 min��
        SlackOffTime = 3f;
        Sleepy_Start -= 6;//������
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//����ʱ��ӵ���ʱ��
        //��ʼ��SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
    public void TakeFood()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        //�ȹ������ϣ�����ֵ -20 ��5 min��
        SlackOffTime = 30f;
        Sleepy_Start -= 15;//������
        if (Sleepy_Start < 0)
        {
            Sleepy_Start = 0;
        }

        Timer.GetComponent<Timer>().timeStart += SlackOffTime;//����ʱ��ӵ���ʱ��
        //��ʼ��SleepTime
        SlackOffTime = 0;
        FungusFlowchart.SetIntegerVariable("SlackType", 0);
    }
}
