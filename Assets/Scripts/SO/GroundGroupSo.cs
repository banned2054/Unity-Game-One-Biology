using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ground group", menuName = "Save/New ground group")]
public class GroundGroupSo : ScriptableObject
{
    public List<GroundSo> Grounds;
}