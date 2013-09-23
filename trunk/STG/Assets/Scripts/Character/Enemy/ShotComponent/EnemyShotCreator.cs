/*===========================================================================*/
/*
*     * FileName    : EnemyShotCreator.cs
*
*     * Description : .
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyShotCreator : GameMonoBehaviour
{
	/// <summary>
	/// ショットID.
	/// </summary>
	public int shotId;
	
	/// <summary>
	/// 速度.
	/// </summary>
	public float speed;
	
	/// <summary>
	/// 発射間隔.
	/// </summary>
	public int interval;
	
	/// <summary>
	/// 発射数.
	/// </summary>
	public int number;
	
	/// <summary>
	/// 発射範囲.
	/// </summary>
	public float range;
	
	/// <summary>
	/// 発射ロックフラグ.
	/// </summary>
	public bool isLock = false;
	
	/// <summary>
	/// プレイヤーが自分より座標が上へ存在しているか.
	/// </summary>
	public bool isPlayerTop = false;
	
	/// <summary>
	/// 発射処理の休憩フラグ.
	/// </summary>
	private bool isSleep = false;
	
	/// <summary>
	/// 休憩フレーム.
	/// </summary>
	private int sleepFrame = 0;
	
	/// <summary>
	/// 現在の発射間隔.
	/// </summary>
	private int currentInterval = 0;
	
	/// <summary>
	/// 発射した総数.
	/// </summary>
	private int totalShotCount = 0;
	
	private List<EnemyShotCreateComponentBase> componentFromSetList = new List<EnemyShotCreateComponentBase>();
	private List<EnemyShotCreateComponentBase> componentFromAddList = new List<EnemyShotCreateComponentBase>();
	
	public override void Awake()
	{
		base.Awake();
		var list = new List<EnemyShotCreateComponentBase>( gameObject.GetComponents<EnemyShotCreateComponentBase>() );
		list.ForEach( (obj) =>
		{
			obj.Initialize( this );
			if( obj.setType == EnemyShotCreateComponent.SetType.Set )
			{
				componentFromSetList.Add( obj );
			}
			else if( obj.setType == EnemyShotCreateComponent.SetType.Add )
			{
				componentFromAddList.Add( obj );
			}
		});
	}
	
	public override void LateUpdate()
	{
		base.LateUpdate();
		if( !isSleep )
		{
			UpdateCreateShot();
		}
		else
		{
			UpdateSleep();
		}
	}
	/// <summary>
	/// 強制的に弾を発射させる.
	/// </summary>
	public void ForceFire()
	{
		enabled = true;
		isSleep = false;
		sleepFrame = 0;
		currentInterval = 0;
		UpdateCreateShot();
	}
	public void InitCurrentInterval( int value )
	{
		currentInterval = value;
	}
	
	public int Sleep
	{
		set
		{
			sleepFrame = value;
			isSleep = true;
		}
		get
		{
			return sleepFrame;
		}
	}
	
	public int TotalShotCount
	{
		get
		{
			return totalShotCount;
		}
	}
	protected void CreateShot()
	{
		// ロックされていたら撃たない.
		// プレイヤーが上へ存在していたら撃たない.
		if( isLock || isPlayerTop )
		{
			totalShotCount++;
			return;
		}
		
		if( number == 1 )
		{
			CreateShot( 0.0f );
		}
		else
		{
			float addRange = range / (number - 1);
			for( int i=0; i<number; i++ )
			{
				CreateShot( (addRange * i) - (range / 2.0f) );
			}
		}
		
		totalShotCount++;
	}
	protected void CreateShot( float _fixedAngle )
	{
		GameObject shot = (GameObject)InstantiateAsChild(
			ReferenceManager.refEnemyLayer,
			ReferenceManager.prefabEnemyShotList[shotId]
			);
		shot.GetComponent<EnemyShot>().Initialize( speed, transform, transform, _fixedAngle );
	}
	
	private void UpdateCreateShot()
	{
		if( currentInterval >= interval )
		{
			componentFromSetList.ForEach( (obj) =>
			{
				obj.Tuning();
			});
			componentFromAddList.ForEach( (obj) => 
			{
				obj.Tuning();
			});
			CreateShot();
			currentInterval = 0;
		}
		else
		{
			currentInterval++;
		}
	}
	
	private void UpdateSleep()
	{
		if( sleepFrame <= 0 )
		{
			isSleep = false;
		}
		else
		{
			sleepFrame--;
		}
	}
}
