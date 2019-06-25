using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsStartIndex
{
    public const int WSQ = 0;
    public const int HCQ = 3000;
}

public enum UIEvent
{
    A = EventsStartIndex.WSQ,
    B,
    C
}

public enum XXEvent
{
    A = EventsStartIndex.HCQ,
    B,
    C
}