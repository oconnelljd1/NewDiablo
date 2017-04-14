﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

	private Dictionary <string, UnityEvent> eventDictionary;
	private static EventManager eventManager;
	public static EventManager instance{
		get{ 
			if(!eventManager){
				eventManager = FindObjectOfType (typeof(EventManager)) as EventManager;
				if (!eventManager) {
					Debug.LogError ("There needs to be one active Event Manager script on a GmaeObject in yor scene.");
				} else {
					eventManager.InitializeManager();
				}
			}
			return eventManager;
		}
	}

	void InitializeManager(){
		if(eventDictionary == null){
			eventDictionary = new Dictionary<string, UnityEvent> ();	
		}
	}

	public static void StartListening(string eventName, UnityAction listener){
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.AddListener (listener);
		} else {
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void StopListening (string eventName, UnityAction listener){
		if (eventManager == null) {
			return;
		} else {
			UnityEvent thisEvent = null;
			if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
				thisEvent.RemoveListener (listener);
			}
		}
	}

	public static void TriggerEvent(string eventName){
		UnityEvent thisEvent = null;
		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
			thisEvent.Invoke ();
		}
	}

}
