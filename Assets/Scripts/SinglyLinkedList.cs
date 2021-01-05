using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglyLinkedList
{
    private Node firstNode, lastNode;

    public SinglyLinkedList()
    {
        firstNode = null;
        lastNode = null;
    }

    public Node GetFirstNode()
    {
        return firstNode;
    }

    public Node GetLastNode()
    {
        return lastNode;
    }

    public void TraverseLinkedList()
    {
        Node node = firstNode;
        while(node != null)
        {
            node = node.GetNextNode();
        }
    }

    public void InsertNode(string _dataType, string _value, Node y)
    {
        Node node = new Node(_dataType, _value);

        ConnectNode(node, y);
    }

    public void ConnectNode(Node node, Node y)
    {
        if (y != null)
        {
            node.SetNextNode(y.GetNextNode());
            y.SetNextNode(node);
            if (y == lastNode)
            {
                lastNode = node;
            }
        }
        else
        {
            node.SetNextNode(firstNode);
            if (firstNode == null)
            {
                lastNode = node;
            }
            firstNode = node;
        }
        UIController.instance.createdNode = node;
    }
}
