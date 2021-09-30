using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBtnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Mechanics;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WorkBtn()
    {
        Mechanics.GetComponent<Mechanics>().Work_Start += 1;
        Destroy(this.gameObject);
    }
}
