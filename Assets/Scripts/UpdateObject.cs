using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateObject : MonoBehaviour
{
    void Start()
    {
        TimeMgr timeMgr = GameObject.Find("Events").GetComponent<TimeMgr>();
        timeMgr.UpdateList.Add(this);
    }

    public virtual void UpdateTime()
    {
        Debug.Log("base");
    }
}