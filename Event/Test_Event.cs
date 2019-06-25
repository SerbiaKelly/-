using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Event : MonoBehaviour
{
    void Start()
    {
        QEventSystem.RegisterEvent(UIEvent.A, Func0);
        QEventSystem.UnRegisterEvent(XXEvent.A, Func0);
        QEventSystem.SendEvent(UIEvent.A);
    }

    void Func0(int n, params object[] param)
    {
        print(n);
    }
}
