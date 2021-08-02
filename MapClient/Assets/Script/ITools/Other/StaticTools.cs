
using System;
using System.Text;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using System.IO;

public class StaticTools
{
	static StringBuilder sb = new StringBuilder(255);


	internal static string Un64String(string value)
	{
		if (value == null || value == "")
		{
			return "";
		}
		try
		{
			byte[] bytes = System.Convert.FromBase64String(value);
			return Encoding.UTF8.GetString(bytes);
		}
		catch
		{
			return value;
		}
	}

	/// <summary>
	/// 改变父物体下的所有特效及动画播放速度
	/// </summary>
	/// <param name="tr">父物体</param>
	/// <param name="newSpeed">新的倍速</param>

	public static void ChangeEffSpeed(Transform transform, float newSpeed)
	{

		if (transform.GetComponent<Animation>() != null)
		{
			Animation animation = transform.GetComponent<Animation>();
			foreach (AnimationState an in animation)
			{
				an.speed = newSpeed;
			}
		}
		Animator ani = transform.GetComponent<Animator>();
		if (ani != null)
		{
			ani.speed = newSpeed;
		}
		if (transform.GetComponent<ParticleSystem>() != null)
		{
			ParticleSystem.MainModule psMain = transform.GetComponent<ParticleSystem>().main;
			psMain.simulationSpeed = newSpeed;
		}
		for (int i = 0; i < transform.childCount; i++)
		{
			ChangeEffSpeed(transform.GetChild(i), newSpeed);
		}
	}



	public static void DoTweenX(GObject obj, GTweenCallback1 cbv, int start, int target, int speed)
	{
		obj.x = start;
		GTweener tweener = obj.TweenMoveX(target, speed);
		tweener.OnComplete(cbv);
		tweener.SetEase(EaseType.Linear);
	}

	public static void SetShaderLod(int t)
	{
		Shader.globalMaximumLOD = t;
	}
	public static void SetQuanlity(int t)
	{
		QualitySettings.SetQualityLevel(t);
	}
	internal static StringBuilder ToString(string str)
	{
		sb.Append(str);
		return sb;
	}
	internal static string CombStr(string str1, string str2)
	{
		ClearStr();
		ToString(str1);
		ToString(str2);
		return ToEnd();
	}
	internal static string CombStr(string str1, string str2, string str3)
	{
		ClearStr();
		ToString(str1);
		ToString(str2);
		ToString(str3);
		return ToEnd();
	}
	internal static string CombStr(string str1, string str2, string str3, string str4)
	{
		ClearStr();
		ToString(str1);
		ToString(str2);
		ToString(str3);
		ToString(str4);
		return ToEnd();
	}
	internal static string ToEnd()
	{
		return sb.ToString();
	}

	internal static StringBuilder ClearStr()
	{
		sb.Length = 0;
		return sb;
	}

	
	internal static float Parse(string v)
	{
		//1.05
		int index = v.IndexOf('.');
		if (index < 0)
		{
			return int.Parse(v);
		}
		int max = v.Length;
		int cap = index + 3;
		string sb = "";
		int i = 0;
		int _count = 0;
		while (max > i)
		{
			var tmp = v[i];
			if (i != index)
			{
				sb = sb + (tmp);
				_count++;
			}
			i++;
		}
		while (_count < cap)
		{
			sb = sb + ('0');
			_count++;
		}
		return int.Parse(sb) * 0.001f;
	}
	static System.Diagnostics.Stopwatch stopwatch;
	public static void LogEnd(string str)
	{
		//  开始监视代码运行时间
		//  you code ....
		stopwatch.Stop(); //  停止监视
		TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间                
		double seconds = timespan.TotalSeconds;  //  总秒数        
		UnityEngine.Debug.LogError(str + seconds);
	}
	public static void LogStart()
	{
		stopwatch = new System.Diagnostics.Stopwatch();
		stopwatch.Start();
	}
	public static void ScreenChange(int size)
	{
		var b = size / 100f;
		int width = (int)(Screen.currentResolution.width * b);
		int height = (int)(Screen.currentResolution.height * b);
		Screen.SetResolution(width, height, true);
		Debug.LogError(width + "," + height + "=>" + b);
	}
    
    


	static GameObject Reporter;
	public static void LoadReporter()
	{
		if (Reporter == null)
		{
			Reporter = Object.Instantiate(Resources.Load("Reporter")) as GameObject;
			Object.DontDestroyOnLoad(Reporter);
		}
	}
}
