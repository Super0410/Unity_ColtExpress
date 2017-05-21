using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header ("Zoom")]
	public Vector2 cameraSizeMinMax = new Vector2 (2, 6);
	public float zoomScale = 2;
	public float zoomSpeed = 8;
	Camera m_camera;
	float cameraSize;

	[Header ("Move")]
	public Vector2 cameraMoveXMinMax = new Vector2 (-3, 3);
	public Vector2 cameraMoveYMinMax = new Vector2 (-3, 3);
	public float moveSpeed = 8;
	public float moveScale = 0.01f;
	Vector2 lastFrameMousePos;
	public Vector3 cameraPos;


	void Start ()
	{
		m_camera = GetComponent<Camera> ();
		cameraSize = m_camera.orthographicSize;
		cameraPos = transform.position;
	}

	void Update ()
	{
		float mouseScroll = Input.GetAxis ("Mouse ScrollWheel");
		if (mouseScroll != 0) {
			cameraSize += mouseScroll * zoomScale;
			cameraSize = Mathf.Clamp (cameraSize, cameraSizeMinMax.x, cameraSizeMinMax.y);

			cameraPos.x = Mathf.Clamp (cameraPos.x, 1.3f * cameraSize - 12, cameraMoveXMinMax.y);
		}

		if (Input.GetMouseButtonDown (0)) {
			lastFrameMousePos = Input.mousePosition;
		}
		if (Input.GetMouseButton (0)) {
			float distX = Input.mousePosition.x - lastFrameMousePos.x;
			cameraPos.x += -distX * moveScale;
			cameraPos.x = Mathf.Clamp (cameraPos.x, 1.3f * cameraSize - 12, cameraMoveXMinMax.y);

			float distY = Input.mousePosition.y - lastFrameMousePos.y;
			cameraPos.y += -distY * moveScale;
			cameraPos.y = Mathf.Clamp (cameraPos.y, cameraMoveYMinMax.x, cameraMoveYMinMax.y);

			lastFrameMousePos = Input.mousePosition;
		}
	}

	public void ForcusOnPos (Vector3 targetPos)
	{
		targetPos.z = cameraPos.z;
		cameraPos = targetPos;
	}

	void LateUpdate ()
	{
		m_camera.orthographicSize = Mathf.Lerp (m_camera.orthographicSize, cameraSize, zoomSpeed * Time.deltaTime);
		transform.position = Vector3.Lerp (transform.position, cameraPos, moveSpeed * Time.deltaTime);
	}
}
