﻿/*===========================================================================*/
/*
*     * FileName    : ExtraModeConditioner.cs
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Extraモード突入条件.
/// </summary>
public class ExtraModeConditioner : MonoBehaviour
{
	void CatchCondition( ConditionHolder holder )
	{
		holder.IsSuccess = CanPlay;
	}
	private bool CanPlay
	{
		get
		{
			return SaveData.Progresses.Instance.IsClearGame( GameDefine.DifficultyType.Hard );
		}
	}
}
