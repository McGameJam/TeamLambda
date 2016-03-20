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

	public void onDeathMatch()
	{
		SceneManager.LoadScene ("DeathMatch" ,LoadSceneMode.Single);
	}
	public void onKingofHillClick()
	{
		SceneManager.LoadScene ("KingOfTheHill" , LoadSceneMode.Single);
	}
	public void onCreditClick()
	{
		SceneManager.LoadScene ("Credits" , LoadSceneMode.Single);
	}

	public void onReturnToMainMenu()
	{
		SceneManager.LoadScene ("EarlGym" , LoadSceneMode.Single);
	}
	// Update is called once per frame
	void Update () {
		

	
	}
		
}