using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
	[SerializeField] private ScriptableTest scriptableTest;

	private void Awake()
	{
		Debug.Log(scriptableTest.ObjectName);
	}
}