using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SO
{
    [CreateAssetMenu(fileName = "New biology", menuName = "Save/New biology")]
    public class BiologySo : ScriptableObject
    {
        [FormerlySerializedAs("Numb")]
        public int numb;

        [FormerlySerializedAs("BiologyName")]
        public string biologyName;

        [FormerlySerializedAs("BiologySprite")]
        public Sprite biologySprite;

        [FormerlySerializedAs("Price")]
        public float price;

        [FormerlySerializedAs("Size")]
        public float size;

        [FormerlySerializedAs("Distance")]
        public int distance;

        public float     MinWater;
        public List<int> Needs;

        public Vector3 PositionOffset;

        public BiologySo(BiologySo biologySo)
        {
            numb          = biologySo.numb;
            biologyName   = biologySo.biologyName;
            biologySprite = biologySo.biologySprite;
            price         = biologySo.price;
            size          = biologySo.size;
            distance      = biologySo.distance;
            MinWater      = biologySo.MinWater;
            Needs         = new List<int>(biologySo.Needs);
        }
    }
}
