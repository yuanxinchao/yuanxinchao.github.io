using UnityEngine;

public class CaptureTool
{
	
	public static Texture2D shareTexture = null;
	public static string shareFileName;
	public static bool isShareFlag = false;
	
	public static string ShareTexture (tk2dCamera camera)
	{
		if (shareTexture != null) {
			shareTexture = null;
			if (System.IO.File.Exists (shareFileName)) {
				System.IO.File.Delete (shareFileName);
			}
		}
		int heightSize = 400;
		string fileName = CaptureCamera (camera.camera, new Rect (0, 0, (int)((Screen.width / (float)Screen.height) * heightSize), heightSize));
		return fileName;
	}
	
	static string CaptureCamera (Camera camera, Rect rect)
	{  
		// 创建一个RenderTexture对象  
		RenderTexture rt = new RenderTexture ((int)rect.width, (int)rect.height, 0);  
		// 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
		camera.targetTexture = rt;  
		camera.Render ();
		//ps: --- 如果这样加上第二个相机，可以实现只截图某几个指定的相机一起看到的图像。  
//		camera2.targetTexture = rt;  
//		camera2.Render ();
//		camera3.targetTexture = rt;  
//		camera3.Render ();  
		//ps: -------------------------------------------------------------------  
		
		// 激活这个rt, 并从中中读取像素。  
		RenderTexture.active = rt;  
		Texture2D screenShot = new Texture2D ((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);  
		screenShot.ReadPixels (rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
		screenShot.Apply (); 
		
		// 重置相关参数，以使用camera继续在屏幕上显示  
		camera.targetTexture = null;  
//		camera2.targetTexture = null;  
//		camera3.targetTexture = null;  
		//ps: camera2.targetTexture = null;  
		RenderTexture.active = null; // JC: added to avoid errors  
		GameObject.Destroy (rt);  
		// 最后将这些纹理数据，成一个png图片文件  
		byte[] bytes = screenShot.EncodeToJPG ();
		
		shareFileName = string.Format ("{0}/Share{1}.jpg", Application.persistentDataPath, "9527");
		
		Debug.Log ("shareFileName:" + shareFileName);
		
		System.IO.File.WriteAllBytes (shareFileName, bytes);  
		//		Debug.Log(string.Format("截屏了一张照片: {0}", filename));  
		shareTexture = screenShot;
		return shareFileName;
	}
	
	public static void ClearShareCapture ()
	{
		shareTexture = null;
		if (System.IO.File.Exists (shareFileName)) {
			System.IO.File.Delete (shareFileName);
		}
	}
}
