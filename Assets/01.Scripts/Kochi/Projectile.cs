using UnityEngine;

public class Projectile : PoolableMono
{
	private Rigidbody2D rigid;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
	}

	public void Shoot(Vector2 direction, float power = 1f)
	{
		rigid.bodyType = RigidbodyType2D.Dynamic;
		rigid.AddForce(direction.normalized * power, ForceMode2D.Impulse);
	}

	public override void Init()
	{
	}
}
