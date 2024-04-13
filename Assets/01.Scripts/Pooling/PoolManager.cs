using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<string, Pool> pools;

	[SerializeField] private PoolingList poolList;

	private void Awake()
	{
		pools = new ();

		for (int i = 0; i < poolList.list.Count; ++i)
		{
			Pool pool = new Pool(poolList.list[i].prefab, transform);
			for (int j = 0; j < poolList.list[i].count; ++j)
			{
				PoolableMono inst = Instantiate(poolList.list[i].prefab, transform);
				inst.name = inst.name.Replace("(Clone)", "");
				pool.Push(inst);
			}
			pools.Add(poolList.list[i].prefab.gameObject.name, pool);
		}
	}
	
	public PoolableMono Pop(string pop)
	{
		if (pools.ContainsKey(pop))
		{
			return pools[pop].Pop();
		}
		else
		{
			Debug.Assert(pools.ContainsKey(pop), $"{pop} pool is not exist");
			return null;
		}
	}

	public void Push(PoolableMono push)
	{
		if (pools.ContainsKey(name))
		{
			push.transform.SetParent(transform);
			pools[name].Push(push);
		}
		else
		{
			Debug.Assert(pools.ContainsKey(name), $"{name} pool is not exist");
		}
	}
}
