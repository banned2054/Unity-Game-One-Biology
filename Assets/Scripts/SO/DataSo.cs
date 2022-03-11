using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[CreateAssetMenu(fileName = "New data", menuName = "Save/New data")]
public class DataSo : ScriptableObject
{
    public float TechnologyPoint; //科技点数
    public float Money; //货币
    public float Population; //人口
    public int CurrentLevel; //当前科技等级
    public int Month;
    public int Day;

    public float TimeSpeed; //一天在现实中所用时间

    public GameObject GroundPrefab; //代表土地的Prefab
    public GameObject BiologyPrefab; //代表生物的Prefab

    public List<float> GroundLevel; //大地升级点

    public List<float> TechnologyLevel; //科技升级点

    public List<BiologySo> BiologySos;

    public List<Material> Materials; //土地材料

    public List<Sprite> TreeIcons; //科技树图片
    public List<Sprite> TreeLockedIcons; //未解锁时科技树图片
}