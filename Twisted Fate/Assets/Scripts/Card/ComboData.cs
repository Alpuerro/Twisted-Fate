using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ComboData", menuName = "ScriptableObjects/ComboDataObject", order = 1)]
public class ComboData : ScriptableObject
{
    //list is order by level, it means [0], will be combo level 1 and [2] level 3
    public int[] healthUpData = new int[3];
    public int[] armourUpData = new int[3];
    public int[] drawData = new int[3];
    public int[] stunData = new int[3];
    public float[] damageUpData = new float[3];
}