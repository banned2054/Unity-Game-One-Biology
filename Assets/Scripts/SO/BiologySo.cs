using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New biology", menuName = "Save/New biology")]
public class BiologySo : ScriptableObject
{
    public int Numb;
    public string BiologyName;

    public Sprite BiologySprite;
    public float Price;

    public float Size;
    public int Distance;

    public float MinWater;
    public List<int> Needs;

    public Vector3 PositionOffset;

    public BiologySo(BiologySo biologySo)
    {
        Numb = biologySo.Numb;
        BiologyName = biologySo.BiologyName;
        BiologySprite = biologySo.BiologySprite;
        Price = biologySo.Price;
        Size = biologySo.Size;
        Distance = biologySo.Distance;
        MinWater = biologySo.MinWater;
        Needs = new List<int>(biologySo.Needs);
    }
}