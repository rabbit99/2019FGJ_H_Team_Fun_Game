﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace William
{
    public class Player : MonoBehaviour,INotification
    {
        public LineRenderer hookObj;                //钩子起始点 附加有LineRenderer组件
        public float hookSpeed = 10f;               //钩子发射速度
        public float totalDistance = 3f;            //钩子最大长度
        public float stopDistance = 1f;
        public bool isOut = false;                  //钩子发射或者收回标记
        public Transform hookTransform;             //钩子物体（即小球）

        private Collider[] colCollection;           //发射中碰撞到的物体
        private float oriZvalue;
        private bool isStop = true;                 //用来判断钩子是否停止
        private float tempZvalue;

        public Transform playerTransform;
        public float pull_speed = 0.1f;
        private bool isHit = false;

        private Vector2 m_hit_pos;
        private CircleCollider2D m_hook_CircleCollider2D;
        private CircleCollider2D m_player_CircleCollider2D;
        public PlayerControll m_PlayerControll;

        private bool HookisBack = true;

        // Start is called before the first frame update
        void Start()
        {
            m_hook_CircleCollider2D = hookTransform.gameObject.GetComponent<CircleCollider2D>();
            m_hook_CircleCollider2D.enabled = false;
            m_player_CircleCollider2D = playerTransform.gameObject.GetComponent<CircleCollider2D>();
            m_PlayerControll.m_PlayerShoot = ShootHook;

            m_PlayerControll.m_PlayerMoveCondition = CheckSootingState;
        }

        // Update is called once per frame
        void Update()
        {
            checkCollider();
            CheckLength();

            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    isOut = true;
            //    m_hook_CircleCollider2D.enabled = true;
            //}
            
            

            //发射钩子
            if (isOut)
            {
                //Debug.Log("test");
                hookTransform.Translate(0, hookSpeed * Time.deltaTime, 0);
                hookObj.SetPosition(1, hookTransform.localPosition);
            }
            //收回钩子
            else
            {
                if (hookTransform.localPosition.y > oriZvalue && !isHit)
                {
                    hookTransform.Translate(0, -hookSpeed * Time.deltaTime, 0);
                    hookObj.SetPosition(1, hookTransform.localPosition);
                }
                else
                {
                    HookisBack = true;
                }
            }

            if (isHit)
            {
                //playerTransform.Translate(0, hookSpeed* Time.deltaTime, 0);
                playerTransform.transform.position = Vector3.Lerp(playerTransform.transform.position, m_hit_pos, pull_speed);
                hookObj.SetPosition(1, hookTransform.localPosition);
            }

            //判断钩子是否为停止状态
            if (hookTransform.childCount > 0)
            {
                isStop = Mathf.Abs(hookTransform.localPosition.z - tempZvalue) <= 0 ? true : false;
            }
            else
                isStop = false;
            //将敌人够到身前后，放开钩子
            if (isStop) RealeaseChild();
        }


        private void checkCollider()
        {
            //对钩子进行球形检测，返回所有碰到或者在球范围内的碰撞体数组     
            //注意将人称控制器及其子物体的Layer修改为不是Default的一个层，否则钩子会检测到自身的碰撞体
            colCollection = Physics.OverlapSphere(hookTransform.position, 0.2f, 1 << LayerMask.NameToLayer("Default"));
            if (colCollection.Length > 0)
            {
                foreach (Collider item in colCollection)
                {
                    //将敌人的tag设置为“Enemy”
                    if (item.gameObject.tag.Equals("wood"))
                        item.transform.SetParent(hookTransform);
                }
                isOut = false;
            }
        }

        private void CheckLength()
        {
            //Debug.Log("Distance" + Vector3.Distance(hookTransform.position, hookObj.transform.position));
            //Debug.Log("isHit Distance" + Vector3.Distance(playerTransform.position, m_hit_pos));
            if (Vector3.Distance(hookTransform.position, hookObj.transform.position) > totalDistance)
            {
                Debug.Log("isOut = false;");
                isOut = false;
                m_hook_CircleCollider2D.enabled = false;
            }
            if (Vector3.Distance(playerTransform.position, m_hit_pos) < stopDistance)
            {
                isHit = false;
                m_hook_CircleCollider2D.enabled = false;
                m_player_CircleCollider2D.isTrigger = false;
            }
        }

        private void RealeaseChild()
        {
            if (hookTransform.childCount > 0)
            {
                for (int i = 0; i < hookTransform.childCount; i++)
                {
                    hookTransform.GetChild(i).transform.SetParent(null);
                }
            }
        }

        public void Hit(Vector2 hit_pos)
        {
            m_hit_pos = hit_pos;
            isHit = true;
            isOut = false;
            m_player_CircleCollider2D.isTrigger = true;
        }

        private void ShootHook()
        {
            isOut = true;
            m_hook_CircleCollider2D.enabled = true;
            HookisBack = false;
        }

        private bool CheckSootingState()
        {
            return HookisBack;
        }

        private void BeHit(string result)
        {
            Debug.Log("BeHit = " + result);
            
        }


        #region Notification

        void AddNotificationObserver()
        {
            NotificationCenter.Default.AddObserver(this, NotificationKeys.HitPlayer);
        }
        
        void RemoveNotificationObserver()
        {
            NotificationCenter.Default.RemoveObserver(this, NotificationKeys.HitPlayer);
        }

        public void OnNotify(Notification _noti)
        {
            if (_noti.name == NotificationKeys.HitPlayer)
            {
                //TO DO
                //HitPlayer 
                BeHit((string)_noti.data);
            }
        }
        #endregion
    }
}