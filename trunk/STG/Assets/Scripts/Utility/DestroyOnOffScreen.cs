﻿/*===========================================================================*/
/*
*     * FileName    :DestroyOnOffScreen.cs
*
*     * Description : 画面外へ移動したら死亡するコンポーネント.
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 画面外へ移動したら死亡するコンポーネント.
/// </summary>
public class DestroyOnOffScreen : GameMonoBehaviour
{
	[SerializeField]
	private Rect bounds;

	private readonly Rect range = new Rect( -400.0f, 400.0f, 400.0f, -400.0f );

	void OnDrawGizmos()
	{
		var pos = new Rect(
			transform.localPosition.x + bounds.x,
			transform.localPosition.y + bounds.y,
			transform.localPosition.x + bounds.width,
			transform.localPosition.y + bounds.height
			);

		// 左.
		Gizmos.DrawLine( new Vector3( pos.x, pos.y, 0.0f ), new Vector3( pos.x, pos.height, 0.0f ) );
		// 上.
		Gizmos.DrawLine( new Vector3( pos.x, pos.y, 0.0f ), new Vector3( pos.width, pos.y, 0.0f ) );
		// 右.
		Gizmos.DrawLine( new Vector3( pos.width, pos.y, 0.0f ), new Vector3( pos.width, pos.height, 0.0f ) );
		// 下.
		Gizmos.DrawLine( new Vector3( pos.x, pos.height, 0.0f ), new Vector3( pos.width, pos.height, 0.0f ) );

		// 左.
		Gizmos.DrawLine( new Vector3( range.x, range.y, 0.0f ), new Vector3( range.x, range.height, 0.0f ) );
		// 上.
		Gizmos.DrawLine( new Vector3( range.x, range.y, 0.0f ), new Vector3( range.width, range.y, 0.0f ) );
		// 右.
		Gizmos.DrawLine( new Vector3( range.width, range.y, 0.0f ), new Vector3( range.width, range.height, 0.0f ) );
		// 下.
		Gizmos.DrawLine( new Vector3( range.x, range.height, 0.0f ), new Vector3( range.width, range.height, 0.0f ) );
	}

	public override void Update()
	{
		base.Update();

		var fixedBounds = new Rect(
			Trans.localPosition.x + bounds.x,
			Trans.localPosition.y + bounds.y,
			Trans.localPosition.x + bounds.width,
			Trans.localPosition.y + bounds.height
			);

		if( range.y < fixedBounds.height || range.height > fixedBounds.y ||
		    range.x > fixedBounds.width || range.width < fixedBounds.x )
		{
			Destroy( gameObject );
		}
	}
}