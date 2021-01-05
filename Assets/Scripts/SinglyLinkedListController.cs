using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglyLinkedListController : MonoBehaviour
{
    public SinglyLinkedList singlyLinkedList = null;

    #region singleton
    public static SinglyLinkedListController instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    #endregion
    public void CreateSinglyLinkedList()
    {
        singlyLinkedList = new SinglyLinkedList();
    }

    public void AddNode(string _dataType, string _value)
    {
        Node y = singlyLinkedList.GetLastNode();
        singlyLinkedList.InsertNode(_dataType, _value, y);
    }

    public void ResetSinglyLinkedList()
    {
        singlyLinkedList = null;
    }
}
