using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[CreateAssetMenu(fileName = "New data", menuName = "Save/New data")]
public class DataSo : ScriptableObject
{
    public class Date
    {
        public int Month;
        public int Day;
    }

    public float TechnologyPoint; //�Ƽ�����
    public float Money; //����
    public float Population; //�˿�
    public int CurrentLevel; //��ǰ�Ƽ��ȼ�
    public Date Today;


    public float TimeSpeed; //һ������ʵ������ʱ��

    public GameObject GroundPrefab; //�������ص�Prefab
    public GameObject BiologyPrefab; //���������Prefab

    public List<float> GroundLevel; //���������

    public List<float> TechnologyLevel; //�Ƽ�������

    public List<BiologySo> BiologySos;

    public List<Material> Materials; //���ز���

    public List<Sprite> TreeIcons; //�Ƽ���ͼƬ
    public List<Sprite> TreeLockedIcons; //δ����ʱ�Ƽ���ͼƬ
}