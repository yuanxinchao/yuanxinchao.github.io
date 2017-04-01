## Resources.Load
	using UnityEngine;
	using System.Collections;
	
	public class ResourceManager
	{
	    static Sprite[] sprites;
	
	    public static Sprite LoadSinglePic (string path)
	    {
	        return Resources.Load<Sprite>(path);
	    }
	
	    public static Sprite LoadMultiPic (string path , int index)
	    {
	        sprites = Resources.LoadAll<Sprite>(path);
	        return sprites [index];
	    }
	}
