using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
	[SerializeField] private Transform stickTrm;

	[Range(0, 1.0f)]
	[SerializeField] private float boundXRange = 0.8f;
	private float boundX;
	[SerializeField] private float posYOffset = 1f;
	private float posY;

	[SerializeField] private float moveYOffset = 1f;
	private float moveBoundY;

	private void Awake()
	{
		Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
		boundX = bottomRight.x * boundXRange;
		posY = bottomRight.y + posYOffset;
		moveBoundY = posY + moveYOffset;
		transform.position = new Vector3(0, posY, 0);
	}

	private void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
			if (pos.y < moveBoundY)
			{
				Move(pos);
			}
			else
			{
				Rotate(pos);
				if (touch.phase == TouchPhase.Ended)
				{
					//Shoot();
				}
			}
		}
	}

	#region Control

	private void Move(Vector3 touchPos)
	{
		transform.rotation = Quaternion.Euler(0, 0, 0);
		touchPos.x = Mathf.Clamp(touchPos.x, -boundX, boundX);
		transform.position = new Vector3(touchPos.x, posY, 0);
	}

	private void Rotate(Vector3 touchPos)
	{
		Vector3 direction = touchPos - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
	}

	#endregion
}
