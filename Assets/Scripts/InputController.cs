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

	private bool _callbacksDirtied;


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

		if (_callbacksDirtied)
			RemoveBrokenCallbacks();

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

	private void RemoveBrokenCallbacks()
	{
		_callbacksDirtied = false;
		List<KeyCallbackPair> removeList = new List<KeyCallbackPair>();

		for (int i = 0; i < _getKeyCallbackList.Count; ++i)
		{
			if (_getKeyCallbackList[i].Callback.Target != null 
			&& _getKeyCallbackList[i].Callback.Target.ToString() == "null")
				removeList.Add(_getKeyCallbackList[i]);
		}

		for (int i = 0; i < removeList.Count; ++i)
		{
			_getKeyCallbackList.Remove(removeList[i]);
			Debug.Log("Broken callback removed.");
		}

		removeList.Clear();

		for (int i = 0; i < _getKeyDownCallbackList.Count; ++i)
		{
			if (_getKeyDownCallbackList[i].Callback.Target != null 
			&& _getKeyDownCallbackList[i].Callback.Target.ToString() == "null")
				removeList.Add(_getKeyDownCallbackList[i]);
		}

		for (int i = 0; i < removeList.Count; ++i)
		{
			_getKeyDownCallbackList.Remove(removeList[i]);
			Debug.Log("Broken callback removed.");
		}

		removeList.Clear();
	}

	public void DirtyCallbacks()
	{
		_callbacksDirtied = true;
	}
}
