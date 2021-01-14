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

    public Node node = null;
    public bool isValid = true;
    public bool hasValue;

    public void StructureValidation()
    {
        node = SinglyLinkedListController.instance.singlyLinkedList.GetFirstNode();
        S();
        
    }

    public void S()
    {
        isValid = true;
        hasValue = false;
        //Agregar condicional para tomar decisión de cuál escoger
        NtA();
        if (isValid)
            Debug.Log("SIRVIÓOOOOOO: " + node.GetValue());
        else
            Debug.Log("Fallóoooo");
    }


    public void NtA()
    {
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "TipoDato":
            case "Variable":
                TipoDato();
                Variable();
                Separador();
                Valor();
                Fin();
                return;

            default:
                //Poner los errores
                isValid = false;
                Debug.Log("Falló en A con: " + node.GetValue());
                break;
        }
    }

    public void TipoDato()
    {
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "TipoDato":
                node = node.GetNextNode();
                return;

            case "Variable":
                return;

            default:
                //Poner los errores
                isValid = false;
                Debug.Log("Falló en TipoDato con: "+ node.GetValue());
                break;
        }
    }

    public void Variable()
    {
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Variable":
                node = node.GetNextNode();
                return;

            default:
                //Poner los errores
                isValid = false;
                Debug.Log("Falló en Variable con: " + node.GetValue());
                break;
        }
    }

    public void Valor() {
        Debug.Log("Entró a valor con: " + node.GetValue());
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Delimitador":
            case "Numero":
            case "Variable":
            case "Boolean":
            case "Operador":
            case "Separador":
                T();
                ListaE();
                return;

            case "FinSecuencia":
                Debug.Log("Fin de secuencia en Valor");
                T();
                ListaE();
                return;

            default:

                //Poner los errores
                isValid = false;
                Debug.Log("Falló en Valor con: "+ node.GetValue());
                break;
        }
    }

    public void ListaE() {
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Boolean":
            case "Operador":
                node = node.GetNextNode();
                T();
                ListaE();
                return;

            case "Separador":
                return;

            case "Delimitador":
                if (node != null && node.GetValue() == ")")
                    return;
                isValid = false;
                return;

            case "FinSecuencia":
                Debug.Log("Fin de secuencia en ListaE");
                return;

            default:
                
                //Poner los errores
                isValid = false;
                Debug.Log("Falló en ListaE con: " + node.GetValue());
                break;
        }
    }

    public void T() {
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Delimitador":
            case "Numero":
            case "Variable":
            case "Boolean":
            case "Operador":
            case "Separador":
                P();
                return;

            case "FinSecuencia":
                Debug.Log("Fin de secuencia en T");
                P();
                return;

            default:
                if (node == null)
                    return;
                //Poner los errores
                isValid = false;
                Debug.Log("Falló en T con: " + node.GetValue());
                break;
        }
    }

    public void P() {
        if (node.GetValue() != "¬")
            hasValue = true;
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Delimitador":
                if(node != null && node.GetValue() == "(")
                {
                    node = node.GetNextNode();
                    Debug.Log("Abre paréntesis");
                    Valor();
                    if(node != null && node.GetValue() == ")")
                    {
                        node = node.GetNextNode();
                        return;
                    }
                }
                
                else if(node != null && node.GetValue() == ")")
                {
                    return;
                }

                //poner los errores
                isValid = false;
                return;

            case "Variable":
                Variable();
                return;

            case "Numero":
                node = node.GetNextNode();
                return; 

            case "Boolean":
                if(node != null && (node.GetValue() == "true" || node.GetValue() == "false"))
                {
                    node = node.GetNextNode();
                    return;
                }
                return;

            case "Operador":
            case "Separador":
                return;

            case "FinSecuencia":
                Debug.Log("Fin de Secuencia en P");
                return;

            default:

                //Poner los errores
                isValid = false;
                Debug.Log("Falló en P con: " + node.GetValue());
                break;
        }
    }

    public void Separador() {
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Separador":
                node = node.GetNextNode();
                //StructureValidation();
                return;

            case "Operador":
                if(node != null && node.GetValue() == "=")
                {
                    node = node.GetNextNode();
                    return;
                }
                //Poner los errores
                isValid = false;
                return;

            default:
                //Poner los errores
                isValid = false;
                Debug.Log("Falló en Separador con: "+node.GetValue());
                break;
        }
    }
    
    public void Fin() {
        Debug.Log("ENTRÓ A FIN CON: " + node.GetValue());
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Separador":
                node = node.GetNextNode();
                //StructureValidation();
                return;

            case "FinSecuencia":
                Debug.Log("Fin de secuencia en Fin");
               if(hasValue)
                    isValid = false;
                return;

            default:
                //Poner los errores
                isValid = false;
                if (node != null)
                    Debug.Log("Falló en Fin con: " + node.GetValue());
                else
                    Debug.Log("Falló en Fin");
                break;
        }
    }
}

