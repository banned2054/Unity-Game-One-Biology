using Unity.VisualScripting;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public DataSo _DataSo;
    public GroundGroupSo _GroundGroundSO;
    public ChangeSo ChangeSo;

    [SerializeField] private GameObject _maskObject;
    [SerializeField] private GameObject _infoPanel;
    private GameObject _events;
    private GroundMgr _groundMgr;

    void Start()
    {
        _events = GameObject.Find("Events");
        Init();
    }

    void Init()
    {
        _groundMgr = this.AddComponent<GroundMgr>();
        _groundMgr.Init(_GroundGroundSO, _DataSo, _maskObject, _infoPanel);

        TimeMgr timeMgr = _events.GetComponent<TimeMgr>();
        timeMgr.Init();
    }
}