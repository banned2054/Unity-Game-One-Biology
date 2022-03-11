using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundUpdate : UpdateObject
{
    private GameObject groundsGameObject;
    private GroundGroupSo _groundGroupSo;
    private List<GroundSo> _grounds;
    DataSo _dataSo;

    public void Init(GroundGroupSo groundGroupSo, DataSo dataSo)
    {
        groundsGameObject = GameObject.Find("Grounds");
        _dataSo = dataSo;
        _groundGroupSo = groundGroupSo;
        _grounds = _groundGroupSo.Grounds;
    }

    public override void UpdateTime()
    {
        _grounds = _groundGroupSo.Grounds;
        int plantNumb = 0;
        _dataSo.Population = 0;
        for (int i = 0; i < _grounds.Count; i++)
        {
            if (_grounds[i].BiologyNumb == 0) continue;
            BiologySo biologySo = _grounds[i].GroundBiologySo;
            int currentBiologyNumb = _grounds[i].BiologyNumb;
            _dataSo.Population += currentBiologyNumb;
            if (biologySo.Numb == 8)
            {
                _dataSo.TechnologyPoint += 5 * currentBiologyNumb;
                _dataSo.Money += 17 * currentBiologyNumb;
            }

            if (biologySo.Numb < 8 && biologySo.Numb > 4)
            {
                _dataSo.TechnologyPoint += 2 * currentBiologyNumb;
                _dataSo.Money += 2 * currentBiologyNumb;
            }

            if (biologySo.Numb < 5)
            {
                _dataSo.Money += currentBiologyNumb / 4;
                _dataSo.TechnologyPoint += currentBiologyNumb / 2;
                plantNumb += currentBiologyNumb;
            }
        }
        CountRain(plantNumb);
    }

    void CountRain(int plantNumb)
    {
        float rainPercent = 0.02f + Mathf.Log(plantNumb, 2) / 20f;
        float precent = Random.Range(0, 1.0f);
        if (precent < rainPercent)
        {
            for (int i = 0; i < 100; i++)
            {
                _groundGroupSo.Grounds[i].Water += 3f;
            }
        }
        else
        {
            for (int i = 0; i < 100; i++)
            {
                if (_grounds[i].BiologyNumb != 0 && _grounds[i].GroundBiologySo.Numb < 6)
                {
                    _groundGroupSo.Grounds[i].Water -=
                        _grounds[i].Water * 0.01f * (1 - Mathf.Log(_grounds[i].BiologyNumb, 2) / 10);
                }
                else
                {
                    _groundGroupSo.Grounds[i].Water *= 0.98f;
                }
            }
        }

        for (int i = 0; i < 100; i++)
        {
            if (_groundGroupSo.Grounds[i].Water > 100) _groundGroupSo.Grounds[i].Water = 100f;
            if (_groundGroupSo.Grounds[i].Water < 0) _groundGroupSo.Grounds[i].Water = 0;

            for (int j = 2; j > -1; j--)
            {
                if (_groundGroupSo.Grounds[i].Water >= _dataSo.GroundLevel[j])
                {
                    groundsGameObject.transform.GetChild(i).GetComponent<Renderer>().material = _dataSo.Materials[j];
                    break;
                }
            }
        }
    }
}