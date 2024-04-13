/*
*/
using UnityEngine;

public class StickController : MonoBehaviour
{
	[Header("Positiion & Movement")]

	[Range(0, 1.0f)]
	[SerializeField] private float boundXRange = 0.8f;
	private float boundX;
	[SerializeField] private float posYOffset = 1f;
	private float posY;
	[SerializeField] private float moveYOffset = 1f;
	private float moveBoundY;


	[Header("Test")]

	[SerializeField] private Projectile test;
	private Projectile projectile;

	private void Awake()
	{
		// ȭ���� ���� �ϴ��� ��ǥ�� �������� ��ġ�� ���� �� ���� �ʱ�ȭ
		Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
		boundX = bottomRight.x * boundXRange;
		posY = bottomRight.y + posYOffset;
		moveBoundY = posY + moveYOffset;
		transform.position = new Vector3(0, posY, 0);
	}

	private void Start()
	{
		projectile = PoolManager.Instance.Pop(test.gameObject.name) as Projectile;
		projectile.transform.SetParent(transform);
		projectile.transform.localPosition = Vector3.zero;
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
					projectile.Shoot(pos - transform.position, 30f);
					projectile.transform.SetParent(null);
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
