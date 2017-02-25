## 批量为UGUI 的Button 添加事件

        UnityAction[] shuzu = new UnityAction[]
        {
            () =>
            {
                ApplovinAds.GetInstance().ShowVideo();
            },
            () =>
            {
                Debug.Log(ApplovinAds.GetInstance().IsIncentivizedAvailable());
            },
            () =>
            {
                VungleAds.GetInstance().ShowVideo();
            },
            () =>
            {
                Debug.Log(VungleAds.GetInstance().IsIncentivizedAvailable());
            },
           
           
        };


        GameObject canvas = GameObject.Find("Canvas");
        Button[] b = canvas.GetComponentsInChildren<Button>();
        for (int i = 0; i < b.Length; i++)
        {
            b [i].onClick.AddListener(shuzu [i]);
        }


## GUILayout
这里直接可以在Button里放需要调试的方法，很方便
结果可以用`console.Getinstance().print();`打印出来，很方便。**console**放在同路径文件里

    void OnGUI ()
    {

         
        GUILayout.BeginArea(Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical("box");
        if (GUILayout.Button("IsInterstitialAvailable", GUILayout.Width(Screen.width / 3), GUILayout.Height(Screen.height / 8)))
        {
			
            IsInterstitialAvailable();
			
        }
        if (GUILayout.Button("GetInterstitial", GUILayout.Width(Screen.width / 3), GUILayout.Height(Screen.height / 8)))
        {
			
            GdtAds.GetInstance().GetInterstitial();
        }
        if (GUILayout.Button("ShowInterVideo", GUILayout.Width(Screen.width / 3), GUILayout.Height(Screen.height / 8)))
        {
            ShowInterVideo();
			
        }
        if (GUILayout.Button("I'm the top button", GUILayout.Width(Screen.width / 3), GUILayout.Height(Screen.height / 8)))
        {
			
			
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }