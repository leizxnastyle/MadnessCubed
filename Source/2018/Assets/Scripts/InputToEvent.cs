using System;
using UnityEngine;

public class InputToEvent : MonoBehaviour
{
	public static GameObject goPointedAt { get; private set; }

	public Vector2 DragVector
	{
		get
		{
			return (!this.Dragging) ? Vector2.zero : (this.currentPos - this.pressedPosition);
		}
	}

	private void Start()
	{
		this.m_Camera = base.GetComponent<Camera>();
	}

	private void Update()
	{
		if (this.DetectPointedAtGameObject)
		{
			InputToEvent.goPointedAt = this.RaycastObject(UnityEngine.Input.mousePosition);
		}
		if (UnityEngine.Input.touchCount > 0)
		{
			Touch touch = UnityEngine.Input.GetTouch(0);
			this.currentPos = touch.position;
			if (touch.phase == TouchPhase.Began)
			{
				this.Press(touch.position);
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				this.Release(touch.position);
			}
			return;
		}
		this.currentPos = UnityEngine.Input.mousePosition;
		if (Input.GetMouseButtonDown(0))
		{
			this.Press(UnityEngine.Input.mousePosition);
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.Release(UnityEngine.Input.mousePosition);
		}
		if (Input.GetMouseButtonDown(1))
		{
			this.pressedPosition = UnityEngine.Input.mousePosition;
			this.lastGo = this.RaycastObject(this.pressedPosition);
			if (this.lastGo != null)
			{
				this.lastGo.SendMessage("OnPressRight", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	private void Press(Vector2 screenPos)
	{
		this.pressedPosition = screenPos;
		this.Dragging = true;
		this.lastGo = this.RaycastObject(screenPos);
		if (this.lastGo != null)
		{
			this.lastGo.SendMessage("OnPress", SendMessageOptions.DontRequireReceiver);
		}
	}

	private void Release(Vector2 screenPos)
	{
		if (this.lastGo != null)
		{
			GameObject x = this.RaycastObject(screenPos);
			if (x == this.lastGo)
			{
				this.lastGo.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
			}
			this.lastGo.SendMessage("OnRelease", SendMessageOptions.DontRequireReceiver);
			this.lastGo = null;
		}
		this.pressedPosition = Vector2.zero;
		this.Dragging = false;
	}

	private GameObject RaycastObject(Vector2 screenPos)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(this.m_Camera.ScreenPointToRay(screenPos), out raycastHit, 200f))
		{
			InputToEvent.inputHitPos = raycastHit.point;
			return raycastHit.collider.gameObject;
		}
		return null;
	}

	private GameObject lastGo;

	public static Vector3 inputHitPos;

	public bool DetectPointedAtGameObject;

	private Vector2 pressedPosition = Vector2.zero;

	private Vector2 currentPos = Vector2.zero;

	public bool Dragging;

	private Camera m_Camera;
}
