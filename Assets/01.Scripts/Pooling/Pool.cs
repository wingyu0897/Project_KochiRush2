using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Stack<PoolableMono> pool;

	private PoolableMono mono;
	private Transform parent;

    public Pool(PoolableMono mono, Transform parent)
	{
		this.mono = mono;
		this.parent = parent;

		pool = new Stack<PoolableMono>();
	}

	public PoolableMono Pop()
	{
		PoolableMono pop;

		if (pool.Count > 0)
		{
			pop = pool.Pop();
		}
		else
		{
			pop = GameObject.Instantiate(mono, parent);
			pop.name = pop.name.Replace("(Clone)", "");
		}
		pop.gameObject.SetActive(true);
		pop.Init();
		return pop;
	}

	public void Push(PoolableMono push)
	{
		if (pool.Contains(push))
		{
			Debug.Assert(!pool.Contains(push), $"{push.name} is already in the pool");
			return;
		}
		push.gameObject.SetActive(false);
		pool.Push(push);
	}
}
