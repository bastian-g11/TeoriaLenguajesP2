using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableGrammar
{
    public Node node;
    public Node lastNode;
    public bool hasValue;
    public bool noVar = false;
    public bool hasDT = false;

    public void NtA()
    {
        node = StructureValidator.instance.node;
        lastNode = StructureValidator.instance.lastNode;
        hasValue = StructureValidator.instance.hasValue;
        noVar = StructureValidator.instance.noVar;
        hasDT = StructureValidator.instance.hasDT;

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
                return;

            case "FinSecuencia":
                return;

            default:
                //Poner los errores
                StructureValidator.instance.node = node;

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

            case "KeyWord":
                //Se ignora
                return;

            default:
                break;
        }
    }

    public void Valor()
    {
 
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
                T();
                ListaE();
                return;

            //SE AGREGÓ PARA RECONOCER ERRORES
            case "KeyWord":
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Nombre de variable es palabra reservada \n";
                T();
                ListaE();
                return;

            default:

                //Poner los errores
                break;
        }
    }

    public void ListaE()
    {

        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Boolean":
            case "Operador":
                if (noVar)
                {
                    StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Dos o más operadores seguidos \n";
                }
                node = node.GetNextNode();

                T();
                ListaE();
                return;

            case "Separador":
                if(node.GetValue() != ";")
                {
                    StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Operador erróneo\n";
                    node = node.GetNextNode();

                    T();
                    ListaE();
                }
                return;

            case "Delimitador":
                if (node != null && node.GetValue() == ")")
                    return;
                StructureValidator.instance.errors = StructureValidator.instance.errors
                   + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Error en Delimitador, falta operador antes de delimitador\n";
                return;

            case "FinSecuencia":
                return;

            //SE AGREGÓ PARA MOSTRAR ERRORES
            case "KeyWord":
                return;
                //

            default:

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
                P();
                return;

            default:
                if (node == null)
                    return;
                //Poner los errores
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

                if(node != null && (node.GetValue() == "{" ||
                    node.GetValue() == "}" || node.GetValue() == "[" ||
                    node.GetValue() == "]" ))
                {
                    StructureValidator.instance.errors = StructureValidator.instance.errors
                   + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Delimitador no válido\n";
                    while (node != null && node.GetValue() != "¬")
                    {
                        node = node.GetNextNode();
                    }
                    StructureValidator.instance.lineNumber++;
                    node = node.GetNextNode();
                    StructureValidator.instance.node = node;
                    StructureValidator.instance.S();
                    node = StructureValidator.instance.node;
                    return;
                }

                //poner los errores
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Falta cerrar comillas, apóstrofe o paréntesis\n";
                StructureValidator.instance.isBalanced = false;
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
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Operador booleano ubicado de manera errónea\n";
                return;

            case "Operador":
            case "Separador":
                noVar = true;
                return;

            case "FinSecuencia":
                return;

            case "KeyWord":
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Nombre de variable es palabra reservada\n";
                node = node.GetNextNode();
                return;
            default:

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
                    StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Falta tipo de dato en variable o hay un operador ubicado de manera errónea\n";
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

                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Operador no válido después de variable\n";

                return;

            case "FinSecuencia":
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Falta punto y coma\n";
                return;

            default:
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea "+(StructureValidator.instance.lineNumber + 1).ToString() + "</b>:Error en declaración de variable\n";
                break;
        }
    }

    public void Fin()
    {
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
                if (hasValue)
                {
                    StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea "+ (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Falta punto y coma\n";
                }
                StructureValidator.instance.lineNumber++;
                node = node.GetNextNode();
                StructureValidator.instance.node = node;
                StructureValidator.instance.S();
                node = StructureValidator.instance.node;
                return;


            case "Delimitador":
                if(node.GetValue() == ")")
                {
                    StructureValidator.instance.errors = StructureValidator.instance.errors
                   + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Falta abrir paréntesis\n";
                    StructureValidator.instance.isBalanced = false;
                    while (node != null && node.GetValue() != "¬")
                    {
                        node = node.GetNextNode();
                    }
                    StructureValidator.instance.lineNumber++;
                    node = node.GetNextNode();
                    StructureValidator.instance.node = node;
                    StructureValidator.instance.S();
                    node = StructureValidator.instance.node;
                }
                
                else if (node.GetValue() == "\"" || node.GetValue() == "(" || node.GetValue() == "\'"
                    || node.GetValue() == "{" || node.GetValue() == "}" || node.GetValue() == "[" || node.GetValue() == "]")
                {
                    while (node != null && node.GetValue() != "¬")
                    {
                        node = node.GetNextNode();
                    }
                    StructureValidator.instance.lineNumber++;
                    node = node.GetNextNode();
                    StructureValidator.instance.node = node;
                    StructureValidator.instance.S();
                    node = StructureValidator.instance.node;
                }
                return;

            default:
                //Poner los errores

                if(node!=null)
                    node = node.GetNextNode();
                StructureValidator.instance.node = node;
                StructureValidator.instance.S();
                node = StructureValidator.instance.node;
                break;
        }
    }
}
