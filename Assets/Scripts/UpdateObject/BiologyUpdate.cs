using SO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UpdateObject
{
    public class BiologyUpdate : UpdateBase
    {
        private GroundGroupSo _groundGroupSo;

        private int       _x, _y;
        private GroundSo  _selfGroundSo;
        private List<int> _needs;


        public void Init(GroundGroupSo groundGroupSo, BiologySo biologySo, int x, int y)
        {
            _groundGroupSo = groundGroupSo;

            _x = x;
            _y = y;

            _selfGroundSo                             = _groundGroupSo.grounds[x * 10 + y];
            _selfGroundSo.BiologyNumb                 = 1;
            _selfGroundSo.GroundBiologySo             = ScriptableObject.CreateInstance<BiologySo>();
            _selfGroundSo.GroundBiologySo.biologyName = biologySo.biologyName;
            _selfGroundSo.GroundBiologySo.numb        = biologySo.numb;
            _selfGroundSo.GroundBiologySo.MinWater    = biologySo.MinWater;
            _selfGroundSo.GroundBiologySo.distance    = biologySo.distance;
            _needs                                    = new List<int>(biologySo.Needs);
        }


        public override void UpdateTime()
        {
            var numb = _selfGroundSo.BiologyNumb;
            int maxNumb;
            CheckOut();

            if (_selfGroundSo.GroundBiologySo.numb is 0 or 2 or 3 or 5 or 6) //植物
            {
                var water        = _selfGroundSo.Water;
                var needWater    = _selfGroundSo.GroundBiologySo.MinWater;
                var waterPercent = Mathf.FloorToInt(water / needWater);
                maxNumb = waterPercent < 10 ? waterPercent : 10;

                var addNumb = Mathf.CeilToInt(maxNumb * (0.2f - Mathf.Abs(0.5f - numb * 1.0f / maxNumb) * 0.4f));

                _selfGroundSo.BiologyNumb += addNumb;
            }
            else //动物
            {
                var numbToEat = 0;
                for (var i = 0; i <= _selfGroundSo.GroundBiologySo.distance; i++)
                {
                    var j     = 0;
                    var flag1 = j      <= _selfGroundSo.GroundBiologySo.distance - i;
                    var flag2 = j      <= _y;
                    var flag3 = j + _y < 10;
                    for (j = 0; flag1 && flag2 && flag3; j++)
                    {
                        if (i + j == 0) continue;
                        var numb1 = (_x - i) * 10 + (_y - j);
                        var numb2 = (_x - i) * 10 + (_y + j);
                        var numb3 = (_x + j) * 10 + (_y - j);
                        var numb4 = (_x + j) * 10 + (_y + j);

                        numbToEat += CountTarget(numb1);
                        numbToEat += CountTarget(numb2);
                        numbToEat += CountTarget(numb3);
                        numbToEat += CountTarget(numb4);

                        flag1 = j      <= _selfGroundSo.GroundBiologySo.distance - i;
                        flag2 = j      <= _y;
                        flag3 = j + _y < 10;
                    }
                }

                maxNumb = numbToEat / 10;
                if (maxNumb >= 10) maxNumb = 10;

                CheckOut();
                numb = _selfGroundSo.BiologyNumb;
                var addNumb = Mathf.CeilToInt(maxNumb * (0.2f - Mathf.Abs(0.5f - numb * 1.0f / maxNumb) * 0.4f));
                _selfGroundSo.BiologyNumb += addNumb;
            }

            EndCheck(maxNumb);
        }

        private void CheckOut()
        {
            var numb = _selfGroundSo.BiologyNumb;
            _selfGroundSo.BiologyNumb = numb switch
            {
                < 0  => 1,
                > 10 => 10,
                _    => _selfGroundSo.BiologyNumb
            };
        }

        private void EndCheck(int maxNumb)
        {
            CheckOut();
            var numb = _selfGroundSo.BiologyNumb;
            if (numb > maxNumb && numb <= maxNumb * 1.1)
            {
                _selfGroundSo.BiologyNumb -= Mathf.CeilToInt(maxNumb * 0.1f);
            }
            else if (numb > maxNumb * 1.1)
            {
                var numbPercent = numb * 1.0f / (maxNumb * 5) - 0.2f;

                _selfGroundSo.BiologyNumb -= Mathf.CeilToInt(maxNumb * (0.01f + numbPercent));
            }

            CheckOut();
        }

        public int CountTarget(int target)
        {
            var grounds = _groundGroupSo.grounds;
            if (target < 0 || target >= 100 || grounds[target].BiologyNumb <= 0) return 0;
            var currentNumb = grounds[target].GroundBiologySo.numb;
            return _needs.Any(need => currentNumb == need) ? grounds[target].BiologyNumb : 0;
        }
    }
}
