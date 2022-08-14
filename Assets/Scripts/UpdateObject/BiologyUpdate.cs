using System.Collections.Generic;
using UnityEngine;

public class BiologyUpdate : UpdateObject
{
    private GroundGroupSo _groundGroupSo;

    private int _x, _y;
    private GroundSo _selfGroundSo;
    private List<int> _needs;


    public void Init(GroundGroupSo groundGroupSo, BiologySo biologySo, int x, int y)
    {
        _groundGroupSo = groundGroupSo;

        _x = x;
        _y = y;

        _selfGroundSo = _groundGroupSo.Grounds[x * 10 + y];
        _selfGroundSo.BiologyNumb = 1;
        _selfGroundSo.GroundBiologySo = ScriptableObject.CreateInstance<BiologySo>();
        _selfGroundSo.GroundBiologySo.BiologyName = biologySo.BiologyName;
        _selfGroundSo.GroundBiologySo.Numb = biologySo.Numb;
        _selfGroundSo.GroundBiologySo.MinWater = biologySo.MinWater;
        _selfGroundSo.GroundBiologySo.Distance = biologySo.Distance;
        _needs = new List<int>(biologySo.Needs);
    }


    public override void UpdateTime()
    {
        int numb = _selfGroundSo.BiologyNumb;
        int maxNumb;
        CheckOut();

        if (_selfGroundSo.GroundBiologySo.Numb == 0 || _selfGroundSo.GroundBiologySo.Numb == 2 ||
            _selfGroundSo.GroundBiologySo.Numb == 3 || _selfGroundSo.GroundBiologySo.Numb == 5 ||
            _selfGroundSo.GroundBiologySo.Numb == 6) //ֲ��
        {
            float water = _selfGroundSo.Water;
            float needWater = _selfGroundSo.GroundBiologySo.MinWater;
            int waterPercent = Mathf.FloorToInt(water / needWater);
            maxNumb = waterPercent < 10 ? waterPercent : 10;

            int addNumb = 0;
            addNumb = Mathf.CeilToInt(maxNumb * (0.2f - Mathf.Abs(0.5f - numb * 1.0f / maxNumb) * 0.4f));

            _selfGroundSo.BiologyNumb += addNumb;
        }
        else //����
        {
            int numbToEat = 0;
            for (int i = 0; i <= _selfGroundSo.GroundBiologySo.Distance; i++)
            {
                int j = 0;
                bool flag1 = j <= _selfGroundSo.GroundBiologySo.Distance - i;
                bool flag2 = j <= _y;
                bool flag3 = j + _y < 10;
                for (j = 0; flag1 && flag2 && flag3; j++)
                {
                    if (i + j == 0) continue;
                    int numb1 = (_x - i) * 10 + (_y - j);
                    int numb2 = (_x - i) * 10 + (_y + j);
                    int numb3 = (_x + j) * 10 + (_y - j);
                    int numb4 = (_x + j) * 10 + (_y + j);

                    numbToEat += CountTarget(numb1);
                    numbToEat += CountTarget(numb2);
                    numbToEat += CountTarget(numb3);
                    numbToEat += CountTarget(numb4);

                    flag1 = j <= _selfGroundSo.GroundBiologySo.Distance - i;
                    flag2 = j <= _y;
                    flag3 = j + _y < 10;
                }
            }

            maxNumb = numbToEat / 10;
            if (maxNumb >= 10) maxNumb = 10;

            CheckOut();
            numb = _selfGroundSo.BiologyNumb;
            int addNumb = Mathf.CeilToInt(maxNumb * (0.2f - Mathf.Abs(0.5f - numb * 1.0f / maxNumb) * 0.4f));
            _selfGroundSo.BiologyNumb += addNumb;
        }

        EndCheck(maxNumb);
    }

    void CheckOut()
    {
        int numb = _selfGroundSo.BiologyNumb;
        if (numb < 0) _selfGroundSo.BiologyNumb = 1;
        if (numb > 10) _selfGroundSo.BiologyNumb = 10;
    }

    void EndCheck(int maxNumb)
    {
        CheckOut();
        int numb = _selfGroundSo.BiologyNumb;
        if (numb > maxNumb && numb <= maxNumb * 1.1)
        {
            _selfGroundSo.BiologyNumb -= Mathf.CeilToInt(maxNumb * 0.1f);
        }
        else if (numb > maxNumb * 1.1)
        {
            float numbPersent = numb * 1.0f / (maxNumb * 5) - 0.2f;
            _selfGroundSo.BiologyNumb -= Mathf.CeilToInt(maxNumb * (0.01f + numbPersent));
        }

        CheckOut();
    }

    public int CountTarget(int target)
    {
        List<GroundSo> grounds = _groundGroupSo.Grounds;
        if (target >= 0 && target < 100 && grounds[target].BiologyNumb > 0)
        {
            int currentNumb = grounds[target].GroundBiologySo.Numb;
            for (int k = 0; k < _needs.Count; k++)
            {
                if (currentNumb == _needs[k])
                {
                    return grounds[target].BiologyNumb;
                }
            }
        }

        return 0;
    }
}