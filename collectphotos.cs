using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class takePhotos : MonoBehaviour
{
    private bool executing = false;
    private string path = @"C:\Users\Jordan\Documents\envawareproject\dataset\dataset.csv";
    public string environment; 
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(256, 256, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            executing = true;
        }
        if (executing)
        {
            StartCoroutine(ExecuteAfterTime(1f));
            executing = false;
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        Debug.Log("Executing!");
        for (int i = 0; i < 72; i++)
        {
            yield return new WaitForSeconds(time);
            gameObject.transform.Rotate(0f, 5f, 0f, Space.Self);
            StartCoroutine(FinishFirst(5.0f));

        }

        for (int i = 0; i < 3; i++)
        {
            gameObject.transform.Rotate(-15f, 0f, 0f, Space.Self);
            for (int b = 0; b < 72; b++)
            {
                yield return new WaitForSeconds(time);
                gameObject.transform.Rotate(0f, 5f, 0f, Space.Self);
                StartCoroutine(FinishFirst(5.0f));
            }
        }
    }

    IEnumerator FinishFirst(float waitTime)
    {
        using (StreamWriter sw = File.AppendText(path))
        {
            int epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            string filename = @"C:\Users\Jordan\Documents\envawareproject\dataset\images\" + environment + @"\" + epoch + ".png";
            ScreenCapture.CaptureScreenshot(@"C:\Users\Jordan\Documents\envawareproject\dataset\images\" + environment + @"\" + epoch + ".png");
            sw.WriteLine(filename + "," + environment);
        }
        yield return null;
    }


}
