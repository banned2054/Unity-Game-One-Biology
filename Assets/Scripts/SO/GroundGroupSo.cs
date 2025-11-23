using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "New ground group", menuName = "Save/New ground group")]
    public class GroundGroupSo : ScriptableObject
    {
        public List<GroundSo> grounds;
    }
}
