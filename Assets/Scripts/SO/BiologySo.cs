using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New biology", menuName = "Save/New biology")]
public class BiologySo : ScriptableObject
{
    public int Numb;

    public float Size;
    public int Distance;

    public float MinWater;
    public List<int> Needs;
}