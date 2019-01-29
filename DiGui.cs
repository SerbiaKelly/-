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

namespace SimpleUIFrame
{
    public class DiGui : MonoBehaviour
    {
        public Transform tran1;
        public Transform tran2;
        List<TranInfo> allFaseTranList = new List<TranInfo>();

        void Start()
        {
            GetAllFalseTranInfo(tran1,tran1, allFaseTranList);
            Set(tran2, allFaseTranList);
        }

        void Set(Transform maxParent, List<TranInfo> allFaseTranList)
        {
            foreach (var item in allFaseTranList)
            {
                SetOne(maxParent, item);
            }
        }

        void SetOne(Transform maxParent, TranInfo tranInfo)
        {
            Transform end = maxParent;
            for (int i = 1; i < tranInfo.indexList.Count; i++)
            {
                end = end.GetChild(tranInfo.indexList[i]);
            }

            end.gameObject.SetActive(tranInfo.state);
        }

        /// <summary>
        /// 获取所有激死的物体信息List
        /// </summary>
        /// <param name="maxParent"></param>
        /// <param name="tran"></param>
        /// <param name="allFaseTranList"></param>
        /// <returns></returns>
        List<TranInfo> GetAllFalseTranInfo(Transform maxParent, Transform tran, List<TranInfo> allFaseTranList)
        {
            for (int i = 0; i < tran.childCount; i++)
            {
                Transform child = tran.GetChild(i);
                if (child.gameObject.activeSelf == false)
                {
                    List<int> indexList = GetTranIndexList(maxParent, child, new List<int>());
                    TranInfo tranInfo = new TranInfo(child, child.gameObject.activeSelf, child.gameObject.name, indexList);
                    allFaseTranList.Add(tranInfo);
                }

                GetAllFalseTranInfo(maxParent, child, allFaseTranList);
            }

            return allFaseTranList;
        }

        /// <summary>
        /// 获取一个物体的层级list
        /// </summary>
        /// <param name="maxParent"></param>
        /// <param name="tran"></param>
        List<int> GetTranIndexList(Transform maxParent, Transform tran, List<int> list)
        {
            if (tran.parent == null)
            {
                list.Add(0);
                list.Reverse();
                return list;
            }

            if (!ReferenceEquals(tran.parent, maxParent))
            {
                list.Add(GetGoIndex(tran));
                return GetTranIndexList(maxParent, tran.parent, list);
            }
            else
            {
                list.Add(GetGoIndex(tran));
                list.Add(0);
                list.Reverse();
                return list;
            }
        }

        /// <summary>
        /// 层级下的位置
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        int GetGoIndex(Transform tran)
        {
            Transform parent = tran.parent;

            if (parent == null)
            {
                return 0;
            }

            for (int i = 0; i < parent.childCount; i++)
            {
                if (ReferenceEquals(tran, parent.GetChild(i)))
                {
                    return i;
                }
            }

            return 0;
        }
    }

    public class TranInfo
    {
        public Transform tran;
        public bool state;
        public string name;

        public List<int> indexList;

        public TranInfo(Transform tran, bool state, string name, List<int> indexList)
        {
            this.tran = tran;
            this.state = state;
            this.name = name;
            this.indexList = indexList;
        }

        public override string ToString()
        {
            string s = "Name:{0}   State:{1}   indexList:{2}";

            string indexToString = "";
            for (int i = 0; i < indexList.Count; i++)
            {
                indexToString += indexList[i].ToString();
            }

            string str = string.Format(s,this.name, this.state, indexToString);
            return str;
        }
    }
}
