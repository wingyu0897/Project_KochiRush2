using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolingPair
{
    public PoolableMono prefab;
    public int count;
}
 
[CreateAssetMenu(menuName = "SO/Pool/PoolingList")]
public class PoolingList : ScriptableObject
{
    public List<PoolingPair> list;
}
