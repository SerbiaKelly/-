using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onClick = new UnityEvent();
    public UnityEvent onDoubleClick = new UnityEvent();
    public UnityEvent onLongPress = new UnityEvent();

    /// <summary>
    /// 计时
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    IEnumerator IE()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            curTime += Time.deltaTime;
            if (onclickNums == 1 && curTime > waitTime && isUp == true && isLongPress == false)
            {
                //单击
                onclickNums = 0;
                curTime = 0;
                onClick.Invoke();
                //print("单击");
                break;
            }
            else if (isUp == false && onclickNums == 1 && curTime > longPressTime)
            {
                //长按
                isLongPress = true;
                onLongPress.Invoke();
                //print("长按");
            }
        }
    }

    int onclickNums = 0;//点击次数

    Coroutine cor;
    float curTime = 0;
    float waitTime = 0.27f;

    float longPressTime = 0.7f;
    bool isLongPress = false;
    bool isUp = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        isUp = false;
        onclickNums++;
        if (curTime == 0)
        {
            cor = StartCoroutine(IE());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isUp = true;

        if (onclickNums == 2 && isLongPress == false)
        {
            //双击
            StopCoroutine(cor);
            onclickNums = 0;
            curTime = 0;
            onDoubleClick.Invoke();
            //print("双击");
        }
    }

    void Update()
    {
        if (isLongPress)
        {
            onLongPress.Invoke();
            if (isUp == true)
            {
                isLongPress = false;
                onclickNums = 0;
                curTime = 0;
                StopCoroutine(cor);
            }
        }
    }
}