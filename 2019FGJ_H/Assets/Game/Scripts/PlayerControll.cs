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

    public delegate void PlayerShoot();
    public PlayerShoot m_PlayerShoot;

    private Animator tarAni;
    public delegate bool PlayerMoveCondition();
    public PlayerMoveCondition m_PlayerMoveCondition;
    // Start is called before the first frame update
    void Start()
    {
        if(targetObj != null)
        {
            tarAni = targetObj.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_PlayerMoveCondition != null)
        {
            if (!m_PlayerMoveCondition())
                return;
        }

        switch (_MoveState)
        {
            case MoveState.idle:
                if(tarAni != null)
                {
                    tarAni.Play("Idle");
                }
                break;
            case MoveState.up:
                transform.position += moveSpeed*transform.up * Time.fixedDeltaTime;
                if (targetObj)
                {
                    targetObj.transform.position = transform.position;
                    if (transform.rotation.eulerAngles.z >= 0&& transform.rotation.eulerAngles.z < 180)
                    {
                        tarAni.Play("run1");
                    }
                    else if(transform.rotation.eulerAngles.z >=180 && transform.rotation.eulerAngles.z < 360)
                    {
                        tarAni.Play("run2");
                    }
                }
                break;
            case MoveState.down:
                transform.localPosition -= moveSpeed*transform.up * Time.fixedDeltaTime;
                if (targetObj)
                {
                    targetObj.transform.position = transform.position;
                    if (transform.rotation.z > 0 && transform.rotation.z <= 180)
                    {
                        tarAni.Play("run1");
                    }
                    else if (transform.rotation.z <= 0 && transform.rotation.z > -180)
                    {
                        tarAni.Play("run2");
                    }
                }
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
                Debug.Log("shoot");
                if(m_PlayerShoot != null)
                {
                    m_PlayerShoot();
                }
                break;
            default:
                break;
        }
    }
}
