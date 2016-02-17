using UnityEngine;
using System.Collections;

/************************************************
 *                 Game Manager                 *
 ************************************************
 *      This runs the game. This is the major   *
 * entry point for the application. It loads    *
 * the Unity scenes which make up the content   *
 * of the game.                                 *
 ************************************************/

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	private const float FADE_TIME = 0.3f;
	private const string LEVEL_MENU = "menu";
	private const string LEVEL_1 = "level1_ocean";

	private bool isLoaded;
	public bool isSurfActive;

	// Don't destroy this object at level change
	void Awake() {
		if(_instance == null)
		{
			_instance = this;
		}
		else Destroy(this);

		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		AutoFade.LoadLevel("",0,FADE_TIME*2,Color.black);
		Invoke("StartUp",FADE_TIME*2);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void StartUp()
	{
		WindmullerText.StartColor();
		Invoke("FirstLoad",(60/WindmullerText.bpm)*10+.75f);
	}

	private void FirstLoad()
	{
		LoadMenu();
	}

	public void LoadMenu()
	{
		if(AutoFade.Fading)
			return;
		//AudioHandler.get().PlayMenuMusic();
		AutoFade.LoadLevel(LEVEL_MENU,FADE_TIME*2,FADE_TIME,Color.black);
	}

	public void LoadSurf()
	{
		if(AutoFade.Fading)
			return;
		isSurfActive = true;
		AudioHandler.get().PlayGameMusic();
		AutoFade.LoadLevel(LEVEL_1,FADE_TIME,FADE_TIME,Color.black);
	}

	// Get singleton
	public static GameManager get()
	{
		if(_instance == null)
		{
			GameObject _gameManager = new GameObject();
			_gameManager.AddComponent<GameManager>();
			_instance = _gameManager.GetComponent<GameManager>();
		}
		return _instance;
	}
}
