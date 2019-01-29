using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button button = GetComponent<Button>();
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;

        //entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(OnClick);

        trigger.triggers.Add(entry);
	}

    private void OnClick(BaseEventData arg0)
    {
        Debug.Log(arg0.selectedObject.transform.GetChild(0).name);
    }
}
