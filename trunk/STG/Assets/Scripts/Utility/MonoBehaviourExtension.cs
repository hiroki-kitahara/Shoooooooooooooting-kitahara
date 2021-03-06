/*===========================================================================*/
/*
*     * FileName    : MonoBehaviourExtension.cs
*
*     * Description : .
*
*     * Author      : Hiroki_Kitahara.
*/
/*===========================================================================*/
using UnityEngine;
using System.Collections;


public class MonoBehaviourExtension : MonoBehaviour
{
	
	/// <summary>
	/// transform高速アクセスプロパティ.
	/// </summary>
	/// <value>
	/// The trans.
	/// </value>
	public Transform Trans
	{
		get
		{
			if( cachedTransform == null ){ cachedTransform = transform; }
			return cachedTransform;
		}
	}
	[HideInInspector]
	public Transform cachedTransform = null;
	
	/// <summary>
	/// gameObject高速アクセスプロパティ.
	/// </summary>
	/// <value>
	/// The G.
	/// </value>
	public GameObject GO
	{
		get
		{
			if( go == null ){ go = gameObject; }
			return go;
		}
	}
	private GameObject go = null;
	
	public virtual void Awake()
	{
		cachedTransform = transform;
	}
	public virtual void Start()
	{
	}
	public virtual void Update()	
	{
		
	}
	public virtual void LateUpdate()
	{
	
	}
}
