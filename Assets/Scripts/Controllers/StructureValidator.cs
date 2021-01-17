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
    public bool isBalanced = true;
    public bool hasStrangeSymbol = false;
    public int lineNumber;
    public string errors = null;

    public void StructureValidation()
    {
        isValid = true;
        isBalanced = true;
        hasStrangeSymbol = false;
        errors = null;
        node = SinglyLinkedListController.instance.singlyLinkedList.GetFirstNode();
        lastNode = SinglyLinkedListController.instance.singlyLinkedList.GetLastNode();
        lineNumber = 0;
        S();

        if (errors != null && !hasStrangeSymbol)
        {
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
            UIController.instance.SetErrorText(lineNumber, "ESTRUCTURALES");
            UIController.instance.SetBalancedMessage(isBalanced);
        }
        if(hasStrangeSymbol)
        {
            errors = null;
            errors = StructureValidator.instance.errors 
                 + " Delimitador inválido\n" + " Fin de lectura";
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
            UIController.instance.SetErrorText(lineNumber, "ESTRUCTURALES");
        }
        
    }

    public void S()
    {
        hasValue = false;
        hasDT = false;
        noVar = false;
        


        while (node != null && node.GetValue() == "¬" && node != lastNode)
        {
            node = node.GetNextNode();
            lineNumber++;
        }



        if(node != null && node.GetValue() == "¬" && node == lastNode)
        {
            lineNumber++;
        }


        if (node != null)
        {
            if (node.GetClassType() == "TipoDato" || node.GetClassType() == "Variable")
            {
                TextReader.instance.varGram.NtA();
                if (node != lastNode)
                {
                    S();
                }
            }
            else if (node.GetClassType() == "KeyWord")
            {
                TextReader.instance.cycGram.NtB();
                if(node != lastNode)
                {
                    S();
                }
            }
            else if(node != lastNode)
            {
                Debug.Log("Aún no acaba");
            }
        }
    }
}

