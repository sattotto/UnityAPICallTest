using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WebCam : MonoBehaviour {

    public int Width = 1920;
    public int Height = 1080;
    public int FPS = 30;

    public WebCamTexture webcamTexture;
    public Color32[] color32;

	// Use this for initialization
	void Start () {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (var i = 0; i < devices.Length;i++){
            Debug.Log(devices[i].name);
        }
        webcamTexture = new WebCamTexture(devices[0].name, Width, Height, FPS);
        GetComponent<Renderer>().material.mainTexture = webcamTexture;
        webcamTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0){
            color32 = webcamTexture.GetPixels32();

            Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height);
            GameObject.Find("Quad").GetComponent<Renderer>().material.mainTexture = texture;

            texture.SetPixels32(color32);
            texture.Apply();

            var bytes = texture.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath + "camera.png", bytes);
        }
	}
}
