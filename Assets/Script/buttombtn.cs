using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttombtn : MonoBehaviour {
    public UIClinet uIClinet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBtnClick() {
        uIClinet.myinfo.CurrentTitle =  this.GetComponent<Text>().text;

        foreach (var item in ReadJson.instance.myinformationList)
        {
            if (item.BigTitle == uIClinet.myinfo.CurrentTitle) {
                uIClinet.myinfo.CurrentID = item.ID;
            }
        }           
        uIClinet.myinfo.CurrentSubtitleNum = uIClinet.myinfo.DefaultSubtitleNum;
        uIClinet.UpdateUI();
    }
}
