using UnityEngine;
using UnityEngine.UI;

public class UINetworkError : MonoBehaviour 
{
	public Text errorName;
	public Text errorCause;
	public Text errorTip;

	public void ApplyNetworkError (NetworkError error)
	{
		gameObject.SetActive(true);
		
		errorName.text = error.Name;
		errorCause.text = error.Cause;
		errorTip.text = error.Tip;
	}

	public void RemoveNetworkError ()
	{
		gameObject.SetActive(false);
	}
}
