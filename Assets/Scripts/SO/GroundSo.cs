using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ground", menuName = "Save/New ground")]
public class GroundSo : ScriptableObject
{
    public int SerialNumb; //��ǰ�������
    public int BiologySerialNumb; //�����ϵ��������
    public int BiologyNumb; //��������
    public float Water; //����ˮ��
}