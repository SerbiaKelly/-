/***
*	Title："智慧工厂" 项目
*		主题：控制鼠标移动物体
*	Description：
*		功能：点击鼠标左键控制物体360展示
*	Date：2018
*	Version：0.1版本
*	Author：Coffee
*	Modify Recoder：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global
{
	public class Global_FllowMouseRotate : MonoBehaviour
    {
        public static Global_FllowMouseRotate instance;

        public bool Switch
        {
            get
            {
                return this.enabled;
            }

            set
            {
                this.enabled = value;
            }
        }

        private void Awake()
        {
            instance = this;
        }

        public Transform target;                                                //目标物体
        public Transform targer_st;
        public float xSpeed = 200, ySpeed = 200, mSpeed = 10;                   //移动速度
        public float yMinLimit = -50, yMaxLimit = 50;                           //摄像机的Y轴移动最小最大限制
        public float distance = 7, minDistance = 2, maxDistance = 30;           //摄像机与目标物体的距离
        public float num = 0;
        
        public bool needDamping = true;                                         //阻尼默认开启
        float damping = 5.0f;                                                   //默认阻尼为5.0F

        public float x = 0.0f;                                                  //X轴
        public float y = 0.0f;                                                  //Y轴

        public Vector3 m_offset_0;                                               //矫正旋转中心
        public Vector3 m_offset_1;                                               //调整目标屏幕位置

        public Transform[] see_;

        //初始化
        void Start()
        {
            targer_st = target;
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
        }

        /// <summary>
        /// 检测鼠标是否按下进行旋转物体
        /// </summary>
        void LateUpdate()
        {
            if (target)
            {
                //使用按下鼠标左键移动物体
                if (Input.GetMouseButton(0))
                {
                    x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                    y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                    y = ClampAngle(y, yMinLimit, yMaxLimit);
                }


                distance -= Input.GetAxis("Mouse ScrollWheel") * mSpeed;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);


                Quaternion rotation = Quaternion.Euler(y, x, 0.0f);

                Vector3 disVector = new Vector3(0f, 1.0f, -distance);

                disVector = new Vector3(disVector.x + m_offset_1.x, disVector.y + m_offset_1.y, disVector.z + m_offset_1.z);

                Vector3 position = rotation * disVector + new Vector3(target.position.x + m_offset_0.x, target.position.y + m_offset_0.y, target.position.z + m_offset_0.z);
                //adjust the camera
                if (needDamping)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * damping);
                    transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * damping);
                }
                else
                {
                    transform.rotation = rotation;
                    transform.position = position;
                }


            }
        }

        /// <summary>
        /// 旋转角度的控制
        /// </summary>
        /// <param name="angle">旋转的角度</param>
        /// <param name="min">最小角度</param>
        /// <param name="max">最大角度</param>
        /// <returns></returns>
        static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }
        public void See_1()
        {
            num = 1.5f;
            target = see_[0];
            distance = 2;
        }
        public void See_2()
        {
            num = 1.5f;
            target = see_[1];
            distance = 2;
        }
        public void See_3()
        {
            num = 1.5f;
            target = see_[2];
            distance = 2;
        }

        public void See_4()
        {
            num = 1.5f;
            target = see_[3];
            distance = 2;
        }

        public void See_5()
        {
            num = 1.5f;
            target = see_[4];
            distance = 2;
        }

        public void See_6()
        {
            num = 1.5f;
            target = see_[5];
            distance = 2;
        }

        public void See_7()
        {
            num = 1.5f;
            target = see_[6];
            distance = 2;
        }

        public void See_8()
        {
            num = 1.5f;
            target = see_[7];
            distance = 2;
        }

        public void See_9()
        {
            num = 1.5f;
            target = see_[8];
            distance = 2;
        }
        public void See_10()
        {
            num = 1.5f;
            target = see_[9];
            distance = 2;
        }
        public void See_11()
        {
            num = 1.5f;
            target = see_[10];
            distance = 2;
        }
        public void See_12()
        {
            num = 1.5f;
            target = see_[11];
            distance = 2;
        }
        public void See_13()
        {
            num = 1.5f;
            target = see_[12];
            distance = 2;
        }
        public void See_14()
        {
            num = 1.5f;
            target = see_[13];
            distance = 2;
        }
        public void See_15()
        {
            num = 1.5f;
            target = see_[14];
            distance = 2;
        }
        public void See_16()
        {
            num = 1.5f;
            target = see_[15];
            distance = 2;
        }
        public void See_17()
        {
            num = 1.5f;
            target = see_[16];
            distance = 2;
        }
        public void See_18()
        {
            num = 1.5f;
            target = see_[17];
            distance = 2;
        }
        public void See_19()
        {
            num = 1.5f;
            target = see_[18];
            distance = 2;
        }
        public void Fanhui()
        {
            num = 0;
            target = targer_st;
            distance = 7;
        }


    }//class_end
}
