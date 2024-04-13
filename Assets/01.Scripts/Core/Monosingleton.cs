using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	private static bool isQuit = false;

    private static T instance;
    public static T Instance
	{
		get
		{
			if (isQuit)
			{
				instance = null;
			}

			if (instance == null)
			{
				instance = FindObjectOfType<T>();
				
				if (instance == null)
				{
					Debug.Assert(instance != null, $"Object of \"{typeof(T)}\" is not exist");
				}
				else
				{
					isQuit = false;
				}
			}

			return instance;
		}
	}

	private void OnDestroy()
	{
		isQuit = true;
	}
}
