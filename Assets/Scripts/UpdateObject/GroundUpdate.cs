using SO;
using System.Collections.Generic;
using UnityEngine;

namespace UpdateObject
{
    public class GroundUpdate : UpdateBase
    {
        private GameObject     _groundsGameObject;
        private GroundGroupSo  _groundGroupSo;
        private List<GroundSo> _grounds;
        private DataSo         _dataSo;

        public void Init(GroundGroupSo groundGroupSo, DataSo dataSo)
        {
            _groundsGameObject = GameObject.Find("Grounds");
            _dataSo            = dataSo;
            _groundGroupSo     = groundGroupSo;
            _grounds           = _groundGroupSo.grounds;
        }

        public override void UpdateTime()
        {
            _grounds = _groundGroupSo.grounds;
            var plantNumb = 0;
            _dataSo.Population = 0;
            foreach (var ground in _grounds)
            {
                if (ground.BiologyNumb == 0) continue;
                var biologySo          = ground.GroundBiologySo;
                var currentBiologyNumb = ground.BiologyNumb;
                _dataSo.Population += currentBiologyNumb;
                switch (biologySo.numb)
                {
                    case 8 :
                        _dataSo.TechnologyPoint += 5  * currentBiologyNumb;
                        _dataSo.Money           += 17 * currentBiologyNumb;
                        break;
                    case < 8 and > 4 :
                        _dataSo.TechnologyPoint += 2 * currentBiologyNumb;
                        _dataSo.Money           += 2 * currentBiologyNumb;
                        break;
                    case < 5 :
                        _dataSo.Money           += currentBiologyNumb / 4f;
                        _dataSo.TechnologyPoint += currentBiologyNumb / 2f;
                        plantNumb               += currentBiologyNumb;
                        break;
                }
            }

            CountRain(plantNumb);
        }

        private void CountRain(int plantNumb)
        {
            var rainPercent = 0.02f + Mathf.Log(plantNumb, 2) / 20f;
            var precent     = Random.Range(0, 1.0f);
            if (precent < rainPercent)
            {
                for (var i = 0; i < 100; i++)
                {
                    _groundGroupSo.grounds[i].Water += 3f;
                }
            }
            else
            {
                for (var i = 0; i < 100; i++)
                {
                    if (_grounds[i].BiologyNumb != 0 && _grounds[i].GroundBiologySo.numb < 6)
                    {
                        _groundGroupSo.grounds[i].Water -=
                            _grounds[i].Water * 0.01f * (1 - Mathf.Log(_grounds[i].BiologyNumb, 2) / 10);
                    }
                    else
                    {
                        _groundGroupSo.grounds[i].Water *= 0.98f;
                    }
                }
            }

            for (var i = 0; i < 100; i++)
            {
                if (_groundGroupSo.grounds[i].Water > 100) _groundGroupSo.grounds[i].Water = 100f;
                if (_groundGroupSo.grounds[i].Water < 0) _groundGroupSo.grounds[i].Water   = 0;

                for (var j = 2; j > -1; j--)
                {
                    if (!(_groundGroupSo.grounds[i].Water >= _dataSo.GroundLevel[j])) continue;
                    _groundsGameObject.transform.GetChild(i).GetComponent<Renderer>().material = _dataSo.Materials[j];
                    break;
                }
            }
        }
    }
}
