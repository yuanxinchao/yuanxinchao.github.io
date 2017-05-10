## WWW
直接上代码吧  
取文字  

	 IEnumerator shishi ()
    {
        WWW text = new WWW("https://yuanxinchao.github.io/technology/test/test.md");
        yield return text;
        Debug.Log("-----------" + text.text);
    }
取图片  

    IEnumerator GetAdsRes (string icon , string bgImage , string title , string content)
    {
        WWW www = new WWW(icon);
        yield return www;
        NativeAds.iconSprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        www = new WWW(bgImage);
        yield return www;
        NativeAds.bgPicSprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        NativeAds.title = title;
        NativeAds.content = content;
    }
