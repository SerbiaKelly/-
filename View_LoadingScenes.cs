/***
*	Title："智慧工厂" 项目
*		主题：视图层：场景加载
*	Description：
*		功能：
*		    1、实现场景的转换加载功能
*	Date：2018
*	Version：0.1版本
*	Author：Coffee
*	Modify Recoder：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Global;

namespace View
{
    public class View_LoadingScenes : MonoBehaviour
    {
        public Slider loadingSlider;

        public Text loadingText;

        private float loadingSpeed = 1f;

        private float targetValue;

        private AsyncOperation operation;

        void Start()
        {
            loadingSlider.value = 0.0f;

            StartCoroutine(AsyncLoading());
        }

        /// <summary>
        /// 协程异步加载下一场景
        /// </summary>
        /// <returns></returns>
        IEnumerator AsyncLoading()
        {
            operation = SceneManager.LoadSceneAsync("Launch");

            //阻止当加载完成自动切换
            operation.allowSceneActivation = false;

            yield return operation;
        }

        /// <summary>
        /// 显示进度条
        /// </summary>
        private void Update()
        {
            //targetValue = operation.progress;
            //if (operation.progress >= 0.9f)
            //{
            //    //operation.progress的值最大为0.9
            //    targetValue = 1.0f;
            //}

            //if (targetValue != loadingSlider.value)
            //{
            //    //插值运算
            //    loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);
            //    if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
            //    {
            //        loadingSlider.value = targetValue;
            //    }
            //}

            //loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

            //if ((int)(loadingSlider.value * 100) == 100)
            //{
            //    //允许异步加载完毕后自动切换场景
            //    operation.allowSceneActivation = true;
            //}


            Update2();
        }

        public float fl = 0;

        void Update2()
        {
            targetValue = operation.progress;
            if (operation.progress >= 0.9f)
            {
                //operation.progress的值最大为0.9
                targetValue = 1.0f;
            }

            if (targetValue != fl)
            {
                //插值运算
                fl = Mathf.Lerp(fl, targetValue, Time.deltaTime * loadingSpeed);
                if (Mathf.Abs(fl - targetValue) < 0.01f)
                {
                    fl = targetValue;
                }
            }
            loadingText.text = ((int)(fl * 100)).ToString() + "%";

            if ((int)(fl * 100) == 100)
            {
                //允许异步加载完毕后自动切换场景
                operation.allowSceneActivation = true;
            }
        }


    }//class_end
}
