using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIClinet : MonoBehaviour {
    private IEnumerator coroutine;
    public delegate void initializationUItext();
    public  event initializationUItext UpdateUIeventtext;
    public delegate void initializationUImage();
    public  event initializationUItext UpdateUIeventImage;
    int[] tempint;

    private int FrontAndBackInt = -1;
    delegate void setupBottomImage(int i, Information item,bool isback, Image[] BackImageOrFrotImage);
   
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

        public Image[] BottomFrontImage;
        public Image[] BottomBackImage;

        public List<string> BottomTitleSelection;
        public string[] BackBottomTitleSelection;
        public string[] tempF;
        public Text[] BottomTitleText;
        public float ImageFadeTime;


        //public Dictionary<string,Image> BottomDisplay


        public RectTransform[] TopImagePos;
        public RectTransform[] TopimageSlot;
    }

    public info myinfo;
	// Use this for initialization
	void Start () {
        coroutine = UpdateBottomSection(5.0f);
        StartCoroutine(coroutine);
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

    IEnumerator SubScribeUpdateUIevent()
    {
        yield return new WaitForSeconds(.5f);
        UpdateUI();

    }
    #region event and delegate
    public void UpdateUI()
    {
        UpdateUIeventtext += SetupText;
        UpdateUItext();
        UpdateUIeventImage += SetupImage;
        UpdateUIimage();

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

    private void OnDisable()
    {
        UpdateUIeventtext -= SetupText;
        UpdateUIeventImage -= SetupImage;
    }

    #endregion

    #region setupGUI

    void SetupText() {

        SetupMidToptext();
        SetupBottomText();

     //   Debug.Log("bigtitle" + myinfo.BigTitle.text);
    }

    void SetupMidToptext()
    {
        myinfo.BigTitle.text = myinfo.CurrentTitle;//setup default BigTitle
        myinfo.SubTitle.text = ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle[myinfo.CurrentSubtitleNum];//setup subtitle
        myinfo.MainContent.text = ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle_MainContentdictionary[myinfo.SubTitle.text];//setup main content
    }

    public IEnumerator UpdateBottomSection(float waitTime) {
        while (true)
        {
            //Debug.Log("start corutine");
            yield return new WaitForSeconds(waitTime);
            SetupBottomText();
            SetupBottomBarImage();
        }
    } 

    void SetupBottomText() {
        myinfo.tempF = GetBottomText(myinfo.BottomTitleSelection);
        myinfo.BackBottomTitleSelection = GetBottomText(myinfo.BottomTitleSelection);

        for (int i = 0; i < myinfo.BottomTitleText.Length; i++)
        {
            myinfo.BottomTitleText[i].text = myinfo.tempF[i];
        }
    }

    string[] GetBottomText(List<string> _BottomTitleSelection) {
        string[] temp;
        _BottomTitleSelection.Clear();
        List<string> templist = new List<string>();
        for (int i = 0; i < ReadJson.instance.myinformationList.Count; i++)//add title to list 
        {
            _BottomTitleSelection.Add(ReadJson.instance.myinformationList[i].BigTitle);
            
        }

        //take out it's self
        _BottomTitleSelection.Remove(myinfo.CurrentTitle);
        tempint = getDifferentNumInRange(0, _BottomTitleSelection.Count-1, myinfo.BottomTitleText.Length);//random 3 different num 

        for (int i = 0; i < tempint.Length; i++)//set 3different title
        {
            templist.Add(_BottomTitleSelection[tempint[i]]);
        }

        temp = templist.ToArray();
        _BottomTitleSelection.Clear();
        templist.Clear();
        return temp;
    }

    void SetupImage() {
        setupMidTopImage();
        SetupBottomBarImage();
    }

    void setupMidTopImage() {
        Sprite temp = ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle_SubImagedictionary[myinfo.SubTitle.text];
        myinfo.BigTitleImage.sprite = temp;
        SetupTopBarImage();
    }

    void SetupBottomBarImage()
    {
        FrontAndBackInt = NumPositiveAndNegitive(FrontAndBackInt);
        if (FrontAndBackInt > 0)
        {
            SetupBottomBarImage(setupImage, true, false, myinfo.BottomFrontImage);
            SetupBottomBarImage(setupImage, false, true, myinfo.BottomBackImage);
            //show FrontImage            //front fade in 
            FadeAndshowImage(myinfo.BottomFrontImage, 0f, 1f, myinfo.ImageFadeTime);
            //back fade out
            FadeAndshowImage(myinfo.BottomBackImage, 1f, 0f, myinfo.ImageFadeTime);
        }
        else
        {
            //front fade out 
            SetupBottomBarImage(setupImage, true, true, myinfo.BottomFrontImage);
            SetupBottomBarImage(setupImage, false, false, myinfo.BottomBackImage);
            //show FrontImage                       //front fade out 
            FadeAndshowImage(myinfo.BottomFrontImage, 1f, 0f, myinfo.ImageFadeTime);
            //back fade in
            FadeAndshowImage(myinfo.BottomBackImage, 0f, 1f, myinfo.ImageFadeTime);
        }


    }

    void FadeAndshowImage(Image[] image,float from, float to, float time)
    {
        LeanTween.value(from, to, time).setOnUpdate((float value) =>
        {
            for (int i = 0; i < image.Length; i++)
            {
                image[i].color = new Color(1f, 1f, 1f, value);
            }
        });

    }
    int NumPositiveAndNegitive(int x)
    {
        return -x;
    }


    public void SetupTopBarImage()//keepworking
    {
            int index = MidNumber(myinfo.TopImagePos.Length);

        int resourceImgeSize = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage.Length;
            myinfo.TopImagePos[index].GetComponent<Image>().sprite = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage[myinfo.CurrentSubtitleNum];
        myinfo.TopImagePos[index-1].GetComponent<Image>().sprite = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage[LoopNumber("right", resourceImgeSize, myinfo.CurrentSubtitleNum)];
        myinfo.TopImagePos[index+1].GetComponent<Image>().sprite = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage[LoopNumber("left", resourceImgeSize, myinfo.CurrentSubtitleNum)];

    }

     void SetupBottomBarImage(setupBottomImage mydelegate,bool isSetupFront, bool isback, Image[] BackImageOrFrotImage) {
        List<Image[]> list = new List<Image[]>();
       // Debug.Log(myinfo.BottomTitleText[0].text);
        foreach (var item in ReadJson.instance.myinformationList)
        {
            for (int i = 0; i < myinfo.BottomFrontImage.Length; i++)
            {
                if (isSetupFront)
                {
                        mydelegate(i, item, isback, BackImageOrFrotImage);
                }
                else {
                        mydelegate(i, item, isback, BackImageOrFrotImage);
                }
            } 
        }
    }

    void setupImage(int i, Information item, bool isback , Image[] BackImageOrFrotImage) {
        if (isback)
        {
            if (item.BigTitle == myinfo.BackBottomTitleSelection[i])
            {
                BackImageOrFrotImage[i].sprite = item.Bigtitle_BackgroundImagedictionary[myinfo.BackBottomTitleSelection[i]];
            }
        }
        else {
            if (item.BigTitle == myinfo.BottomTitleText[i].text)
            {
                BackImageOrFrotImage[i].sprite = item.Bigtitle_BackgroundImagedictionary[myinfo.tempF[i]];
            }
        }
       
    }


    #endregion

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
        SetupMidToptext();
        setupMidTopImage();
        }

    #endregion

    #region general function or math function
    int MidNumber(int arrayLenth)
    {
        return Mathf.FloorToInt(arrayLenth / 2);
    }


    int[] getDifferentNumInRange(int min, int max, int index)
    {
        List<int> temp = new List<int>();
        if (index - 1 <= max - min)
        {
            for (int i = 0; i < index; i++)
            {
                int value = Random.Range(min, max + 1);
                if (i == 0)
                {
                    temp.Add(value);
                }
                else
                {
                    while (temp.Contains(value))
                    {
                        value = Random.Range(min, max + 1);
                    }
                    temp.Add(value);
                }
            }
            int[] differentRandomNum = temp.ToArray();
            temp.Clear();
            return differentRandomNum;
        }
        else
        {
            Debug.Log("Error the index is too big");

        }
        return temp.ToArray();
    }
    #endregion


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // UpdateUI();
       //    UpdateBottomSection();
        }
        
    }

    
}
