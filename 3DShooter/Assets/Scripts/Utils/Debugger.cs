using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Debugger
{
    public static void DebugLog(string message, string color)
    {
        Debug.Log($"<color={color}>{message}</color>");
    }
}

public static class DebuggerConsts
{
    public const string Red = "red";
    public const string Green = "green";
    public const string Yellow = "yellow";
}
