/*===========================================================================*/
/*
*     * FileName    : ObjectMoveTween.cs
*
*     * Description : .
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;


public class ObjectMoveTween : A_ObjectMove
{
	private Vector3 initialPosition;
	
	protected override void InitMove()
	{
		base.InitMove();
		initialPosition = refTrans.localPosition;
	}
	protected override void UpdateMove()
	{
		refTrans.localPosition = Vector3.Lerp( initialPosition, data.targetPosition, data.curve0.Evaluate( Duration ) );
		currentDuration++;
	}
	protected override void Finish ()
	{
		isComplete = true;
		enabled = false;
		if( data.isDestroy )
		{
			Destroy( refTrans.gameObject );
		}
	}
}
