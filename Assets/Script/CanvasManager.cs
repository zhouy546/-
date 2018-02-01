using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour {
    #region Debug
    public RectTransform DebugCanvas;
    public Toggle FrameRateToggle;
    public Text FrameRateText;
    IEnumerator FrameRateCoroutine;

    public Text SetReslitionWidth;
    public Text SetReslitionHeight;
    #endregion

    [SerializeField]
    UIClinet[] SectionUIClinet;

	// Use this for initialization
	void Start () {
        Screen.SetResolution(2560, 1080, false, 60);
        Application.targetFrameRate = 120;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            DebugCanvas.gameObject.SetActive(true);
        }
	}


    void SetupCanvas()
    {

    }


    public void showFrameRate() {
        if (FrameRateToggle.isOn)
        {
            StartCoroutine(UpdateUpdateFrameRate(1f));
            FrameRateText.gameObject.SetActive(FrameRateToggle.isOn);
        }
        else {
            StopCoroutine(UpdateUpdateFrameRate(1f));
            FrameRateText.gameObject.SetActive(FrameRateToggle.isOn);
        }
    }


    public void setResolution() {
        int width = int.Parse( SetReslitionWidth.text);
        int height = int.Parse(SetReslitionHeight.text);
        Screen.SetResolution(width, height, false,120);
        Debug.Log("set res");
    }

    public IEnumerator UpdateUpdateFrameRate(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            FrameRateText.text =((int)(1f / Time.deltaTime)).ToString()+"FPS";
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ResetApplication()
    {
        for (int i = 0; i < SectionUIClinet.Length; i++)
        {
            SectionUIClinet[i].ResetProgram();
        }
    }

    public void CloseDebugConsole() {
        DebugCanvas.gameObject.SetActive(false);
    }
}
