using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ground", menuName = "Save/New ground")]
public class GroundSo : ScriptableObject
{
    public int SerialNumb; //当前土地序号
    public int BiologySerialNumb; //土地上的生物序号
    public int BiologyNumb; //生物数量
    public float Water; //土地水分
}