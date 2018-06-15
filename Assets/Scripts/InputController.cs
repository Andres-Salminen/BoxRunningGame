using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyCallbackPair
{
	public KeyCode Key;
	public UnityAction Callback;
}

public class InputController : MonoBehaviour {

	private List<KeyCallbackPair> _getKeyCallbackList = new List<KeyCallbackPair>();

	private List<KeyCallbackPair> _getKeyDownCallbackList = new List<KeyCallbackPair>();

	public static InputController Instance;


	void Awake () {
		if (Instance == null)
			Instance = this;
		else
			Destroy(this);
	}
	
	void Update () {


		for (int i = 0; i < _getKeyCallbackList.Count; ++i)
		{
			if (Input.GetKey(_getKeyCallbackList[i].Key))
			{
				_getKeyCallbackList[i].Callback();
			}
		}



		for (int i = 0; i < _getKeyDownCallbackList.Count; ++i)
		{
			if (Input.GetKeyDown(_getKeyDownCallbackList[i].Key))
			{
				_getKeyDownCallbackList[i].Callback();
			}
		}


	}

	public void AddGetKeyInput(KeyCode key, UnityAction callback)
	{
		KeyCallbackPair newInputPair = new KeyCallbackPair
		{
			Key = key,
			Callback = callback
		};

		_getKeyCallbackList.Add(newInputPair);
	}

	public void AddGetKeyDownInput(KeyCode key, UnityAction callback)
	{
		KeyCallbackPair newInputPair = new KeyCallbackPair
		{
			Key = key,
			Callback = callback
		};

		_getKeyDownCallbackList.Add(newInputPair);
	}
}
