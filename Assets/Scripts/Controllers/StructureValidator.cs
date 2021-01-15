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
    public int lineNumber = 1;
    public string errors = null;

    public void StructureValidation()
    {
        isValid = true;
        node = SinglyLinkedListController.instance.singlyLinkedList.GetFirstNode();
        lastNode = SinglyLinkedListController.instance.singlyLinkedList.GetLastNode();
        lineNumber = 1;
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
            Debug.Log("Hay errores lógicos en la línea");
        }
        //Debug.Log("Número de líneas: " + lineNumber);
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
                Debug.Log("La línea empieza de forma inválida");
            }
        }
    }
}

