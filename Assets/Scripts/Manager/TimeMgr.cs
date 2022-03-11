using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeMgr : MonoBehaviour
{
    private float _dayPerTime;
    private float _timeGoes;
    private bool _beginFlow;

    [SerializeField] private DataSo _dataSo;
    [SerializeField] private GameObject _treePanel;
    [SerializeField] private GameObject _titlePanel;

    public List<UpdateObject> UpdateList;

    void Start()
    {
        _beginFlow = false;
    }

    public void Init()
    {
        _dayPerTime = _dataSo.TimeSpeed;
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
        _dataSo.Today.Day++;
        if (_dataSo.Today.Day > 12)
        {
            _dataSo.Today.Day = 1;
            _dataSo.Today.Month++;
        }

        //计算科技点数
        {
            for (int i = _dataSo.TechnologyLevel.Count - 1; i >= 0; i--)
            {
                if (_dataSo.TechnologyPoint >= _dataSo.TechnologyLevel[i])
                {
                    _dataSo.CurrentLevel = i;
                    break;
                }
            }
        }


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