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
    public int lineNumber;
    public string errors = null;

    public void StructureValidation()
    {
        isValid = true;
        node = SinglyLinkedListController.instance.singlyLinkedList.GetFirstNode();
        lastNode = SinglyLinkedListController.instance.singlyLinkedList.GetLastNode();
        lineNumber = 0;
        S();
        if (node != null)
            Debug.Log("SIRVIÓOOOOOO: " + node.GetValue());

        Debug.Log("Número de líneas: " + lineNumber);
    }

    public void S()
    {
        hasValue = false;
        hasDT = false;
        noVar = false;
        if(node!=null)
            Debug.Log("Volvió a S con: " + node.GetValue());
        while (node != null && node.GetValue() == "¬" && node != lastNode)
        {
            node = node.GetNextNode();
            lineNumber++;
        }


        if (node != null)
        {
            if (node.GetClassType() == "TipoDato" || node.GetClassType() == "Variable")
            {
                TextReader.instance.varGram.NtA();
                //Debug.Log("Valor de errors: " + errors);
                //if (errors != null)
                //{
                //    Debug.Log("Entr+o");
                //    ErrorController.instance.SetErrorMessage(errors);
                //    ErrorController.instance.SetLineHasError(true);
                //    UIController.instance.SetErrorText(lineNumber);
                //    errors = null;
                //}
            }
            else if (node.GetClassType() == "KeyWord")
            {
                TextReader.instance.cycGram.NtB();
            }
            else if(node != lastNode)
            {
                Debug.Log("Aún no acaba");
            }
        }
    }
}

