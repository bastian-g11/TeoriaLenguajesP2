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

        //Agregar condicional para tomar decisión de cuál escoger
        if (node != null)
            NtA();
    }

    public void NtA()
    {
        Debug.Log("Entró a A con: " + node.GetValue());
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

            case "FinSecuencia":
                return;

            default:
                //Poner los errores
                isValid = false;
                if (node != null)
                    Debug.Log("Falló en A con: " + node.GetValue());
                else
                    Debug.Log("Falló en A");
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
                hasDT = true;
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
        Debug.Log("Entró a ListaE con: " + node.GetValue());
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Boolean":
            case "Operador":
                if(noVar)
                {
                    isValid = false;
                    Debug.Log("Falló en ListaE, dos operadores seguidos");
                }
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

                //Caso Comillas
                else if(node != null && node.GetValue() == "\"")
                {
                    node = node.GetNextNode();
                    if (node.GetClassType() == "Termino")
                        node = node.GetNextNode();
                    if (node != null && node.GetValue() == "\"")
                    {
                        node = node.GetNextNode();
                        return;
                    }
                }

                //Caso Apóstrofe
                else if (node != null && node.GetValue() == "\'")
                {
                    node = node.GetNextNode();
                    if (node.GetClassType() == "Termino")
                        node = node.GetNextNode();
                    if (node != null && node.GetValue() == "\'")
                    {
                        node = node.GetNextNode();
                        
                        return;
                    }
                }

                //poner los errores
                Debug.Log("Falló en P, falta cerrar comillas, apóstrofe o paréntesis");
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
                //noVar implicar que <P> se reemplazó por lambda y entonces habrían 2 operadores seguidos
                noVar = true;
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
                if (!hasDT)
                {
                    isValid = false;
                    Debug.Log("Falló en separador, falta inicializar variable, NO hay tipo de dato");
                }
                node = node.GetNextNode();
                return;

            case "Operador":
                if(node != null && node.GetValue() == "=")
                {
                    node = node.GetNextNode();
                    return;
                }
                //Poner los errores

                Debug.Log("Falló en Separador, operador no válido después de variable");
                isValid = false;
                return;

            default:
                //Poner los errores
                isValid = false;
                Debug.Log("Falló en Separador con: "+node.GetValue() + " no hay punto y coma ni igual ó dos o más variables seguidas");
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
                S();
                return;

            case "FinSecuencia":
               Debug.Log("Fin de secuencia en Fin");
               if(hasValue)
                {
                    isValid = false;
                    Debug.Log("Falló en Fin, no hay punto y coma");
                }
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

