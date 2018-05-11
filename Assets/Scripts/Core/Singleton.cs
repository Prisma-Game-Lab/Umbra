using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
	
	#region Fields

	/// <summary>
	/// The instance.
	/// </summary>
	private static T instance;

	#endregion

	#region Properties

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static T Instance
	{
		get
		{
			if ( instance == null )
			{
				instance = FindObjectOfType<T> ();
				if ( instance == null )
				{
					throw new UnityException("Game Logic Error - An instance of " + typeof(T) + 
												 " is needed in the scene, but there is none. " +
												 "Have you imported the _GLOBAL_CONFIG_ prefab into the scene?. " +
												 "Aborting execution.");
				}
			}
			return instance;
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	protected virtual void Awake ()
	{
		if ( instance == null )
		{
			instance = this as T;
			DontDestroyOnLoad ( gameObject );
		}
		else
		{
			Destroy ( gameObject );
		}
	}

	#endregion
	
}