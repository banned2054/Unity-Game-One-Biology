using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMgr : MonoBehaviour
{
    public int Today;
    private float _dayPerTime;
    private float _timeGoes;
    private bool _beginFlow;

    public List<UpdateObject> UpdateList;

    void Start()
    {
        _beginFlow = false;
    }

    public void Init(DataSo dataSo)
    {
        Today = dataSo.Date;
        _dayPerTime = dataSo.TimeSpeed;
        _timeGoes = 0;
        _beginFlow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_beginFlow)
        {
            _timeGoes += Time.deltaTime;
            if (_timeGoes >= _dayPerTime)
            {
                UpdateDate();
            }
        }
    }

    void UpdateDate()
    {
        _timeGoes = 0;
        Today++;
        for (int i = 0; i < UpdateList.Count; i++)
        {
            UpdateList[i].UpdateTime();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _beginFlow = false;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        _beginFlow = true;
    }
}