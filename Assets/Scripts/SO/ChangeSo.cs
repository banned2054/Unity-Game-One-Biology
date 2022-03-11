using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeSo : MonoBehaviour
{
    [SerializeField] private DataSo _dataSO;
    [SerializeField] private GroundGroupSo _groundGroupSo;
    
    public void InitializeDataSo()
    {
        _dataSO.Day = 1;
        _dataSO.Month = 1;
        _dataSO.TechnologyPoint = 0;
        _dataSO.Money = 20;
        _dataSO.Population = 0;
        _dataSO.CurrentLevel = 0;
    }

    public void InitializeGroundSo(GroundSo currentGroundSo, int serialNumb)
    {
        currentGroundSo.SerialNumb = serialNumb;
        currentGroundSo.GroundBiologySo = null;
        currentGroundSo.BiologyNumb = 0;
        currentGroundSo.Water = 0;
    }

    public void InitializeGroundGroupSo()
    {
        for (int i = 0; i < _groundGroupSo.Grounds.Count; i++)
        {
            InitializeGroundSo(_groundGroupSo.Grounds[i], i);
        }
    }
}