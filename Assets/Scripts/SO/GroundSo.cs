using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New ground", menuName = "Save/New ground")]
public class GroundSo : ScriptableObject
{
    public int SerialNumb;              //��ǰ�������
    public int BiologyNumb;             //��������
    public float Water;                 //����ˮ��
    public BiologySo GroundBiologySo;   //������Ϣ
}