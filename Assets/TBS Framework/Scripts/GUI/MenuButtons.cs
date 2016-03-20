using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	public GameObject startPanel;
	public GameObject startButton;


	// Use this for initialization
	void Start () {
		startButton.SetActive (true);
		startPanel.SetActive (false);

	}


	public void onQuitButtonClick()
	{
		Application.Quit ();
		Debug.Log ("Quit triggered");
	}

	public void onStartButtonClick()
	{
		startPanel.SetActive (true);
		startButton.SetActive (false);
	}

	public void onVersusModeButtonClick()
	{
		SceneManager.LoadScene ("VS_Side_Select" ,LoadSceneMode.Single);
	}

	// Update is called once per frame
	void Update () {
		

	
	}
		
}