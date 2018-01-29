using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class myjoint : MonoBehaviour {
    public SkeletonRenderer skeletonRenderer;//记得要把你的SDK代码所在的gameobject拖拽到这个代码所在的gameboject里的inspect上面的对应位置
    
    public Image image;//这是是你的图片
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        image.transform.position = getPostiont();//这个就是让你图片位置等于关节位置。

    }

    Vector3 getPostiont() {
        return  skeletonRenderer._bodySkeletons[0][0].transform.position;//这个是获得另一个script 里dictionary 的value第一个中括号里面的数字代表第一个人。 第二个数字代表第几个joint。 所以可与试着改下第二个数字比如改成4肯能就是手的位置。
    }
}
