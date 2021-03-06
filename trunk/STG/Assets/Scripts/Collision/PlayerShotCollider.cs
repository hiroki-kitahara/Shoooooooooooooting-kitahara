/*===========================================================================*/
/*
*     * FileName    : PlayerShotCollider.cs
*
*     * Description : .
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;


public class PlayerShotCollider : A_Collider
{
	/// <summary>
	/// 親オブジェクト参照.
	/// </summary>
	public GameObject refParent;
	
	/// <summary>
	/// 攻撃力.
	/// </summary>
	public int power;
	
	/// <summary>
	/// 衝突時の爆発プレハブ.
	/// </summary>
	public GameObject prefabExplosion;
	
	/// <summary>
	/// 衝突した際に死亡するか.
	/// </summary>
	[SerializeField]
	private bool isCollisionDead = true;
	
	/// <summary>
	/// 敵と衝突したか.
	/// </summary>
	private bool isEnemyCollision = false;

	/// <summary>
	/// 衝突した際のイベント.
	/// </summary>
	private const string CollisionMessage = "OnEnemyCollision";
	
	public override void Awake()
	{
		base.Awake();

		if( ReferenceManager == null )	return;

		ReferenceManager.refCollisionManager.AddPlayerShotCollider( this );
	}
	
	public override void Update()
	{
		base.Update();
		UpdatePositionZ();
	}

	public override void OnCollision (A_Collider target)
	{
		if( !isCollisionDead )	return;
		
		ObjectPool.Instance.GetGameObject( prefabExplosion, refParent.transform.position, prefabExplosion.transform.rotation );
		Destroy( refParent );
		SendMessage( CollisionMessage, SendMessageOptions.DontRequireReceiver );
		isEnemyCollision = true;
	}
	public override EType Type
	{
		get
		{
			return EType.PlayerShot;
		}
	}
	
	public bool IsEnemyCollision
	{
		get
		{
			return isEnemyCollision;
		}
	}
}
