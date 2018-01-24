using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;

public class ReadJson : MonoBehaviour {
    private JsonData itemDate;
    private string jsonString;
    
   public List<Information> myinformationList = new List<Information>();
    // Use this for initialization
  //  [SerializeField]
  //  Image image;
    
    void Start () {
        jsonString = Resources.Load<TextAsset>("information").text;
        itemDate = JsonMapper.ToObject(jsonString);
        readjson();

        foreach (Information item in myinformationList)
        {
            for (int i = 0; i < item.SubTitle.Length; i++)
            {
                Debug.Log("Id: " + item.ID + "  BigTitle:" + item.BigTitle + "  subtitle:" +item.SubTitle[i]);
            }
            item.setupImage();
        }
        foreach (Information item in myinformationList)
        {
            item.setupDictionary();
        }
            //DEBUG IMAGE
            //  myinformationList[0].getImage();
            // Debug.Log(myinformationList[0].BackgroundImage.name);
            //   image.sprite = myinformationList[0].BackgroundImage;
        }

    void readjson()
    {
        for (int i = 0; i < itemDate["information"].Count; i++)
        {

            int id = int.Parse(itemDate["information"][i]["id"].ToString());//get id;

            string bigTitle = itemDate["information"][i]["BigTitle"].ToString();//get bigtitle;


            string[] subtitle = getStringArrayFromTag(i, "SubTitle");//get subtitle

            string[] mainContent = getStringArrayFromTag(i, "MainContent");//get MainContent

            string[] othertitleContent = getStringArrayFromTag(i, "OtherTitleContent");//get OtherTitleContent

            myinformationList.Add(new Information(id,bigTitle,subtitle,mainContent, othertitleContent));
        }

      //  Debug.Log(myinformationList[0].BigTitle + myinformationList[0].SubTitle[0] + myinformationList[0].MainContent[0]);
    }

    string[] getStringArrayFromTag(int i,string tag) {
        string[] temp;
        List<string> List = new List<string>();//get subtitle;
        for (int j = 0; j < itemDate["information"][i][tag].Count; j++)
        {
            List.Add(itemDate["information"][i][tag][j].ToString());
        }
        temp = List.ToArray();
        return temp;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
