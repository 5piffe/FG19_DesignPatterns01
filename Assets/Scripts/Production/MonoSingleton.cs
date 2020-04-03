using System;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T :  MonoSingleton<T>
{
    private static T m_Instance;

    public static T Instance {
		get
		{
			if (m_Instance == null)
			{
				T[] instances = FindObjectsOfType<T>();

				if (instances.Length > 1)
				{
					throw new InvalidOperationException($"There is more than one { typeof(T).Name } instance in the scene");
				}

				if (instances.Length > 0)
				{
					m_Instance = instances[0];
				}

				if (m_Instance == null)
				{
					GameObject singletonPrefab = Resources.Load<GameObject>(typeof(T).Name);

					if (singletonPrefab == null)
					{
						throw new NullReferenceException($"There is no { typeof(T).Name } prefab in the Resources folder");
					}
					
					GameObject gOInstance = Instantiate(singletonPrefab);
					m_Instance = gOInstance.GetComponent<T>();

					if (m_Instance == null)
					{
						throw new NullReferenceException($"There is no { typeof(T).Name } component attached to the singleton prefab");	
					}
				}				
				DontDestroyOnLoad(m_Instance.gameObject);
			}	
			return m_Instance;
		}    
	}

	protected virtual void Awake()
	{
		if (m_Instance == null)
		{
			m_Instance = (T)this; //GetComponent<T>();
			DontDestroyOnLoad(gameObject);
		} 
		else if (m_Instance != this)
		{
			throw new InvalidOperationException($"There is more than one { typeof(T).Name } instance in the scene");
		}		
	}
}
