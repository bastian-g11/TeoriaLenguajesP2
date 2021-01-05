using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINode : MonoBehaviour
{
    public Text txtClassType;
    public Text txtValue;
    public LineRenderer lineRenderer;
    public Node node;
    public Node nextNode;

    void Update()
    {
        SetUINode(node);
    }

    public void SetUINode(Node _node)
    {
        node = _node;
        if (_node.GetNextNode() != null) nextNode = _node.GetNextNode();
        if(_node.GetNextNode() != null) nextNode = _node.GetNextNode();
        txtClassType.text = _node.GetClassType();
        txtValue.text = _node.GetValue();
        lineRenderer.enabled = _node.GetNextNode() != null;
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, transform.position + Vector3.forward * 0.01f);
            lineRenderer.SetPosition(1, nextNode.uiNode.gameObject.transform.position + Vector3.forward * 0.01f);
        }   
    }

}
