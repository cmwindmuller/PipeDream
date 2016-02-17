using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuReporter : MonoBehaviour {

	public Text hiScore;
	private string prefix;
	public Vector3 worldMovement;
	private bool hasStarted;

	void Awake () {
		Impermanent.worldMovement = worldMovement;
		prefix = hiScore.text + " ";
		hiScore.text = prefix + RecordKeeper.GetHighScore().ToString();
	}

	void Start()
	{
		AudioHandler.get().Title();
		Invoke("BeginMusic",1.0f);
	}

	void BeginMusic()
	{
		AudioHandler.get().PlayMenuMusic();
	}

	void Update()
	{
		CheckGameStart();
	}

	void CheckGameStart()
	{
		//on any input, start the game
		if(Input.anyKeyDown || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !hasStarted)
		{
			hasStarted = true;
			AudioHandler.get().GameStart();
			AudioHandler.get().StopCurrentMusic();
			Invoke("StartGame",1.0f);
			//StartGame();
		}
	}

	void StartGame()
	{
		GameManager.get().LoadSurf();
	}
}
