using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainShakeController : MonoBehaviour
{
	[SerializeField] TrainManager trainManager;
	[SerializeField] Vector2 shakeYMinMax;
	[SerializeField] Vector2 shakeThreholdMinMax;
	[SerializeField] float backSpeed;

	float preY;
	float shakeY;

	void Start ()
	{
		preY = transform.localPosition.y;
		StartCoroutine (shake ());
	}

	void LateUpdate ()
	{
		transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (transform.localPosition.x, preY, transform.localPosition.z), backSpeed * Time.deltaTime);
	}

	IEnumerator shake ()
	{
		float waitTime = Random.Range (shakeThreholdMinMax.x, shakeThreholdMinMax.y);
		yield return new WaitForSeconds (waitTime);
		shakeY = Random.Range (shakeYMinMax.x, shakeYMinMax.y);
		transform.localPosition = new Vector3 (transform.localPosition.x, shakeY, transform.localPosition.z);

		if (trainManager != null)
			trainManager.TrainShake ();
		
		StartCoroutine (shake ());
	}
}
