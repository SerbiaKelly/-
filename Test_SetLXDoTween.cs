/***
*	Title："XXX" 项目
*		主题：XXX
*	Description：
*		功能：XXX
*	Date：2017
*	Version：0.1版本
*	Author：Coffee
*	Modify Recoder：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UIFrame
{
	public class Test_SetLXDoTween : BaseDoTween
	{
        public int currentTweenNum = 0;                                         //当前播放的Tween编号
        //public float allTweenSpeed = 0.3f;                                    //设置该物体的所有Tween速度

        public List<BaseDoTween> baseDoTweens;                                  //顺序保存所有的BaseTween对象(BaseSetting()中获取)

        void Start()
		{
            SetEventLX(baseDoTweens);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PlayForward();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                PlayBackwards();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                TogglePause();
            }
        }

        public override void PlayForward()
        {
            baseDoTweens[currentTweenNum].tweener.PlayForward();
        }

        public override void PlayBackwards()
        {
            baseDoTweens[currentTweenNum].tweener.PlayBackwards();
        }

        void TogglePause()
        {
            baseDoTweens[currentTweenNum].tweener.TogglePause();
        }

        public override void BaseSetting()
        {
            //添加到List去掉最后没有tweener的一项
            BaseDoTween[] arrary = GetComponents<BaseDoTween>();
            baseDoTweens = new List<BaseDoTween>();
            for (int i = 0; i < arrary.Length - 1; i++)
            {
                baseDoTweens.Add(arrary[i]);
            }

            //设置currentTweenNum当前播放的Tween编号
            for (int i = 0; i < baseDoTweens.Count; i++)
            {
                int n = i;
                baseDoTweens[n].tweener.OnPlay(delegate () { currentTweenNum = n; });
            }

            //baseDoTweens[0].tweener.onComplete += delegate () { baseDoTweens[1].tweener.PlayForward(); };
            //baseDoTweens[1].tweener.onComplete += delegate () { baseDoTweens[2].tweener.PlayForward(); };

            //baseDoTweens[2].tweener.onRewind += delegate () { baseDoTweens[1].tweener.PlayBackwards(); };
            //baseDoTweens[1].tweener.onRewind += delegate () { baseDoTweens[0].tweener.PlayBackwards(); };

            //串联事件
            for (int i = 0; i < baseDoTweens.Count - 1; i++)//012 Count 3
            {
                int n = i;
                if (n < baseDoTweens.Count - 1)//0 到 Count - 1
                {
                    baseDoTweens[n].tweener.OnComplete(delegate () { baseDoTweens[n + 1].tweener.PlayForward(); });
                }

                int num = baseDoTweens.Count - (n + 1);
                if (num >= 1 || num <= baseDoTweens.Count - 1)//Count - 1 到 1
                {
                    baseDoTweens[num].tweener.OnRewind(delegate () { baseDoTweens[num - 1].tweener.PlayBackwards(); });
                }
            }
        }

        /// <summary>
        /// 串联事件
        /// </summary>
        void SetEventLX(List<BaseDoTween> list)
        {
            //设置currentTweenNum当前播放的Tween编号
            for (int i = 0; i < baseDoTweens.Count; i++)
            {
                int n = i;
                baseDoTweens[n].tweener.OnPlay(delegate () { currentTweenNum = n; });
            }

            //串联事件
            for (int i = 0; i < list.Count - 1; i++)//012 Count 3
            {
                int n = i;
                if (n < list.Count - 1)//0 到 Count - 1
                {
                    list[n].tweener.OnComplete(delegate () { list[n + 1].tweener.PlayForward(); });
                }

                int num = list.Count - (n + 1);
                if (num >= 1 || num <= list.Count - 1)//Count - 1 到 1
                {
                    list[num].tweener.OnRewind(delegate () { list[num - 1].tweener.PlayBackwards(); });
                }
            }
        }

        /// <summary>
        /// 设置所有的Tween的duration
        /// </summary>
        //[ContextMenu("SetThisGoAllTweenSpeed")]
        //public void SetThisGoAllTweenSpeed()
        //{
        //    BaseDoTween[] arrary = GetComponents<BaseDoTween>();

        //    arrary[0].duration = allTweenSpeed;
        //    foreach (var item in arrary)
        //    {
        //        item.duration = allTweenSpeed;
        //    }
        //}
    }
}
