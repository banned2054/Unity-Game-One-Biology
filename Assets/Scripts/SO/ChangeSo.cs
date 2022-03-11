using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeSo : MonoBehaviour
{
    [SerializeField] private DataSo dataSO;
    [SerializeField] private GroundGroupSo groundGroupSo;

    public class BiologyData
    {
        public int BiologySerialNumb; //土地上的生物序号
        public int BiologyNumb; //生物数量
    }

    public class GroundData
    {
        public float Water; //土地水分
    }

    public void InitializeDataSo()
    {
        dataSO.TechnologyPoint = 0;
        dataSO.Money = 50;
        dataSO.Population = 0;
        dataSO.CurrentLevel = 0;
    }

    public void InitializeGroundSo(GroundSo currentGroundSo, int serialNumb)
    {
        currentGroundSo.SerialNumb = serialNumb;
        currentGroundSo.BiologySerialNumb = -1;
        currentGroundSo.BiologyNumb = 0;
        currentGroundSo.Water = 20;
    }

    public void InitializeGroundGroupSo()
    {
        for (int i = 0; i < groundGroupSo.Grounds.Count; i++)
        {
            InitializeGroundSo(groundGroupSo.Grounds[i], i);
        }
    }

    public void UpdateData(float technologyPoint, float money, float population, int currentLevel)
    {
        if (technologyPoint >= 0) dataSO.TechnologyPoint = technologyPoint;
        if (money >= 0) dataSO.Money = money;
        if (population >= 0) dataSO.Population = population;
        if (currentLevel >= 0) dataSO.CurrentLevel = currentLevel;
    }

    public void UpdateGround(GroundSo groundSo, GroundData saveGround, BiologyData saveBiology)
    {
        groundSo.BiologySerialNumb = saveBiology.BiologySerialNumb;
        groundSo.BiologyNumb = saveBiology.BiologyNumb;

        groundSo.Water = saveGround.Water;
    }
}