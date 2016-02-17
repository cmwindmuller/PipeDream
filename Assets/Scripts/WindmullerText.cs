using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]

public class WindmullerText : MonoBehaviour {

	private Text text;
	public float BPM;
	public static float bpm;
	private static float startTime;
	private string[] colors = {"lime","cyan","#fff000ff","magenta","#ff9000ff","#fff000ff","red","red","red","magenta"};
	private string name = "Windmuller";
	private string output;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		startTime = -1;
	}
	
	// Update is called once per frame
	void Update () {
		bpm = BPM;
		if(startTime >= 0)
		{
			float relativeTime = Time.time - startTime;
			output = name;
			int index = Mathf.FloorToInt(relativeTime/(60/bpm));
			//index = index%colors.Length;
			if(index < colors.Length)
			{
				output = output.Insert(index+1,"</color>");
				output = output.Insert(index,"<color="+colors[index]+">");
				text.text = output;
			}
			else text.text = name;
		}
	}

	public static void StartColor()
	{
		startTime = Time.time;
		AudioHandler.get().PlayLogoMusic();
	}
}
