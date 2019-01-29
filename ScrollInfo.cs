using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleUIFrame;
using Random = UnityEngine.Random;
using Control;

public class ScrollInfo : MonoBehaviour
{
    public static ScrollInfo instance;

    public ScrollRect scrollRect;
    public Transform grid;

    public float itemH = 30.45f;   //Item Hight
    public float itemSpaceY = 0;   //Item 间距
    public int maxNums = 100;
    public int middleNums = 50;
    public int curNums = 0;
    public int onceLoadNums = 5;
    
    public float ld = 300f;

    Coroutine loadStartItemCor;
    Coroutine loadEndItemCor;

    void Awake()
    {
        instance = this;
    }

    void Start()
	{
        scrollRect.onValueChanged.AddListener((value) =>
        {
            if (scrollRect.normalizedPosition.y >= 1 && scrollRect.velocity.y < -ld)//顶部
            {
                if (loadStartItemCor == null)
                {
                    loadStartItemCor = StartCoroutine(StartLoadStartItems());
                }
            }
            else if (scrollRect.normalizedPosition.y <= 0 && scrollRect.velocity.y > ld)//底部
            {
                if (loadEndItemCor == null)
                {
                    loadEndItemCor = StartCoroutine(StartLoadEndItems());
                }
            }
        });
    }
	
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendOneItemEnd();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (grid.childCount > 0)
            {
                RecycleItem(grid.GetChild(0).gameObject);
                RectTransform rect = grid.GetComponent<RectTransform>();
                float x = rect.anchoredPosition.x;
                float y = rect.anchoredPosition.y - itemH - itemSpaceY;
                rect.anchoredPosition = new Vector2(x, y);
            }
        }
    }

    IEnumerator StartLoadStartItems()
    {
        while (true)
        {
            if (scrollRect.velocity.y == 0 && Math.Abs(scrollRect.normalizedPosition.y - 1) <= 0.01f)
            {
                for (int i = 0; i < onceLoadNums; i++)
                {
                    SendOneItemStart();
                }
                if (loadStartItemCor != null)
                {
                    StopCoroutine(loadStartItemCor);
                    loadStartItemCor = null;
                }
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator StartLoadEndItems()
    {
        while (true)
        {
            if (scrollRect.velocity.y == 0)
            {
                for (int i = 0; i < onceLoadNums; i++)
                {
                    SendOneItemEnd();
                }
                Cutter();

                if (loadEndItemCor != null)
                {
                    StopCoroutine(loadEndItemCor);
                    loadEndItemCor = null;
                }
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator WaitTimeLoad()
    {
        yield return new WaitForSeconds(waitTime);
        canLoad = true;
    }

    void SendOneItemStart()
    {
        GameObject go = PoolMgr.instance.GetGo(PoolGoType.Item_SSBJ);
        go.transform.SetParent(grid);
        go.transform.localScale = Vector3.one;
        go.transform.SetAsFirstSibling();

        //RectTransform rect = grid.GetComponent<RectTransform>();
        //float x = rect.anchoredPosition.x;
        //float y = rect.anchoredPosition.y + itemH + itemSpaceY;
        //rect.anchoredPosition = new Vector2(x, y);

        go.GetComponent<Image>().color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        curNums++;
       
    }

    public void SendOneItemEnd(Ctrl_View_DG_History_Data data)
    {
        GameObject go = PoolMgr.instance.GetGo(PoolGoType.Item_SSBJ);
        go.transform.SetParent(grid);
        go.transform.localScale = Vector3.one;
        go.transform.SetAsLastSibling();

        go.GetComponent<Image>().color = new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        curNums++;
    }

    public void Clear()
    {
        int count = grid.childCount;
        for (int i = 0; i < count; i++)
        {
            RecycleItem(grid.GetChild(0).gameObject);
        }
        curNums = 0;
    }
    
    void RecycleItem(GameObject go)
    {
        PoolMgr.instance.RecycleGo(PoolGoType.Item_SSBJ, go);
    }

    void Cutter()
    {
        if (curNums >= maxNums)
        {
            int delNums = curNums - middleNums;//裁剪数量
            for (int i = 0; i < delNums; i++)
            {
                RecycleItem(grid.GetChild(0).gameObject);
            }
            curNums = grid.childCount;
        }
    }
}
