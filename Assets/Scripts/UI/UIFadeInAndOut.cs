using UnityEngine;
using UnityEngine.UI;

public class UIFadeInAndOut : MonoBehaviour 
{
	public Text textToFade;
	public float fadeValue;
	public float fadeTime;
	// Update is called once per frame
	void Update () 
	{
		fadeValue = Mathf.PingPong(fadeValue, fadeTime);

		//fadeValue = Mathf.Clamp01(fadeValue);

		textToFade.color = new Color(0,0,0,fadeValue);

	}
}
