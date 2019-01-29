using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

public class Ctrl_Test : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform grid;
    //private float tierSpaceX = 30f;//层级菜单X间距

    public List<Transform> arrayGos;

    void Start()
    {
        arrayGos = new List<Transform>();

        List<DataMenuCell> datas = LoadJsonMenuData();
        ShowMenuData(datas);

        
        foreach (var item in arrayGos)
        {
            item.gameObject.SetActive(false);
            if (item.GetComponent<Test_SetRectTranSize>())
            {
                item.GetComponent<Test_SetRectTranSize>().SetLayout();

                if (item.childCount >= 1)
                {
                    item.GetComponent<Test_SetRectTranSize>().listMargins = new List<Transform>();
                    item.GetComponent<Test_SetRectTranSize>().listMargins.Add(item.GetChild(0));
                    item.GetComponent<Test_SetRectTranSize>().listMargins.Add(item.GetChild(item.childCount - 1));
                    item.GetComponent<Test_SetRectTranSize>().SetSize();
                }
            }
        }

        Transform arrayParent = grid.Find("Array");
        //arrayParent.GetComponent<Test_SetRectTranSize>().SetLayout();
        //Ctrl_DoubleButton_ChangeLayout.ArraySetSize(arrayParent);
        arrayParent.gameObject.SetActive(true);
        for (int i = 0; i < arrayParent.childCount; i++)
        {
            arrayParent.GetChild(i).gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
           
        }
    }

    List<DataMenuCell> LoadJsonMenuData()
    {
        string FilePath = Application.dataPath + "/TestJson.json";
        string josnString = File.ReadAllText(FilePath, Encoding.Default);
        //JObject jo = JObject.Parse(josnString);   //对象
        JArray jArray = JArray.Parse(josnString); //数组

        List<DataMenuCell> list = new List<DataMenuCell>();
        foreach (var item in jArray)
        {
            list.Add(item.ToObject<DataMenuCell>());
        }

        return list;
    }

    void ShowMenuData(List<DataMenuCell>  datas)
    {
        foreach (var item in datas)
        {
            SetOne(grid, item);
        }

        //SetOne(grid, datas[0]);
    }

    void SetOne(Transform parent, DataMenuCell data)
    {
        GameObject clone = Instantiate(itemPrefab);
        clone.gameObject.SetActive(true);
        clone.name = "Item";
        clone.transform.localScale = Vector3.one;
        Transform arrayGo = parent.Find("Array");
        arrayGos.Add(arrayGo);//布局
        clone.transform.SetParent(arrayGo);
        

        clone.transform.localPosition = Vector3.zero;
        clone.transform.localScale = Vector3.one;

        clone.transform.Find("This/Text").GetComponent<Text>().text = data.name;

        foreach (var item in data.array)
        {
            SetOne(clone.transform, item);
        }
    }

    void Func(JToken jarr)
    {
        foreach (var item in jarr)
        {
            print(item["Name"]);

            Func(item["Array"]);
        }
    }
}

public class DataMenuCell
{
    public string name;
    public List<DataMenuCell> array;

    public DataMenuCell()
    {

    }
}
