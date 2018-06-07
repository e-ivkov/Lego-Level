using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamPhotoCamera : MonoBehaviour
{

    WebCamTexture webCamTexture;
    public string DeviceName;

    void Start()
    {
        webCamTexture = new WebCamTexture();
        if (!string.IsNullOrEmpty(DeviceName))
            webCamTexture.deviceName = DeviceName;
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
        foreach (var device in WebCamTexture.devices)
        {
            Debug.Log(device.name);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LevelBuilder levelBuilder = GameObject.Find("Main Camera").GetComponent<LevelBuilder>();
            Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
            photo.SetPixels(webCamTexture.GetPixels());
            photo.Apply();
            levelBuilder.LegoBlocks = photo;
            levelBuilder.BuildLevel();
        }
    }

    void TakePhoto()
    {

        // NOTE - you almost certainly have to do this here:

        //yield return new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        //File.WriteAllBytes(your_path + "photo.png", bytes);
    }
}
