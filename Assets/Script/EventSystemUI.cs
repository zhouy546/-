using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSystemUI : MonoBehaviour {
    public UnityEvent OnPointerClick;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            OnPointerClick.Invoke();
        }
	}

  //   protected virtual void OnPointerClick(PointerEventData eventData)
   //   {
 //       onPointerClick.Invoke();
  //  }
}
