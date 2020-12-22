using UnityEngine;
using UnityEngine.Rendering;

public class ScreenshotHandler : MonoBehaviour 
{

    private static ScreenshotHandler instance;

    private Camera _camera;
    private bool _takeScreenshotOnNextFrame;

    void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }
    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }
    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }

    private void Awake() 
    {
        instance = this;
        _camera = gameObject.GetComponent<Camera>();
        Debug.Log("screnshot camera");
    }

    private void OnPostRender() 
    {        
        if (_takeScreenshotOnNextFrame) 
        {
            _takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = _camera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
            Debug.Log("Saved CameraScreenshot.png");

            RenderTexture.ReleaseTemporary(renderTexture);
            _camera.targetTexture = null;

        }
    }

    private void TakeScreenshot(int width, int height) 
    {
        _camera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        _takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height) 
    {
        instance.TakeScreenshot(width, height);
    }
}