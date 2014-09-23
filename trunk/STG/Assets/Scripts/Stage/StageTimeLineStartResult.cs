﻿/*===========================================================================*/
/*
*     * FileName    : StageTimeLineStartResult.cs
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
public class StageTimeLineStartResult : A_StageTimeLineActionable
{
	public override void Action ()
	{
		ReferenceManager.refUILayer.BroadcastMessage( GameDefine.StartResultMessage );
		ReferenceManager.refPlayerLayer.BroadcastMessage( GameDefine.StartResultMessage );
		Destroy( gameObject );
	}

	protected override string GameObjectName
	{
		get
		{
			return string.Format( "[{0}] StartResult", timeLine );
		}
	}
}