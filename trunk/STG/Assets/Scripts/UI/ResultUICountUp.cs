﻿/*===========================================================================*/
/*
*     * FileName    : ResultUICountUp.cs
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
public class ResultUICountUp : ResultUIEffectExecuter
{
	[SerializeField]
	protected TextMesh refTextMesh;

	[SerializeField]
	private int starId;

	[SerializeField]
	protected int rate;

	[SerializeField]
	protected GameObject refResultManager;

	protected int currentCount = 0;

	protected int targetCount = 0;

	protected bool isUpdate = false;

	public override void Update()
	{
		if( !isUpdate )
		{
			return;
		}

		CountUp();
	}

	protected override void Action ()
	{
		isUpdate = true;
		targetCount = ReferenceManager.ScoreManager.EarnedStarItemList[starId];
	}

	protected void CountUp()
	{
		if( currentCount >= targetCount )
		{
			isUpdate = false;
			this.currentCount = 0;
			refResultManager.SendMessage( ResultUIManager.CompleteMessage );
			return;
		}
		if( Input.GetKeyDown( KeyCode.Z ) )
		{
			var sub = targetCount - currentCount;
			currentCount = targetCount;
			CountUpped( sub * rate );
		}
		else
		{
			currentCount++;
			CountUpped( rate );
		}
		refTextMesh.text = (currentCount * rate).ToString();
	}

	protected virtual void CountUpped( int addedValue )
	{
	}
}