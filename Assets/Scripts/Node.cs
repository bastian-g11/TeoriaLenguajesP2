using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node 
{
    public string classType;
    public string value;
    public Node nextNode;
    public UINode uiNode;

    public Node(string _classType, string _value)
    {
        classType = _classType;
        value = _value;
        nextNode = null;
        uiNode = null;
    }

    public string GetClassType()
    {
        return classType;
    }

    public string GetValue()
    {
        return value;
    }

    public Node GetNextNode()
    {
        return nextNode;
    }

    public UINode GetUINode()
    {
        return uiNode;
    }

    public void SetUINode(UINode _uiNode)
    {
        uiNode = _uiNode;
    }

    public void SetClassType(string _classType)
    {
        classType = _classType;
    }

    public void SetValue(string _value)
    {
        value = _value;
    }

    public void SetNextNode(Node _nextNode)
    {
        nextNode = _nextNode;
    }
}
