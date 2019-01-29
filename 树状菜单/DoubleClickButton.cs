using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoubleClickButton : Button
{
    protected DoubleClickButton()
    {
        m_onDoubleClick = new ButtonClickedEvent();
        m_onLongPress = new ButtonClickedEvent();
    }

    private ButtonClickedEvent m_onLongPress;
    public ButtonClickedEvent OnLongPress
    {
        get { return m_onLongPress; }
        set { m_onLongPress = value; }
    }

    private ButtonClickedEvent m_onDoubleClick;
    public ButtonClickedEvent onDoubleClick
    {
        get { return m_onDoubleClick; }
        set { m_onDoubleClick = value; }
    }

    private bool m_isStartPress = false;

    private float m_curPointDownTime = 0f;

    private float m_longPressTime = 1f;

    private bool m_longPressTrigger = false;

    void Update()
    {
        if (m_isStartPress && !m_longPressTrigger)
        {
            if (Time.time > m_curPointDownTime + m_longPressTime)
            {
                m_longPressTrigger = true;
                m_isStartPress = false;
                if (m_onLongPress != null)
                {
                    m_onLongPress.Invoke();
                }
            }
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        m_curPointDownTime = Time.time;
        m_isStartPress = true;
        m_longPressTrigger = false;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        m_isStartPress = false;
        m_longPressTrigger = false;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        m_isStartPress = false;
        m_longPressTrigger = false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //base.OnPointerClick(eventData);
        if (!m_longPressTrigger)
        {
            if (curTime == 0)
            {
                cor = StartCoroutine(IE());
            }
            else
            {
                //print("双击");
                m_onDoubleClick.Invoke();
                curTime = 0;
                StopCoroutine(cor);
                cor = null;
            }
        }
    }

    IEnumerator IE()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            curTime += Time.deltaTime;
            if (curTime >= waitTime)
            {
                //print("单击");
                curTime = 0;
                onClick.Invoke();
                StopCoroutine(cor);
                cor = null;
                break;
            }
        }
    }

    Coroutine cor;
    float curTime = 0;
    float waitTime = 0.25f;
}
