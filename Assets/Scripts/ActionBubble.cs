﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class ActionBubble : MonoBehaviour {

	public GameObject bubbleContainer;
	public GameObject buttonPrefab; 

	private List<Unit> Units;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CanvasRegisterCallBacks(){
		Units = GameObject.Find ("CellGrid").GetComponent<CellGrid> ().Units;
		foreach (Unit unit in Units)
		{
			unit.UnitSelected += OnUnitSelected;
			unit.UnitDeselected += OnUnitDeselected;
		}
	}
		
	private void OnUnitSelected(object sender, EventArgs e)
	{
		if (sender is UnitAvecActions) {
			UnitAvecActions unitAvecAction = (UnitAvecActions)sender;
			List<UnitAction> unitActions = unitAvecAction.actions;
			foreach (UnitAction unitAction in unitActions) {
				GameObject newGO = GameObject.Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				newGO.transform.SetParent(bubbleContainer.transform);
			}
			if (bubbleContainer.transform.childCount > 0) {
				bubbleContainer.transform.position = Camera.main.WorldToScreenPoint (unitAvecAction.transform.position);
				bubbleContainer.SetActive (true);
			}
		}
	}

	private void OnUnitDeselected(object sender, EventArgs e)
	{ 
		bubbleContainer.SetActive (false);
		foreach(Transform child in bubbleContainer.transform) {
			Destroy(child.gameObject);
		}
	}
}
