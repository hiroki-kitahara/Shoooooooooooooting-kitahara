﻿//================================================
/*!
    @file   StageTimeLineManager.cs

    @brief  ステージのタイムライン管理者クラス.

    @author CyberConnect2 Co.,Ltd.  Hiroki_Kitahara.
*/
//================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StageTimeLineManager
{
	/// <summary>
	/// タイムライン.
	/// </summary>
	public int TimeLine
	{
		set
		{
			timeLine = value;
			timeLine = timeLine <= 0 ? 0 : timeLine;
		}
		get
		{
			return timeLine;
		}
	}
	[SerializeField]
	private int timeLine;
	
	/// <summary>
	/// 更新処理.
	/// </summary>
	public void Update()
	{
		if( PauseManager.Instance.IsPause )	return;
		
		timeLine++;
	}
}
