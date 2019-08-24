using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControll : MonoBehaviour
{
    public enum MoveState
    {
        idle, up, down
    }
    public enum RotateState
    {
        idle, left, right
    }
    public MoveState _MoveState;
    public RotateState _RotateState;
    public GameObject targetObj;
    [System.NonSerialized]
    public float moveSpeed, rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (_MoveState)
        {
            case MoveState.idle:
                break;
            case MoveState.up:
                transform.position += moveSpeed*transform.up * Time.fixedDeltaTime;
                if (targetObj)
                    targetObj.transform.position = transform.position;
                break;
            case MoveState.down:
                transform.localPosition -= moveSpeed*transform.up * Time.fixedDeltaTime;
                if(targetObj)
                    targetObj.transform.position = transform.position;
                break;
            default:
                break;
        }
        switch (_RotateState)
        {
            case RotateState.idle:
                break;
            case RotateState.left:
                transform.Rotate(0, 0, -rotateSpeed * Time.fixedDeltaTime);
                break;
            case RotateState.right:
                transform.Rotate(0, 0, rotateSpeed * Time.fixedDeltaTime);
                break;
            default:
                break;
        }
    }
    public void moveCall(int moveState)
    {
        switch (moveState)
        {
            case 0:
                _MoveState = MoveState.up;
                break;
            case 1:
                _MoveState = MoveState.down;
                break;
            case 2:
                _RotateState = RotateState.left;
                break;
            case 3:
                _RotateState = RotateState.right;
                break;
            case 4:
                _MoveState = MoveState.idle;
                break;
            case 5:
                _RotateState = RotateState.idle;
                break;
            case 6:
                break;
            default:
                break;
        }
    }
}
