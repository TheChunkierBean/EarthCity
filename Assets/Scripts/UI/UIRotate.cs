using UnityEngine;

public class UIRotate : MonoBehaviour 
{
	public float rotateSpeed;
	// Update is called once per frame
	void Update () 
	{ 
		GetComponent<RectTransform>().eulerAngles += new Vector3(0, 0, rotateSpeed * Time.deltaTime);
	}
}
