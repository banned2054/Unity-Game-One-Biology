using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New ground", menuName = "Save/New ground")]
public class GroundSo : ScriptableObject
{
    public int SerialNumb;              //当前土地序号
    public int BiologyNumb;             //生物数量
    public float Water;                 //土地水分
    public BiologySo GroundBiologySo;   //生物信息
}