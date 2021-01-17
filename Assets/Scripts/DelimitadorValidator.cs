using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelimitadorValidator : MonoBehaviour
{
    #region singleton
    public static DelimitadorValidator instance;
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
    public int parentesisIzq = 0;
    public int parentesisDer = 0;
    public int llaveIzq = 0;
    public int llaveDer = 0;
    public int corcheteIzq = 0;
    public int corcheteDer = 0;
    public bool balanceado = true;


    private void Start()
    {
    parentesisIzq = 0;
    parentesisDer = 0;
    llaveIzq = 0;
    llaveDer = 0;
    corcheteIzq = 0;
    corcheteDer = 0;
    balanceado = true;
    }

    public void ValidateDelimitador()
    {
        Node node = SinglyLinkedListController.instance.singlyLinkedList.GetFirstNode();
        Node lastNode = SinglyLinkedListController.instance.singlyLinkedList.GetLastNode();
        Debug.Log("ESTO TIENE: " + node.GetValue());
        while (node != lastNode)
        {
            Debug.Log("Entró");
        }
            //    if(node.GetClassType() == "Delimitador")
            //    {
            //        //if (node.GetValue() == ")")
            //        //    parentesisIzq++;
            //        //else if (node.GetValue() == "(")
            //        //    parentesisDer++;
            //        //else if (node.GetValue() == "{")
            //        //    llaveIzq++;
            //        //else if (node.GetValue() == "}")
            //        //    llaveDer++;
            //        //else if (node.GetValue() == "[")
            //        //    corcheteIzq++;
            //        //else if (node.GetValue() == "]")
            //        //    corcheteDer++;

            //        node = node.GetNextNode();
            //        if (node == null)
            //            Debug.Log("Nuloooooooo");
            //    }
            //}

            //if (parentesisIzq != parentesisDer)
            //    balanceado = false;
            //else if (llaveIzq != llaveDer)
            //    balanceado = false;
            //else if (corcheteIzq != corcheteDer)
            //    balanceado = false;
            //return balanceado;

        }
}
