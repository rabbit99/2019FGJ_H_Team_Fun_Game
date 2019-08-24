using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace William
{
    public class Player : MonoBehaviour
    {
        public LineRenderer hookObj;                //钩子起始点 附加有LineRenderer组件
        public float hookSpeed = 10f;               //钩子发射速度
        public float totalDistance = 3f;            //钩子最大长度
        public bool isOut = false;                  //钩子发射或者收回标记
        public Transform hookTransform;             //钩子物体（即小球）

        private Collider[] colCollection;           //发射中碰撞到的物体
        private float oriZvalue;
        private bool isStop = true;                 //用来判断钩子是否停止
        private float tempZvalue;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                isOut = true;
            }
            
            checkCollider();
            CheckLength();

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
                if (hookTransform.localPosition.y > oriZvalue)
                {
                    hookTransform.Translate(0, -hookSpeed * Time.deltaTime,0);
                    hookObj.SetPosition(1, hookTransform.localPosition);
                }
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

        void checkCollider()
        {
            //对钩子进行球形检测，返回所有碰到或者在球范围内的碰撞体数组     
            //注意将人称控制器及其子物体的Layer修改为不是Default的一个层，否则钩子会检测到自身的碰撞体
            colCollection = Physics.OverlapSphere(hookTransform.position, 0.2f, 1 << LayerMask.NameToLayer("Default"));
            if (colCollection.Length > 0)
            {
                foreach (Collider item in colCollection)
                {
                    //将敌人的tag设置为“Enemy”
                    if (item.gameObject.tag.Equals("Enemy"))
                        item.transform.SetParent(hookTransform);
                }
                isOut = false;
            }
        }

        void CheckLength()
        {
            //Debug.Log("Distance" + Vector3.Distance(hookTransform.position, hookObj.transform.position));
            if (Vector3.Distance(hookTransform.position, hookObj.transform.position) > totalDistance)
            {
                isOut = false;
            }
        }

        void RealeaseChild()
        {
            if (hookTransform.childCount > 0)
            {
                for (int i = 0; i < hookTransform.childCount; i++)
                {
                    hookTransform.GetChild(i).transform.SetParent(null);
                }
            }
        }

    }
}