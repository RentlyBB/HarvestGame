﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Vector3 Event Channel")]
public class Vector3EventChannelSO : ScriptableObject {

	public UnityAction<Vector3> OnEventRaised;

	public void RaiseEvent(Vector3 vector) {
		OnEventRaised?.Invoke(vector);
	}
}