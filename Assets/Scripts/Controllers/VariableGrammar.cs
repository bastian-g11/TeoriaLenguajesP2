using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableGrammar
{
    public Node node;
    public Node lastNode;
    public bool isValid = true;
    public bool hasValue;
    public bool noVar = false;
    public bool hasDT = false;

    public void NtA()
    {
        node = StructureValidator.instance.node;
        lastNode = StructureValidator.instance.lastNode;
        isValid = StructureValidator.instance.isValid;
        hasValue = StructureValidator.instance.hasValue;
        noVar = StructureValidator.instance.noVar;
        hasDT = StructureValidator.instance.hasDT;

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

                StructureValidator.instance.node = node;
                StructureValidator.instance.isValid = isValid;
                return;

            case "FinSecuencia":
                return;

            default:
                //Poner los errores
                isValid = false;
                StructureValidator.instance.node = node;
                StructureValidator.instance.isValid = isValid;
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
                Debug.Log("Falló en TipoDato con: " + node.GetValue());
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

    public void Valor()
    {
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

            //SE AGREGÓ PARA RECONOCER ERRORES
            case "KeyWord":
                Debug.Log("Nombre de variable es palabra reservada");
                T();
                ListaE();
                return;

            default:

                //Poner los errores
                isValid = false;
                Debug.Log("Falló en Valor con: " + node.GetValue());
                break;
        }
    }

    public void ListaE()
    {
        Debug.Log("Entró a ListaE de A con: " + node.GetValue());
        //Avanzar hasta que no encuentre fin de secuencia
        while (node != null && node.GetValue() == "¬" && node != lastNode)
        {
            node = node.GetNextNode();
            StructureValidator.instance.lineNumber++;
        }
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Boolean":
            case "Operador":
                if (noVar)
                {
                    //isValid = false;
                    Debug.Log("Falló en ListaE de A, dos operadores seguidos");
                }
                node = node.GetNextNode();

                //Avanzar hasta que no encuentre fin de secuencia
                while (node != null && node.GetValue() == "¬" && node != lastNode)
                {
                    node = node.GetNextNode();
                    StructureValidator.instance.lineNumber++;
                }
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

            //SE AGREGÓ PARA MOSTRAR ERRORES
            case "KeyWord":
                return;
                //

            default:

                //Poner los errores
                isValid = false;
                Debug.Log("Falló en ListaE con: " + node.GetValue());
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
            //SE AGREGÓ PARA MOSTRAR ERRORES
            case "KeyWord":
                //
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

    public void P()
    {

        if (node.GetValue() != "¬")
            hasValue = true;

        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Delimitador":
                if (node != null && node.GetValue() == "(")
                {
                    node = node.GetNextNode();
                    Debug.Log("Abre paréntesis en A");
                    Valor();
                    if (node != null && node.GetValue() == ")")
                    {
                        node = node.GetNextNode();
                        return;
                    }
                }

                else if (node != null && node.GetValue() == ")")
                {
                    return;
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
                if (node != null && (node.GetValue() == "true" || node.GetValue() == "false"))
                {
                    node = node.GetNextNode();
                    return;
                }
                isValid = false;
                return;

            case "Operador":
            case "Separador":
                //noVar implicar que <P> se reemplazó por lambda y entonces habrían 2 operadores seguidos
                noVar = true;
                return;

            case "FinSecuencia":
                Debug.Log("Fin de Secuencia en P");
                return;

             //SE AGREGÓ PARA MOSTRAR ERRORES
            case "KeyWord":
                //Se ignora
                node = node.GetNextNode();
                return;
                //
            default:

                //Poner los errores
                isValid = false;
                Debug.Log("Falló en P con: " + node.GetValue());
                break;
        }
    }

    public void Separador()
    {
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
                if (node != null && node.GetValue() == "=")
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
                Debug.Log("Falló en Separador con: " + node.GetValue() + " no hay punto y coma ni igual ó dos o más variables seguidas");
                break;
        }
    }

    public void Fin()
    {
        Debug.Log("ENTRÓ A FIN CON: " + node.GetValue());
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Separador":
                node = node.GetNextNode();
                StructureValidator.instance.node = node;
                StructureValidator.instance.S();
                node = StructureValidator.instance.node;
                return;

            case "FinSecuencia":
                Debug.Log("Fin de secuencia en Fin");
                if (hasValue)
                {
                    isValid = false;
                    Debug.Log("Falló en Fin, no hay punto y coma");
                    StructureValidator.instance.errors =
                        StructureValidator.instance.errors + "Falta punto y coma";

                    //node = node.GetNextNode();
                    return;
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
