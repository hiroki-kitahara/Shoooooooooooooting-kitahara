/*===========================================================================*/
/*
*     * FileName    : GameManager.cs
*
*     * Description : ゲーム管理者クラス.
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : GameMonoBehaviour
{
	/// <summary>
	/// ゲームレベル.
	/// </summary>
	public int GameLevel
	{
		set
		{
			gameLevel = Mathf.Clamp( value, GameLevelMinDifficulty, GameLevelMaxDifficulty );
		}
		get
		{
			return gameLevel;
		}
	}
	public int gameLevel = 0;

	[SerializeField]
	private GameDefine.DifficultyType difficultyType;

	[SerializeField]
	private int frameRate = 60;

	/// <summary>
	/// ゲームレベル経験値.
	/// </summary>
	/// <value>
	/// The game level experience.
	/// </value>
	public float GameLevelExperience{ get{ return gameLevelExperience; } }
	private float gameLevelExperience = 0;
	
	/// <summary>
	/// バリアに衝突した敵弾の数.
	/// </summary>
	public int CollisionEnemyShot{ get{ return collisionEnemyShot; } }
	private int collisionEnemyShot = 0;

	/// <summary>
	/// グレイズした回数.
	/// ボス戦はカウントされない.
	/// </summary>
	/// <value>The graze count.</value>
	public int GrazeCount{ get{ return grazeCount; } }
	private int grazeCount = 0;
	
	/// <summary>
	/// 敵を倒した数.
	/// </summary>
	public List<int> DestroyEnemyNumList{ get{ return destroyEnemyNumList; } }
	private List<int> destroyEnemyNumList = new List<int>();

	/// <summary>
	/// ボスタイプ.
	/// </summary>
	/// <value>The type of the boss.</value>
	public GameDefine.BossType BossType{ get; private set; }

	/// <summary>
	/// 裏ステージ突入フラグ.
	/// </summary>
	/// <value>The reverse stage flag list.</value>
	public List<bool> ReverseStageFlagList{ get; private set; }

	/// <summary>
	/// ゲームレベル最大値.
	/// </summary>
	private const int GameLevelMax = 100;
	
	public override void Awake()
	{
		base.Awake();
		Application.targetFrameRate = frameRate;
		GameLevel = GameLevel;

		for( int i=0; i<ReferenceManager.Instance.prefabEnemyList.Count; i++ )
		{
			destroyEnemyNumList.Add( 0 );
		}

		this.ReverseStageFlagList = new List<bool>( 3 );
		for( int i=0; i<3; i++ )
		{
			this.ReverseStageFlagList.Add( false );
		}
	}
	
	// Update is called once per frame
	public override void Update()
	{
		base.Update();

		if( Application.targetFrameRate != frameRate )
		{
			Application.targetFrameRate = frameRate;
		}
	}
	/// <summary>
	/// ゲームレベル経験値の加算処理.
	/// </summary>
	/// <param name='value'>
	/// Value.
	/// </param>
	public void AddGameLevelExperience( float value )
	{
		if( this.BossType != GameDefine.BossType.None )	return;

		gameLevelExperience += value;
		GameLevelUp();
	}
	
	public void AddGameLevel( int value )
	{
		if( this.BossType != GameDefine.BossType.None )	return;

		GameLevel += value;
	}
	/// <summary>
	/// 敵弾がバリアと衝突した際のゲームレベル経験値上昇処理.
	/// </summary>
	public void AddGameLevelExperienceFromEnemyShot( int deleteNum = 1 )
	{
		collisionEnemyShot += deleteNum;
		AddGameLevelExperience( deleteNum );
	}
	public void AddGrazeCount()
	{
		grazeCount++;
	}
	/// <summary>
	/// 敵死亡処理.
	/// </summary>
	public void DestroyEnemy( int enemyId )
	{
		if( enemyId == -1 )	return;
		
		if( enemyId >= destroyEnemyNumList.Count )
		{
			for( int i=0,imax=(enemyId + 1) - destroyEnemyNumList.Count; i<imax; i++ )
			{
				destroyEnemyNumList.Add( 0 );
			}
		}
		
		destroyEnemyNumList[enemyId]++;
	}

	public void SetBossType( GameDefine.BossType bossType )
	{
		this.BossType = bossType;
		ReferenceManager.Instance.refUILayer.BroadcastMessage( GameDefine.StartBossBattleMessage, SendMessageOptions.DontRequireReceiver );
	}

	public void EndBoss()
	{
		this.BossType = GameDefine.BossType.None;
		ReferenceManager.Instance.refUILayer.BroadcastMessage( GameDefine.EndBossBattleMessage, SendMessageOptions.DontRequireReceiver );
	}

	public void ReverseStageClear( int stageId )
	{
		this.ReverseStageFlagList[stageId] = true;
	}

	public void Miss()
	{
		if( this.BossType != GameDefine.BossType.None )	return;

		GameLevel -= Mathf.Min( 20, (GameLevel / 5) );
		gameLevelExperience = 0.0f;
	}

	/// <summary>
	/// デバッグで裏ステージクリアフラグを立てる.
	/// </summary>
	public void DebugReverseStageClear()
	{
		for( int i=0; i<this.ReverseStageFlagList.Count; i++ )
		{
			this.ReverseStageFlagList[i] = true;
		}
	}

	/// <summary>
	/// ゲームレベル上昇処理.
	/// </summary>
	private void GameLevelUp()
	{
		if( !IsGameLevelUp )	return;
		
		GameLevel++;
		gameLevelExperience = 0;
	}
	/// <summary>
	/// レベルアップに必要な経験値を返す.
	/// </summary>
	/// <returns>
	/// The need game experience.
	/// </returns>
	/// <param name='level'>
	/// Level.
	/// </param>
	private int GetNeedGameExperience( int level )
	{
		return (level * 5) + (level * 2);
	}
	/// <summary>
	/// レベルアップ可能であるか？.
	/// </summary>
	/// <value>
	/// <c>true</c> if this instance is game level up; otherwise, <c>false</c>.
	/// </value>
	private bool IsGameLevelUp
	{
		get
		{
			return gameLevelExperience >= GetNeedGameExperience( gameLevel + 1 );
		}
	}
	private int GameLevelMinDifficulty
	{
		get
		{
			int[] min = { 0, 0, 60, 100 };
			return min[(int)difficultyType];
		}
	}
	private int GameLevelMaxDifficulty
	{
		get
		{
			int[] max = { 30, 60, 100, 100 };
			return max[(int)difficultyType];
		}
	}
	/// <summary>
	/// ゲームレベルを正規化した値を返す.
	/// </summary>
	/// <value>
	/// The game level normalize.
	/// </value>
	public float GameLevelNormalize
	{
		get
		{
			return (float)gameLevel / (float)GameLevelMax;
		}
	}

	public bool IsAllReverseStageClear
	{
		get
		{
			return GameManager.ReverseStageFlagList.FindIndex( m => m == false ) == -1;
		}
	}
}
