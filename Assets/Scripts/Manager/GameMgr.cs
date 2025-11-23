using SO;
using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class GameMgr : MonoBehaviour
    {
        public DataSo        dataSo;
        public GroundGroupSo groundGroundSo;
        public ChangeSo      changeSo;

        [SerializeField]
        private GameObject maskObject;

        [SerializeField]
        private GameObject infoPanel;

        private GameObject _events;
        private GroundMgr  _groundMgr;

        private void Start()
        {
            _events = GameObject.Find("Events");
            Init();
        }

        private void Init()
        {
            _groundMgr = this.AddComponent<GroundMgr>();
            _groundMgr.Init(groundGroundSo, dataSo, maskObject, infoPanel);

            var timeMgr = _events.GetComponent<TimeMgr>();
            timeMgr.Init();
        }
    }
}
