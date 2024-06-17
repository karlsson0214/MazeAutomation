using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCamera : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("pressed s");
            SaveCameraView(cam);

        }
    }

    void SaveCameraView(Camera cam)
    {
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        cam.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;
        
        for (int x = 0; x < Screen.width; x = x + 10)
        {
            Debug.Log(renderedTexture.GetPixel(x, x));
        }
        

        byte[] byteArray = renderedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/cameracapture.png", byteArray);
        Debug.Log(Application.dataPath + "/cameracapture.png");
    }
}
