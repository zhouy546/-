using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MouseBehavior : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler  {
    public enum Move
    {
        MoveRight,MoveLeft,Idle
    }

    public UIClinet uiclient;

    struct ClientInfo{
        public Vector2 StartPos;
        public Vector2 UpdatePos;
        public Vector2 EndPos;
        private float movedis;
        public float MoveDistance
        {
            get { return movedis = (EndPos-StartPos).magnitude; }
        }
    }
    ClientInfo client;
    //public void Do
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("Start position is : " +client.StartPos);
        client.StartPos = eventData.position;
        client.EndPos = eventData.position;

    }

    public void OnDrag(PointerEventData eventData) {
        client.UpdatePos = eventData.position;       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End position is : " + client.EndPos);
        client.EndPos = eventData.position;
        Debug.Log("Move Distance is : "+client.MoveDistance);
        MoveBehavior(MoveDirection());

    }

    public void MoveBehavior(Move move) {
        switch (move)
        {
            case Move.MoveRight:
                uiclient.MoveRight("right");
                Debug.Log("rigt");
                break;
            case Move.MoveLeft:
                uiclient.MoveLeft("left");
                Debug.Log("left");
                break;
            case Move.Idle:
                Debug.Log("IDLE");
                break;
            default:
                break;
        }
    }

    public Move MoveDirection()
    {
        if (xAxisDis() > 0 && client.MoveDistance > 100)
        {
            return Move.MoveRight;
        }
        else if (xAxisDis() == 0)
        {
            return Move.Idle;
        } else if(xAxisDis()<0&&client.MoveDistance>100) {
           return Move.MoveLeft;
        }     
        return Move.Idle;
    }

    float xAxisDis() {
        return client.EndPos.x - client.StartPos.x;
    }


    public void OnDrop(PointerEventData eventData) {
       // Debug.Log("OnDrop");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
