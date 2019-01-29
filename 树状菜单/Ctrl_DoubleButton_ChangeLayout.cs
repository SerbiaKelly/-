using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_DoubleButton_ChangeLayout : MonoBehaviour
{
    public DoubleClickButton doubleClickButton;
    public Vector2 defSize = new Vector2(176, 60);

    public Transform arrayGo;
    public Test_SetRectTranSize setRectTranSize;

	void Start()
	{
        doubleClickButton.onClick.AddListener(() => { });

        doubleClickButton.onDoubleClick.AddListener(() => 
        {
            UIState uIState = GetComponent<UIState>();
            uIState.StateToReverse();
            if (uIState.state)//显示子层
            {
                //Item调整自身大小  父级Array布局,大小调整  父父级Item大小调整
                //Item调整自身大小
                arrayGo.gameObject.SetActive(true);
                setRectTranSize.listMargins = new List<Transform>();
                setRectTranSize.listMargins.Add(doubleClickButton.transform);
                setRectTranSize.listMargins.Add(arrayGo);
                setRectTranSize.SetSize();

                //上级Item调整
                Show_Higher_SetLayout_R(transform.parent.parent);
            }
            else//收缩子层
            {
                //Item调整自身大小  父级Array布局,大小调整  父父级Item大小调整
                arrayGo.gameObject.SetActive(false);
                GetComponent<RectTransform>().sizeDelta = defSize;

                //上级Item调整
                Hide_Higher_SetLayout_R(transform.parent.parent);
            }
        });
	}

    /// <summary>
    /// 显示时 上级递归调整
    /// </summary>
    void Show_Higher_SetLayout_R(Transform item)
    {
        //Array
        Transform arrayGo = item.Find("Array");
        if (arrayGo)
        {
            Test_SetRectTranSize setRect = arrayGo.GetComponent<Test_SetRectTranSize>();
            setRect.SetLayout();
            if (arrayGo.childCount >= 1)
            {
                setRect.listMargins = new List<Transform>();
                setRect.listMargins.Add(arrayGo.GetChild(0));
                setRect.listMargins.Add(arrayGo.GetChild(arrayGo.childCount - 1));
                setRect.SetSize();
            }
        }

        //Item
        if (item.name == "Item")
        {
            Test_SetRectTranSize setRect0 = item.GetComponent<Test_SetRectTranSize>();
            Ctrl_DoubleButton_ChangeLayout ctrl_DoubleButton = item.GetComponent<Ctrl_DoubleButton_ChangeLayout>();
            setRect0.listMargins = new List<Transform>();
            setRect0.listMargins.Add(ctrl_DoubleButton.doubleClickButton.transform);
            setRect0.listMargins.Add(ctrl_DoubleButton.arrayGo);
            setRect0.SetSize();

            Show_Higher_SetLayout_R(item.parent.parent);
        }
    }

    /// <summary>
    /// 隐藏时 上级递归调整
    /// </summary>
    void Hide_Higher_SetLayout_R(Transform item)
    {
        //Array
        Transform arrayGo = item.Find("Array");
        if (arrayGo)
        {
            Test_SetRectTranSize setRect = arrayGo.GetComponent<Test_SetRectTranSize>();
            setRect.SetLayout();
            if (arrayGo.childCount >= 1)
            {
                setRect.listMargins = new List<Transform>();
                setRect.listMargins.Add(arrayGo.GetChild(0));
                setRect.listMargins.Add(arrayGo.GetChild(arrayGo.childCount - 1));
                setRect.SetSize();
            }
        }

        //Item
        if (item.name == "Item")
        {
            Test_SetRectTranSize setRect0 = item.GetComponent<Test_SetRectTranSize>();
            Ctrl_DoubleButton_ChangeLayout ctrl_DoubleButton = item.GetComponent<Ctrl_DoubleButton_ChangeLayout>();
            setRect0.listMargins = new List<Transform>();
            setRect0.listMargins.Add(ctrl_DoubleButton.doubleClickButton.transform);
            setRect0.listMargins.Add(ctrl_DoubleButton.arrayGo);
            setRect0.SetSize();

            Show_Higher_SetLayout_R(item.parent.parent);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////Static////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////

    public static void ArraySetSize(Transform arrayGo)
    {
        if (arrayGo.childCount >= 1)
        {
            Test_SetRectTranSize setRect = arrayGo.GetComponent<Test_SetRectTranSize>();
            setRect.listMargins = new List<Transform>();
            setRect.listMargins.Add(arrayGo.GetChild(0));
            setRect.listMargins.Add(arrayGo.GetChild(arrayGo.childCount - 1));
            setRect.SetSize();
        }
    }

    public static void ItemSetSize()
    {

    }
}
