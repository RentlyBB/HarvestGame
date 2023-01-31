using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName =  "Events/Void Event Channel")]
public class VoidEventChannelSO : DescriptionBaseSO {

	public UnityAction OnEventRaised;

	public void RaiseEvent() {
		OnEventRaised?.Invoke();
	}
}
