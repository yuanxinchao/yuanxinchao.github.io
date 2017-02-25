## 用PlayerPrefs存bool型
😂😂😂😂

	PlayerPrefs.SetInt("Name", Convert.ToInt32(yourBool));
	yourBool = Convert.ToBoolean(PlayerPrefs.GetInt("Name"));
或者
	
	PlayerPrefs.SetInt("Name", (yourBool ? 1 : 0));
	yourBool = (PlayerPrefs.GetInt("Name") != 0);