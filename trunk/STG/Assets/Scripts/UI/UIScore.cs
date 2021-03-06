/*===========================================================================*/
/*
*     * FileName    : UIScore.cs
*
*     * Description : .
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UIScore : UIScoreBase
{
	protected override string AssetKey
	{
		get
		{
			return "ScoreUIFormat";
		}
	}

	protected override ulong DrawValue
	{
		get
		{
			return ReferenceManager.ScoreManager.Score;
		}
	}
}
