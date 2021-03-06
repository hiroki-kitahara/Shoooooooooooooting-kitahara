﻿/*===========================================================================*/
/*
*     * FileName    :PlayerChaseOnSpeed.cs
*
*     * Description : .
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// .
/// </summary>
public class PlayerChaseOnSpeed : GameMonoBehaviour
{
	public Vector3 fixedPosition;
	
	[SerializeField]
	private float speed = 0.0f;

	public override void LateUpdate()
	{
		base.LateUpdate();

		if( PauseManager.Instance.IsPause )	return;
		
		var targetPos = ReferenceManager.Player.cachedTransform.position + fixedPosition;
		
		if( speed <= 1.0f )
		{
			Trans.position = targetPos;
		}
		else
		{
			var velocity = (targetPos - cachedTransform.position);
			if( velocity.sqrMagnitude <= speed )
			{
				Trans.position = targetPos;
			}
			else
			{
				Trans.position += velocity.normalized * speed;
			}
		}
	}
}
