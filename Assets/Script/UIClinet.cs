using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIClinet : MonoBehaviour {
    public delegate void initializationUItext();
    public  event initializationUItext UpdateUIeventtext;
    public delegate void initializationUImage();
    public  event initializationUItext UpdateUIeventImage;

    Queue queueLeft;
    [System.Serializable]
    public struct info{


        public int DefaultID;
        public int CurrentID;

        public int DefaultSubtitleNum;
        public int CurrentSubtitleNum;

        public string DefaultTitle;
        public string CurrentTitle;

        public Text BigTitle;
        public Image BigTitleImage;
        public Text BigTitleDiscription;

        public Text SubTitle;
        public Text MainContent;

        public Image[] BottomLeftImage;
        public Image[] BottomMidImage;
        public Image[] BottomRightImage;

        public Transform subImageGroup;
        public RectTransform[] TopImagePos;
        public RectTransform[] TopimageSlot;
    }

    public info myinfo;
	// Use this for initialization
	void Start () {

       
    }

    public void ResetProgram() {
        myinfo.CurrentTitle = myinfo.DefaultTitle;
        myinfo.CurrentID = myinfo.DefaultID;
        myinfo.CurrentSubtitleNum = myinfo.DefaultSubtitleNum;
        StartCoroutine(SubScribeUpdateUIevent());
    }

    private void OnEnable()
    {
        ResetProgram();

    }

    private void OnDisable()
    {
        UpdateUIeventtext -= SetupText;
    }


    IEnumerator SubScribeUpdateUIevent()
    {
        yield return new WaitForSeconds(.1f);
        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateUIeventtext += SetupText;
        UpdateUItext();
        UpdateUIeventImage += SetupImage;
        UpdateUIimage();
    }

    void SetupText() {

        myinfo.BigTitle.text = myinfo.CurrentTitle;//setup default BigTitle
        myinfo.SubTitle.text = ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle[myinfo.CurrentSubtitleNum];//setup subtitle
        myinfo.MainContent.text = ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle_MainContentdictionary[myinfo.SubTitle.text];//setup main content
     //   Debug.Log("bigtitle" + myinfo.BigTitle.text);
    }

    void SetupImage() {
       // Debug.Log("ID"+myinfo.CurrentID);
      //  Debug.Log("subtitle" + myinfo.SubTitle.text);
        Sprite temp = ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle_SubImagedictionary[myinfo.SubTitle.text];
        myinfo.BigTitleImage.sprite = temp;
        SetupTopBarImage();
    }
    public void SetupTopBarImage()//keepworking
    {

            int index = MidNumber(myinfo.TopImagePos.Length);

        int resourceImgeSize = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage.Length;
            myinfo.TopImagePos[index].GetComponent<Image>().sprite = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage[myinfo.CurrentSubtitleNum];
        myinfo.TopImagePos[index-1].GetComponent<Image>().sprite = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage[LoopNumber("right", resourceImgeSize, myinfo.CurrentSubtitleNum)];
        myinfo.TopImagePos[index+1].GetComponent<Image>().sprite = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage[LoopNumber("left", resourceImgeSize, myinfo.CurrentSubtitleNum)];

    }

    int MidNumber(int arrayLenth) {
        return Mathf.FloorToInt(arrayLenth / 2);
    }

    public void UpdateUItext()
    {
        if (UpdateUIeventtext != null)
        {
            UpdateUIeventtext();
        }

    }

    public void UpdateUIimage()
    {
        if (UpdateUIeventImage != null)
        {
            UpdateUIeventImage();
        }

    }
    #region TopImageMove
    public void MoveLeft(string left) {
        for (int i = 1; i < myinfo.TopImagePos.Length; i++)
        {
            MoveTopImage(i, myinfo.TopimageSlot[i - 1].localPosition, myinfo.TopimageSlot[i-1].localScale);

        }
        MoveTopImage(0, myinfo.TopimageSlot[4].localPosition, myinfo.TopimageSlot[4].localScale);
        myinfo.TopImagePos = updateArray(left);
        UpdateSubtitle(left);
    }

    public void MoveRight(string Right) {
        for (int i =0; i < myinfo.TopImagePos.Length-1; i++)
        {
                MoveTopImage(i, myinfo.TopimageSlot[i+1].localPosition, myinfo.TopimageSlot[i+1].localScale);

        }
        MoveTopImage(myinfo.TopImagePos.Length - 1, myinfo.TopimageSlot[0].localPosition, myinfo.TopimageSlot[0].localScale);
        myinfo.TopImagePos = updateArray(Right);
        UpdateSubtitle(Right);
    }

    void MoveTopImage(int i, Vector3 target, Vector3 scale) {
        LeanTween.moveLocal(myinfo.TopImagePos[i].gameObject, target, .2f);
        LeanTween.scale(myinfo.TopImagePos[i].gameObject, scale, .2f);
    }

    RectTransform[] updateArray(string RightOrLeft) {
        RectTransform[] temp = new RectTransform[myinfo.TopImagePos.Length];
        if (RightOrLeft == "left") {
            for (int i = 0; i < temp.Length-1; i++)
            {
                temp[i] = myinfo.TopImagePos[i + 1];
            }
            temp[temp.Length - 1] = myinfo.TopImagePos[0];
        }else if(RightOrLeft == "right") {
            for (int i = 0; i < temp.Length-1 ; i++)
            {
                temp[i+1] = myinfo.TopImagePos[i];
            }
            temp[0] = myinfo.TopImagePos[temp.Length - 1];
        }
        return temp;
    }


    /*
        void imageInAndOut(string leftAndRight)
        {
            if (leftAndRight == "left") {

            }
        }
        */
    int LoopNumber(string LeftRight,int size, int currentNum) {

        if (LeftRight == "left")
        {
            if (currentNum - 1 < 0)
            {
             //   Debug.Log("current number" + (size - 1));
                return currentNum = size - 1;
            }
            else
            {
             //   Debug.Log("current number" + (currentNum - 1));
                return currentNum-1;
            }
        }
        else if (LeftRight == "right") {
            if (currentNum + 1 < size)
            {
              //  Debug.Log("current number" + (0));
                return currentNum + 1;
            }
            else {
           //     Debug.Log("current number" + (currentNum + 1));
                return 0;
            }
        }
        return currentNum;
    
    }

    void UpdateSubtitle(string leftRight) {
            myinfo.CurrentSubtitleNum = LoopNumber(leftRight, ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage.Length, myinfo.CurrentSubtitleNum);
       // Debug.Log("I ma here the important num"+myinfo.CurrentSubtitleNum);
            UpdateUI();
        }

    #endregion
    // Update is called once per frame
    void Update () {
		
	}


}
