using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class MButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onClick = new UnityEvent();
    public UnityEvent onDoubleClick = new UnityEvent();
    public UnityEvent onLongPress = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

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

            if (onclickNums == 1 && curTime > waitTime && isUp == true)
            {
                //单击
                onclickNums = 0;
                curTime = 0;
                onClick.Invoke();
                break;
            }
            else if (onclickNums == 2 && curTime < waitTime && isUp == true)
            {
                //双击
                onclickNums = 0;
                curTime = 0;
                onDoubleClick.Invoke();
                break;
            }
            else if (onclickNums == 1 && curTime > longPressTime)
            {
                isLongPress = true;
                break;
            }
        }
    }

    public int onclickNums = 0;//点击次数

    Coroutine cor;
    float curTime = 0;
    float waitTime = 0.25f;

    float longPressTime = 0.7f;
    public bool isLongPress = false;
    public bool isUp = true;
    
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