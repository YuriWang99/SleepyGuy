using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class WorkBtnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Mechanics;
    public GameObject Work;
    public Flowchart FungusFlowchart;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WorkBtn()
    {
        Mechanics.GetComponent<Mechanics>().Work_Start += 1f;
        Mechanics.GetComponent<Mechanics>().Sleepy_Start += 0.5f;
        Destroy(this.gameObject);
    }
    public void StopWorking()
    {
        FungusFlowchart.SetBooleanVariable("Working", false);
        FungusFlowchart.SetBooleanVariable("ClickAccess", true);
        Work.SetActive(false);

    }
}
