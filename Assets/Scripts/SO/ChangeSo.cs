using UnityEngine;

namespace SO
{
    public class ChangeSo : MonoBehaviour
    {
        [SerializeField]
        private DataSo dataSo;

        [SerializeField]
        private GroundGroupSo groundGroupSo;

        public void InitializeDataSo()
        {
            dataSo.Day             = 1;
            dataSo.Month           = 1;
            dataSo.TechnologyPoint = 0;
            dataSo.Money           = 20;
            dataSo.Population      = 0;
            dataSo.CurrentLevel    = 0;
        }

        public void InitializeGroundSo(GroundSo currentGroundSo, int serialNumb)
        {
            currentGroundSo.SerialNumb      = serialNumb;
            currentGroundSo.GroundBiologySo = null;
            currentGroundSo.BiologyNumb     = 0;
            currentGroundSo.Water           = 0;
        }

        public void InitializeGroundGroupSo()
        {
            for (var i = 0; i < groundGroupSo.grounds.Count; i++)
            {
                InitializeGroundSo(groundGroupSo.grounds[i], i);
            }
        }
    }
}
