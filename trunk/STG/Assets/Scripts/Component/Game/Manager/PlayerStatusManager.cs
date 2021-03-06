/*===========================================================================*/
/*
*     * FileName    :PlayerStatusManager.cs
*
*     * Description : プレイヤーステータス管理者.
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// プレイヤーステータス管理者.
/// </summary>
public class PlayerStatusManager : GameMonoBehaviour
{
	/// <summary>
	/// プレイヤーID.
	/// </summary>
	/// <value>The player identifier.</value>
	public int PlayerId{ set{ playerId = value; } get{ return playerId; } }
	[SerializeField]
	private int playerId;

	/// <summary>
	/// 残機数.
	/// </summary>
	/// <value>
	/// The life.
	/// </value>
	public int Life{ get{ return life; } }
	[SerializeField]
	private int life = 3;

	/// <summary>
	/// SPポイント.
	/// </summary>
	/// <value>The special point.</value>
	public float SpecialPoint{ get{ return specialPoint; } }
	[SerializeField]
	private float specialPoint;

	[SerializeField]
	private PlayerCreator refPlayerCreator;

	/// <summary>
	/// エクステンドするために必要なスコアリスト.
	/// </summary>
	[SerializeField]
	private List<string> extendScoreStringList;

	/// <summary>
	/// エクステンドするために必要なスコアリスト.
	/// ulongをシリアライズ出来ないので文字列からパース.
	/// </summary>
	private List<ulong> extendScoreList;
	
	[SerializeField]
	private float addSpecialPointGrazeMin;
	
	[SerializeField]
	private float addSpecialPointGrazeMax;

	[SerializeField]
	private AnimationCurve addSpecialPointGrazeCurve;

	/// <summary>
	/// エクステンド回数.
	/// </summary>
	private int extendCount = 0;

	/// <summary>
	/// ミス回数.
	/// </summary>
	public int MissCount{ get{ return missCount; } }
	private int missCount = 0;

	/// <summary>
	/// SPモードを使用した回数.
	/// </summary>
	public int UsedSpecialModeCount{ get{ return usedSpecialModeCount; } }
	private int usedSpecialModeCount = 0;

	/// <summary>
	/// SPポイント最大値.
	/// </summary>
	public const float MaxSpecialPoint = 100.0f;

	/// <summary>
	/// グレイズ時に加算される経験値.
	/// </summary>
	private const float AddGameLevelExperienceGraze = 2.5f;

	public override void Awake ()
	{
		PlayerId = GameStatusInterfacer.PlayerId;

		InitializeGameStatus();

		InitializeExtendScoreList();
		CreatePlayer();
	}

	public void AddSpecialPoint( float value )
	{
		specialPoint += value / (1.0f + (float)usedSpecialModeCount * 0.1f);
		specialPoint = specialPoint > MaxSpecialPoint ? MaxSpecialPoint : specialPoint;

		// デバッグが有効なら常に最大値にする.
		if( DebugManager.IsSpecialPointInfinity )
		{
			specialPoint = MaxSpecialPoint;
		}

		ReferenceManager.Instance.Player.BroadcastMessage( GameDefine.ModifiedSpecialPointMessage, specialPoint, SendMessageOptions.DontRequireReceiver );
	}

	public void UseSpecialMode( float needPoint )
	{
		specialPoint -= needPoint;
		usedSpecialModeCount++;
		ReferenceManager.Instance.Player.BroadcastMessage( GameDefine.ModifiedSpecialPointMessage, specialPoint, SendMessageOptions.DontRequireReceiver );
	}

	/// <summary>
	/// ミス処理.
	/// </summary>
	public void Miss()
	{
		if( DebugManager.IsNotLifeDecrement )	return;
		
		life--;
		life = life < 0 ? 0 : life;
		missCount++;
		usedSpecialModeCount = 0;

		ReferenceManager.Instance.refUILayer.BroadcastMessage( GameDefine.MissEventMessage, SendMessageOptions.DontRequireReceiver );
	}

	public void Extend( ulong score )
	{
		if( extendCount >= extendScoreList.Count )
		{
			return;
		}

		if( score < extendScoreList[extendCount] )
		{
			return;
		}

		extendCount++;
		life++;
		SoundManager.Instance.Play( "Extend" );
		ReferenceManager.Instance.refUILayer.BroadcastMessage( GameDefine.ExtendMessage );
	}

	public void Graze( Transform grazeObject )
	{
		if( ReferenceManager.Instance.refGameManager.BossType != GameDefine.BossType.Boss )
		{
			AddSpecialPoint( AddSpecialPointGraze );
			GameManager.AddGameLevelExperience( AddGameLevelExperienceGraze );
		}

		ReferenceManager.Instance.Player.Graze( grazeObject );
	}

	public void RegistGameStatus()
	{
		GameStatusInterfacer.Life = this.life;
		GameStatusInterfacer.SpecialPoint = this.specialPoint;
		GameStatusInterfacer.MissCount = this.missCount;
		GameStatusInterfacer.ExtendCount = this.extendCount;
		AddSpecialPoint( 0 );
	}

	public void Continue()
	{
		this.life = SaveData.Settings.Instance.Life;
		this.extendCount = 0;
	}

	public bool IsMaxSpecialPoint
	{
		get
		{
			return specialPoint >= MaxSpecialPoint;
		}
	}

	public void DebugChange( int id )
	{
		playerId = id;
		CreatePlayer();
	}

	private void InitializeExtendScoreList()
	{
		extendScoreList = new List<ulong>( extendScoreStringList.Count );
		extendScoreStringList.ForEach( s =>
		{
			extendScoreList.Add( ulong.Parse( s ) );
		});
	}

	/// <summary>
	/// プレイヤーの生成.
	/// </summary>
	private void CreatePlayer()
	{
		if( ReferenceManager.Player != null )
		{
			Destroy( ReferenceManager.Player.gameObject );
		}

		refPlayerCreator.OnCreate( PlayerId );
	}

	private void InitializeGameStatus()
	{
		this.life = GameStatusInterfacer.Life;
		this.specialPoint = GameStatusInterfacer.SpecialPoint;
		this.missCount = GameStatusInterfacer.MissCount;
		this.extendCount = GameStatusInterfacer.ExtendCount;
		this.usedSpecialModeCount = GameStatusInterfacer.UsedSpecialModeCount;
	}

	private float AddSpecialPointGraze
	{
		get
		{
			return Mathf.Lerp( addSpecialPointGrazeMin, addSpecialPointGrazeMax, addSpecialPointGrazeCurve.Evaluate( GameManager.GameLevelNormalize ) );
		}
	}
}
