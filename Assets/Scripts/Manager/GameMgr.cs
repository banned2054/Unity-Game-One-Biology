using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public DataSo _DataSo;
    public GroundGroupSo _GroundGroundSO;
    public ChangeSo ChangeSo;

    [SerializeField] private GameObject _maskObject;
    [SerializeField] private GameObject _infoPanel;
    private GroundMgr _groundMgr;

    void Start()
    {
        Init();
    }

    void Init()
    {
        GameObject groundMgr = new GameObject("GroundMgr");
        groundMgr.transform.parent = transform;
        _groundMgr = groundMgr.AddComponent<GroundMgr>();
        _groundMgr.Init(_GroundGroundSO, _DataSo, _maskObject,_infoPanel);
    }
}