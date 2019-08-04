namespace OpenCvSharp.Demo
{
	using UnityEngine;
	using OpenCvSharp;
	using UnityEngine.UI;
	using UnityEngine.Video;

	public class ChromaKeying : MonoBehaviour
	{	
		public RawImage Video_1;
		public Texture2D VideoTexture_1;
		//public Texture2D VideoTexture_2;
		public void Start()
		{
			//VideoTexture_1 = Video_1.GetTexture("_MainTex") as Texture2D;
		}

		void Update()
		{
			ChromaKeyByFrame();
		}
		private void ChromaKeyByFrame()
		{	
			ReadPixelsFromMatTexture();
	    
			Mat mat = Unity.VidTextureToMat (VideoTexture_1);
			Mat grayMat = new Mat ();
			
			//Cv2.CvtColor (mat, grayMat, ColorConversionCodes.BGR2GRAY); 
			//Cv2.InRange (lower_green, upper_green)

			var lower_green = new OpenCvSharp.Scalar(0, 0, 100);
			var upper_green = new OpenCvSharp.Scalar(120, 100, 255);

			var mask = new Mat();

			Cv2.InRange(mat,lower_green, upper_green, mask);

			Mat masked_image = mat;
			
			

			Texture2D texture = Unity.MatToTexture (mask);

			RawImage rawImage = gameObject.GetComponent<RawImage> ();
			rawImage.texture = texture;
		}

		private void ReadPixelsFromMatTexture()
		{
			Texture mainTexture = Video_1.texture;
            Texture2D texture2D = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
             
            RenderTexture currentRT = RenderTexture.active;
            
            RenderTexture renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 32);
            Graphics.Blit(mainTexture, renderTexture);
 
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new UnityEngine.Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();

			VideoTexture_1 = texture2D;
            //Color[] pixels = texture2D.GetPixels();
 
            RenderTexture.active = currentRT;
		}

	}
}