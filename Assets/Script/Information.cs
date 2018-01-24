using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Information : MonoBehaviour {



    public int ID;
    public string BigTitle;
    public string[] SubTitle;
    public string[] MainContent;
    public string[] OtherTitleContent;
    public Sprite[] SubImage;
    public Sprite[] OtherTitleImage;
    public Sprite BackgroundImage;

    public Dictionary<int, string> ID_BigTitledictionary = new Dictionary<int, string>();
    public Dictionary<string, string[]> BigTitle_SubTitledictionary = new Dictionary<string, string[]>();
    public Dictionary<string, string> SubTitle_MainContentdictionary = new Dictionary<string, string>();
    public Dictionary<string, Sprite> Bigtitle_BackgroundImagedictionary = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> SubTitle_SubImagedictionary = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> OtherTitleContent_OtherTitleImagedictionary = new Dictionary<string, Sprite>();

    public Information() { }

    public Information(int _id, string _bigTitle,string[] _subTitle, string [] _mainContent,  string[] _otherTitleContent) {
        ID = _id;
        BigTitle = _bigTitle;
        SubTitle = _subTitle;
        MainContent = _mainContent;
        OtherTitleContent = _otherTitleContent;
    }

    public void setupDictionary() {
        ID_BigTitledictionary.Add(this.ID, this.BigTitle);

        BigTitle_SubTitledictionary.Add(BigTitle, SubTitle);

        Bigtitle_BackgroundImagedictionary.Add(BigTitle, BackgroundImage);

        for (int i = 0; i < MainContent.Length; i++)
        {
            SubTitle_MainContentdictionary.Add(SubTitle[i], MainContent[i]);
        }
     

        for (int j = 0; j < SubTitle.Length; j++)
       {
           SubTitle_SubImagedictionary.Add(SubTitle[j], SubImage[j]);
       }

        for (int k = 0; k < OtherTitleContent.Length; k++)
        {
            OtherTitleContent_OtherTitleImagedictionary.Add(OtherTitleContent[k], OtherTitleImage[k]);
        }
    }

    public void setupImage() {
        BackgroundImage = ReadImage("Image/"+ BigTitle+"/"+ BigTitle);
        SubImage = ArraySprite(SubTitle);
        OtherTitleImage = ArraySprite(OtherTitleContent);

    }

    Sprite[] ArraySprite(string[] tag) {
        List<Sprite> sprite = new List<Sprite>();
        for (int i = 0; i < tag.Length; i++)
        {         
            Sprite temp = ReadImage("Image/" + BigTitle + "/" + tag[i]);
            sprite.Add(temp);
        }

        return sprite.ToArray();
    }
    /*
    Sprite[] ReadAllImage(string path) {

        Sprite[] temp = Resources.LoadAll<Sprite>(path);
        return temp;
    }
    */
    Sprite ReadImage(string path) {
        Debug.Log(path);
        Sprite temp = Resources.Load<Sprite>(path);
        Debug.Log(temp);
        return temp;
    }
}
