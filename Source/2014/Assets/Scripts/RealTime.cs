using System;
using UnityEngine;

public class RealTime : MonoBehaviour
{
	public static float time
	{
		get
		{
			if (RealTime.mInst == null)
			{
				RealTime.Spawn();
			}
			return RealTime.mInst.mRealTime;
		}
	}

	public static float deltaTime
	{
		get
		{
			if (RealTime.mInst == null)
			{
				RealTime.Spawn();
			}
			return RealTime.mInst.mRealDelta;
		}
	}

	private static void Spawn()
	{
		GameObject gameObject = new GameObject("_RealTime");
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		RealTime.mInst = gameObject.AddComponent<RealTime>();
		RealTime.mInst.mRealTime = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		this.mRealDelta = Mathf.Clamp01(realtimeSinceStartup - this.mRealTime);
		this.mRealTime = realtimeSinceStartup;
	}

	private static RealTime mInst;

	private float mRealTime;

	private float mRealDelta;
}
