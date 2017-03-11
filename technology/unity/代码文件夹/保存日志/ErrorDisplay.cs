using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class ErrorDisplay : MonoBehaviour
{
	private bool LogError = true;
	private bool LogCommon = false;
	private bool LogWrite = true;
	private Vector2 m_scroll;
	private string m_logs;
	private string FontSize;
	public static ErrorDisplay instance;
	private string filePath;
	private string outpath;

	void Awake ()
	{
		instance = this;
		FontSize = (Screen.width / 45).ToString ();
		LogError = GlobalData.isLogError;
		LogCommon = GlobalData.isLogCommon;
		filePath = Application.persistentDataPath + string.Format ("/ErrorLog_{0}.{1}", System.DateTime.Now.Month.ToString ("00"), System.DateTime.Now.Day.ToString ("00"));

		outpath = string.Empty;
		if (Application.loadedLevelName != "LoginScene") {
			int account = SymmetricMethod.EncryptID (KBEMgr.Instance.GetDBID ());
			outpath = filePath + string.Format ("/Error_Account{0}_{1}.txt", account, GlobalData.playerName);
		} else {
			outpath = filePath + string.Format ("/Error_{0}.txt", GlobalData.playerName);
		}
		
		if (!Directory.Exists (filePath)) {
			Directory.CreateDirectory (filePath);
		}
	}
	void Start ()
	{
		Application.RegisterLogCallback (HandleLog);
	}

	void Update ()
	{
		if (LogError) {
			if (Input.GetMouseButtonDown (0)) {
				if (Time.timeScale == 0) {
					Time.timeScale = 1;
				}
			} else if (Input.GetMouseButtonDown (1)) {
				m_logs = null;
			} else if (Input.GetMouseButtonDown (2)) {
				LogCommon = !LogCommon;
			}
		}
	}
	void OnGUI ()
	{
		if (LogError || LogCommon) {
			m_scroll = GUILayout.BeginScrollView (m_scroll);
			GUILayout.Label (m_logs);
			GUILayout.EndScrollView ();
		}
	}
	/// <summary>
	/// 输出错误信息
	/// </summary>
	/// <param name="logString">错误信息</param>
	/// <param name="stackTrace">跟踪堆栈</param>
	/// <param name="type">错误类型</param>
	void HandleLog (string logString, string stackTrace, LogType type)
	{
		#if UNITY_EDITOR
		return;
		#elif UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_WIN || UNITY_STANDALONE
		if(LogWrite){
			if(type == LogType.Error || type == LogType.Exception){
				WriteResult(logString,stackTrace);
			}
		}
		if(type == LogType.Error || type == LogType.Exception){
			Debuger.LogError ("Error");
			if(LogError){
				Debuger.LogError ("Error");
				m_logs += "<color=#FF0000><size="+FontSize+">Error : " + logString + "</size></color>"+"\n";
				m_logs += "<color=#FFFFCC><size="+FontSize+">Error Detaile : "+ stackTrace + "</size></color>"+"\n";
				Time.timeScale = 0;
			}
		}else if(type == LogType.Log){
			if(LogCommon){
				m_logs += "<color=#FFFFCC><size="+FontSize+">Log : " + logString + "</size></color>"+"\n";
				m_logs += "<color=#FFFFFF><size="+FontSize+">Log Detaile : "+ stackTrace + "</size></color>"+"\n";
			}
		}
		#endif
	}
	
	void WriteResult (string logString, string stackTrace)
	{
		List<string> mWriteTxt = new List<string> ();
		string TempStr = null;
		TempStr = "记录时间：" + System.DateTime.Now;
		mWriteTxt.Add (TempStr);
		TempStr = "错误信息：" + logString;
		mWriteTxt.Add (TempStr);
		TempStr = "堆栈信息：" + stackTrace;
		mWriteTxt.Add (TempStr);
		
		string[] temp = mWriteTxt.ToArray ();
		using (StreamWriter writer = new StreamWriter(outpath, true, Encoding.UTF8)) {
			writer.WriteLine ("");
		}
		foreach (string t in temp) {
			using (StreamWriter writer = new StreamWriter(outpath, true, Encoding.UTF8)) {
				writer.WriteLine (t);
			}
		}
	}
}