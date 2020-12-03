using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public void TakeScreenshot()
    {
        ScreenshotHandler.TakeScreenshot_Static(3510, 1620);
    }
}
