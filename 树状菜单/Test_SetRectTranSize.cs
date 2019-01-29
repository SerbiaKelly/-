using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 计算出最大框 gameObject物体轴心点为(x, 1)
/// </summary>
public class Test_SetRectTranSize : MonoBehaviour
{
    public List<Transform> listMargins;//传入边缘框 
    void Start()
	{
        
    }
	
	void Update()
	{
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    SetContent(GetComponent<RectTransform>(), listMargins);
        //}
    }

    [ContextMenu("SetSize")]
    public void SetSize()
    {
        SetContent(GetComponent<RectTransform>(), listMargins);
    }

    [ContextMenu("SetSizeLayout")]
    public void SetSizeLayout()
    {
        SetContentLayout(GetComponent<RectTransform>(), listMargins);
    }

    [ContextMenu("SetLayout")]
    public void SetLayout()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform curRect = transform.GetChild(i).GetComponent<RectTransform>();
            RectTransform lastRect = null;
            if (i - 1 >= 0)
            {
                lastRect = transform.GetChild(i - 1).GetComponent<RectTransform>();
            }
            float x = curRect.anchoredPosition.x;
            float y;
            if (lastRect == null)
            {
                y = 0;
            }
            else
            {
                y = lastRect.anchoredPosition.y - lastRect.sizeDelta.y;//
            }
            curRect.anchoredPosition = new Vector2(x, y);
        }
    }

    void SetContent(RectTransform rectTran, List<Transform> listMargins)
    {
        if (rectTran == null || listMargins == null || listMargins.Count == 0)
        {
            print("return");
            return;
        }

        //保存还原参数 (第一层子物体)
        List<Transform> childs = new List<Transform>();
        List<Vector3> listStartPos = new List<Vector3>();
        for (int i = 0; i < rectTran.childCount; i++)
        {
            Transform tran = rectTran.GetChild(i);
            childs.Add(tran);
            listStartPos.Add(tran.position);
        }

        MarginValue marginValue = RetSizeDelta(listMargins);
        rectTran.position = new Vector3(marginValue.leftBest, marginValue.topBest, rectTran.position.z);
        rectTran.sizeDelta = marginValue.GetSizeDelta(rectTran);

        //还原开始坐标
        for (int i = 0; i < childs.Count; i++)
        {
            childs[i].position = listStartPos[i];
        }
    }

    void SetContentLayout(RectTransform rectTran, List<Transform> listMargins)
    {
        //保存还原参数 (第一层子物体)
        List<Transform> childs = new List<Transform>();
        List<Vector3> listStartPos = new List<Vector3>();
        for (int i = 0; i < rectTran.childCount; i++)
        {
            Transform tran = rectTran.GetChild(i);
            childs.Add(tran);
            listStartPos.Add(tran.position);
        }

        MarginValue marginValue = RetSizeDelta(listMargins);
        rectTran.position = new Vector3(marginValue.leftBest, marginValue.topBest, rectTran.position.z);
        //rectTran.sizeDelta = marginValue.GetSizeDelta(rectTran);
        Vector2 v = marginValue.GetSizeDelta(rectTran);
        rectTran.GetComponent<LayoutElement>().preferredWidth = v.x;
        rectTran.GetComponent<LayoutElement>().preferredHeight = v.y;

        //还原开始坐标
        for (int i = 0; i < childs.Count; i++)
        {
            childs[i].position = listStartPos[i];
        }
    }

    /// <summary>
    /// 使用这些物体获取边缘值
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    MarginValue RetSizeDelta(List<Transform> list)
    {
        MarginValue marginValue = new MarginValue(int.MaxValue, int.MinValue, int.MinValue, int.MaxValue);

        foreach (var item in list)
        {
            //listStartPos.Add(item.position);

            MarginValue temp = GetRectTranMarginValue(item.GetComponent<RectTransform>());
            if (temp.leftBest < marginValue.leftBest)
            {
                marginValue.leftBest = temp.leftBest;
            }
            if (temp.rightBest > marginValue.rightBest)
            {
                marginValue.rightBest = temp.rightBest;
            }
            if (temp.topBest > marginValue.topBest)
            {
                marginValue.topBest = temp.topBest;
            }
            if (temp.bomBest < marginValue.bomBest)
            {
                marginValue.bomBest = temp.bomBest;
            }
        }

        return marginValue;
    }

    List<Transform> GetAllChild(Transform go, bool isincludeSelf)
    {
        List<Transform> list = new List<Transform>();

        if (isincludeSelf)
        {
            list.Add(go);
        }

        Get(go, list);

        return list;
    }

    void Get(Transform parent, List<Transform> list)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform tran = parent.GetChild(i);
            list.Add(tran);

            Get(tran, list);
        }
    }

    /// <summary>
    /// 世界坐标值
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <returns></returns>
    public MarginValue GetRectTranMarginValue(RectTransform rectTransform)
    {
        float x1 = rectTransform.pivot.x * rectTransform.sizeDelta.x;
        Vector3 localPosition = new Vector3(rectTransform.localPosition.x - x1, 0, 0);
        Vector3 postion = rectTransform.parent.TransformPoint(localPosition);
        float left = postion.x;

        float x2 = (1 - rectTransform.pivot.x) * rectTransform.sizeDelta.x;
        localPosition = new Vector3(rectTransform.localPosition.x + x2, 0, 0);
        postion = rectTransform.parent.TransformPoint(localPosition);
        float right = postion.x;

        float y1 = (1 - rectTransform.pivot.y) * rectTransform.sizeDelta.y;
        localPosition = new Vector3(0, rectTransform.localPosition.y + y1, 0);
        postion = rectTransform.parent.TransformPoint(localPosition);
        float top = postion.y;

        float y2 = rectTransform.pivot.y * rectTransform.sizeDelta.y;
        localPosition = new Vector3(0, rectTransform.localPosition.y - y2, 0);
        postion = rectTransform.parent.TransformPoint(localPosition);
        float bom = postion.y;

        return new MarginValue(left, right, top, bom);
    }

    public class MarginValue
    {
        public float leftBest;
        public float rightBest;
        public float topBest;
        public float bomBest;

        public MarginValue(float leftBest, float rightBest, float topBest, float bomBest)
        {
            this.leftBest = leftBest;
            this.rightBest = rightBest;
            this.topBest = topBest;
            this.bomBest = bomBest;
        }

        public Vector2 GetSizeDelta(RectTransform transform)
        {
            Vector3 localRX = transform.InverseTransformPoint(rightBest, 0, 0);
            Vector3 localLX = transform.InverseTransformPoint(leftBest, 0, 0);
            float x = localRX.x - localLX.x;

            Vector3 localTY = transform.InverseTransformPoint(0, topBest, 0);
            Vector3 localBY = transform.InverseTransformPoint(0, bomBest, 0);
            float y = localTY.y - localBY.y;
            return new Vector2(x, y);
        }
    }
}
