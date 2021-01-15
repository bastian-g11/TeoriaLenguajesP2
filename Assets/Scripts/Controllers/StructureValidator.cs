using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureValidator : MonoBehaviour
{
    #region singleton
    public static StructureValidator instance;
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

    public Node node, lastNode = null;
    public bool isValid = true;
    public bool hasValue;
    public bool noVar = false;
    public bool hasDT = false;

    public void StructureValidation()
    {
        isValid = true;
        node = SinglyLinkedListController.instance.singlyLinkedList.GetFirstNode();
        lastNode = SinglyLinkedListController.instance.singlyLinkedList.GetLastNode();

        S();
        if (isValid)
        {
            if (node != null)
                Debug.Log("SIRVIÓOOOOOO: " + node.GetValue());
            else
                Debug.Log("SIRVIÓOOOOOO");
        }
        else
        {
            Debug.Log("Fallóoooo");
        }
    }

    public void S()
    {
        hasValue = false;
        hasDT = false;
        noVar = false;
        Debug.Log("Volvió a S con: " + node.GetValue());
        while (node != null && node.GetValue() == "¬" && node != lastNode)
        {
            node = node.GetNextNode();
        }

        if (node != null)
        {
            if (node.GetClassType() == "TipoDato" || node.GetClassType() == "Variable")
            {
                TextReader.instance.varGram.NtA();
            }
            else if (node.GetClassType() == "KeyWord")
            {
                TextReader.instance.cycGram.NtB();
            }
            else
            {
                Debug.Log("La línea empieza de forma inválida");
            }
        }
    }
}

