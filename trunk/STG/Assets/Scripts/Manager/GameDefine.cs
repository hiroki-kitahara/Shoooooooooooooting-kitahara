/*===========================================================================*/
/*
*     * FileName    : GameDefine.cs
*
*     * Description : .
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;


public class GameDefine : A_Singleton<GameDefine>
{
	/// <summary>
	/// プレイヤーミス時のイベントメッセージ.
	/// </summary>
	public static string MissEventMessage = "OnMiss";

	/// <summary>
	/// 敵死亡時のイベントメッセージ.
	/// </summary>
	public static string DeadEventMessage = "OnDead";

	/// <summary>
	/// 敵ダメージ時のイベントメッセージ.
	/// </summary>
	public static string DamageEventMessage = "OnDamage";

	/// <summary>
	/// EnemyShotCreatorが全弾出し切った際のメッセージ.
	/// </summary>
	public static string EnemyShotCreatorFreezeMessage = "OnFreezeEnemyShotCreator";

	/// <summary>
	/// 銃口がアクティブになった際のメッセージ.
	/// </summary>
	public static string ActiveMuzzleMessage = "OnActiveMuzzle";

	public float ColliderLayer = 0.0f;
	
	public override void Awake()
	{
		base.Awake();
		Instance = this;
	}
}
