using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleGrammar
{
    public Node node;
    public Node lastNode;
    public bool isValid = true;


    public void NtB()
    {
        node = StructureValidator.instance.node;
        lastNode = StructureValidator.instance.lastNode;
        isValid = StructureValidator.instance.isValid;


        Debug.Log("Entró a B con: " + node.GetValue());

        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "KeyWord":
                PalabraReservada();
                Delimitador();

                StructureValidator.instance.node = node;
                StructureValidator.instance.isValid = isValid;
                return;

            default:
                //Poner los errores
                isValid = false;
                StructureValidator.instance.node = node;
                StructureValidator.instance.isValid = isValid;

                Debug.Log("Falló en B con: " + node.GetValue());
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
                Debug.Log("Falló en B Variable con: " + node.GetValue());
                break;
        }
    }

    public void Valor()
    {
        Debug.Log("Entró a B valor con: " + node.GetValue());
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Delimitador":
                if (node != null && (node.GetValue() == "\"" 
                    || node.GetValue() == "\'" || node.GetValue() == "("))
                {
                    T();
                    ListaE();
                    return;

                }

                isValid = false;
                Debug.Log("Falló en B Variable con: " + node.GetValue());
                return;

            case "Numero":
                T();
                ListaE();
                return;
            case "Variable":
                T();
                ListaE();
                return;

            case "Boolean":
                if (node != null && (node.GetValue() == "true" || node.GetValue() == "false"))
                {
                    node = node.GetNextNode();
                    return;
                }
                isValid = false;
                return;

            default:
                //Poner los errores
                isValid = false;
                Debug.Log("Falló en B Variable con: " + node.GetValue());
                break;
        }
    }

    public void ListaE()
    {
        Debug.Log("Entró a ListaE de B con: " + node.GetValue());
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
                Debug.Log("Fin de secuencia B en ListaE");
                return;

            default:

                //Poner los errores
                isValid = false;
                Debug.Log("Falló en ListaE de B con: " + node.GetValue());
                break;
        }
    }

    public void T()
    {
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Delimitador":
                if (node != null && (node.GetValue() == "\""
                    || node.GetValue() == "\'" || node.GetValue() == "("))
                {
                    P();
                    return;

                }

                isValid = false;
                Debug.Log("Falló en Variable con: " + node.GetValue());
                return;

            case "Numero":
                P();
                ListaE();
                return;

            case "Variable":
                P();
                ListaE();
                return;

            case "Boolean":
                if (node != null && (node.GetValue() == "true" || node.GetValue() == "false"))
                {
                    P();
                    return;
                }
                isValid = false;
                return;

            default:
                //Poner los errores
                isValid = false;
                Debug.Log("Falló en B Variable con: " + node.GetValue());
                break;
        }

    }

    public void P()
    {
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Delimitador":
                if (node != null && node.GetValue() == "(")
                {
                    node = node.GetNextNode();
                    Debug.Log("Abre paréntesis en B");
                    Valor();
                    if (node != null && node.GetValue() == ")")
                    {
                        node = node.GetNextNode();
                        return;
                    }
                }

                //Caso Comillas 
                else if (node != null && node.GetValue() == "\"")
                {
                    node = node.GetNextNode();
                    while (node != null && node.GetClassType() == "Termino" && node != lastNode)
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
                    while (node != null && node.GetClassType() == "Termino" && node != lastNode)
                        node = node.GetNextNode();
                    if (node != null && node.GetValue() == "\'")
                    {
                        node = node.GetNextNode();

                        return;
                    }
                }

                //poner los errores
                Debug.Log("Falló en P en B, falta cerrar comillas, apóstrofe o paréntesis");
                isValid = false;
                return;

            case "Variable":
                Variable();
                return;

            case "Numero":
                node = node.GetNextNode();
                return;

            case "Boolean":
                if (node != null && (node.GetValue() == "true" || node.GetValue() == "false"))
                {
                    node = node.GetNextNode();
                    return;
                }
                isValid = false;
                return;

            default:
                //Poner los errores
                isValid = false;
                Debug.Log("Falló en P con: " + node.GetValue());
                break;
        }
    }

    public void Delimitador()
    {
        Debug.Log("Entró a Delimitador con: " + node.GetValue());
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Delimitador":
                if (node != null && node.GetValue() == "(")
                {
                    node = node.GetNextNode();
                    Debug.Log("Abre paréntesis en B Delimitador");
                    Valor();
                    if (node != null && node.GetValue() == ")")
                    {
                        node = node.GetNextNode();
                        return;
                    }
                    //poner los errores
                    Debug.Log("Falló en Delimitador B, falta cerrar un Paréntesis");
                    isValid = false;
                    return;
                }

                else if (node != null && node.GetValue() == "{")
                {
                    node = node.GetNextNode();
                    StructureValidator.instance.node = node;
                    StructureValidator.instance.S();
                    Debug.Log("Abre Llave en B Delimitador");
                    Valor();
                    if (node != null && node.GetValue() == "}")
                    {
                        node = node.GetNextNode();
                        return;
                    }
                    //poner los errores
                    Debug.Log("Falló en Delimitador B, falta cerrar una LLave");
                    isValid = false;
                    return;
                }

                //poner los errores
                Debug.Log("Faltan llaves o Paréntesis");
                isValid = false;
                return;

            default:

                //Poner los errores
                isValid = false;
                Debug.Log("Falló en P en B con: " + node.GetValue());
                break;

        }
    }

    public void PalabraReservada()
    {
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "KeyWord":

                if (node != null && (node.GetValue() == "while" || node.GetValue() == "if"))
                {
                    node = node.GetNextNode();
                    Delimitador();
                    return;
                }
                else if(node != null && node.GetValue() == "else")
                {
                    node = node.GetNextNode();
                    return;
                }

                //poner los errores
                isValid = false;
                return;

            default:
                //Poner los errores
                isValid = false;
                Debug.Log("Falló en B Separador con: " + node.GetValue() + " no hay punto y coma ni igual ó dos o más variables seguidas");
                break;
        }
    }
}
