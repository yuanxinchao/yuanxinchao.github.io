using UnityEngine;
using System.Collections;
public class YXC_analyze : MonoBehaviour
{
	public static void AnalyzeInit (string umeng_key, string channal_id)
	{
		//*************初始化友盟统计************//
		Umeng.GA.StartWithAppKeyAndChannelId (umeng_key, channal_id);

		//*************其他统计的初始化************//

	}
	public static void Events (string event_name)
	{

		Umeng.GA.Event (event_name);
	}

}
