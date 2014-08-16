﻿/*===========================================================================*/
/*
*     * FileName    : TextureFlashFromEnemyHitPoint.cs
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 敵のヒットポイントによってテクスチャの色を赤く点滅させるコンポーネント.
/// </summary>
public class TextureFlashFromEnemyHitPoint : GameMonoBehaviour
{
	[SerializeField]
	private EnemyController refEnemy;

	[SerializeField]
	private List<Renderer> refRendererList;

	private int timer = 0;

	private MeshColorManager meshColorManager;

	/// <summary>
	/// 点滅させるために必要な残りヒットポイントのパーセンテージ.
	/// </summary>
	private const int CanUpdatePercent = 10;

	/// <summary>
	/// 通常の色.
	/// </summary>
	private readonly Color NormalColor = Color.white;

	/// <summary>
	/// 瀕死の色.
	/// </summary>
	private readonly Color DyingColor = Color.red;

	[ContextMenu( "All Select Renderer" )]
	void AllSelect()
	{
		refRendererList.Clear();
		refEnemy.Trans.AllVisit( (t) =>
		                        {
			if( t.renderer != null )
			{
				refRendererList.Add( t.renderer );
			}
		});
	}

	public override void Start ()
	{
		this.meshColorManager = new MeshColorManager();
		this.meshColorManager.Initialize( this.refRendererList );
	}
	public override void LateUpdate ()
	{
		if( !IsUpdate )	return;

		if( timer % 60 < 2 )
		{
			this.meshColorManager.SetColor( DyingColor );
		}
		else if( timer % 60 == 2 )
		{
			this.meshColorManager.SetColor( NormalColor );
		}

		timer++;
	}

	private bool IsUpdate
	{
		get
		{
			return ((refEnemy.Hp / refEnemy.MaxHp) * 100.0f) <= CanUpdatePercent;
		}
	}

}
