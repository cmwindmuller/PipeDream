using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SurfHUD : MonoBehaviour {

	public int points;
	public Text scoreboard;
	public GameObject pointPrefab;
	public GameObject lifePrefab;
	private GameObject[] lifeIcon;

	public void Awake()
	{
		points = 0;
	}

	public void GainPoints(int amount)
	{
		points += amount;
		UpdateScore();
	}
	public void UpdateScore()
	{
		scoreboard.text = points.ToString();
	}

	public void UpdateLives(int lives)
	{
		if(lives < 0)
		{
			//Game over, man. Game over!
			return;
		}
		if(lifeIcon != null)
		for(int i=0;i<lifeIcon.Length;i++)
		{
			Destroy(lifeIcon[i].gameObject);
		}

		lifeIcon = new GameObject[lives];

		for(int i=0;i<lives;i++)
		{
			lifeIcon[i] = Instantiate(lifePrefab);
			lifeIcon[i].transform.SetParent(this.transform,false);
		}
		for(int i=0;i<lives;i++)
		{
			lifeIcon[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(32 * (i + 1),-32);
		}
	}
	
	public void RenderPoints(int points)
	{
		GameObject newPoint = Instantiate(pointPrefab);
		newPoint.GetComponent<Text>().text = "+"+points.ToString();
		newPoint.transform.SetParent(this.transform, false);
	}
}
