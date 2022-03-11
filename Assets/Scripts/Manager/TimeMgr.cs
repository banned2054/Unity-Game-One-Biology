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

    [SerializeField] private Text _moneyText;
    [SerializeField] private Text _techPointText;
    [SerializeField] private Text _populationText;
    [SerializeField] private Text _timeText;
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
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (_beginFlow)
        {
            _timeGoes += Time.deltaTime;
            if (_timeGoes >= _dayPerTime)
            {
                //Debug.Log(_timeGoes);
                Debug.Log(_dayPerTime);
                _timeGoes = 0;
                UpdateDate();
            }
        }
    }

    void UpdateDate()
    {
        Debug.Log("update");
        _timeGoes = 0;
        _dataSo.Day++;
        if (_dataSo.Day > 12)
        {
            _dataSo.Day = 1;
            _dataSo.Month++;
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
        UpdateUI();
    }

    public void UpdateUI()
    {
        _timeText.text = _dataSo.Month.ToString() + "月" + _dataSo.Day.ToString() + "日";
        _techPointText.text = _dataSo.TechnologyPoint.ToString();
        _moneyText.text = _dataSo.Money.ToString() + "块";
        _populationText.text = _dataSo.Population.ToString();
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