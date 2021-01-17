using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleGrammar
{
    public Node node;
    public Node lastNode;


    public void NtB()
    {
        node = StructureValidator.instance.node;
        lastNode = StructureValidator.instance.lastNode;

        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "KeyWord":
                PalabraReservada();
                Delimitador();
                StructureValidator.instance.node = node;
                return;

            default:
                //Poner los errores
                StructureValidator.instance.node = node;

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
                if (node != null && (node.GetValue() == "\"" 
                    || node.GetValue() == "\'" || node.GetValue() == "("))
                {
                    T();
                    ListaE();
                    return;

                }
                if(node != null && (node.GetValue() == "{" || node.GetValue() == "}" ||
                    node.GetValue() == "[" || node.GetValue() == "]"))
                {
                    StructureValidator.instance.hasStrangeSymbol = true;
                }

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
                return;

            //SE AGREGÓ PARA RECONOCER ERRORES
            case "KeyWord":
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
        //Avanzar hasta que no encuentre fin de secuencia
        while (node != null && node.GetValue() == "¬" && node != lastNode)
        {
            node = node.GetNextNode();
        }
        string nodeType = null;
        if (node != null)
            nodeType = node.GetClassType();

        switch (nodeType)
        {
            case "Boolean":
                node = node.GetNextNode();
                //Avanzar hasta que no encuentre fin de secuencia
                while (node != null && node.GetValue() == "¬" && node != lastNode)
                {
                    node = node.GetNextNode();
                }
                T();
                ListaE();
                return;
            case "Operador":
                node = node.GetNextNode();
                //Avanzar hasta que no encuentre fin de secuencia
                while (node != null && node.GetValue() == "¬" && node != lastNode)
                {
                    node = node.GetNextNode();
                }
                T();
                ListaE();
                return;

            case "Separador":
                return;

            case "Delimitador":
                if (node != null && node.GetValue() == ")")
                    return;
                return;

            case "FinSecuencia":
                return;

            default:

                //Poner los errores
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
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Dos operadores juntos: Boolean después de otro operador\n";
                return;

            case "KeyWord":
                //SE TOMA COMO TÉRMINO NORMAL
                P();
                ListaE();
                return;

            default:
                //Poner los errores
                StructureValidator.instance.errors = StructureValidator.instance.errors
                   + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Dos o más operadores seguidos\n";
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
                return;

            case "KeyWord":
                //ERROR
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Nombre de variable contiene palabra reservada\n";
                node = node.GetNextNode();
                return;

            default:
                //Poner los errores
                break;
        }
    }

    public void Delimitador()
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
                    Valor();
                    if (node != null && node.GetValue() == ")")
                    {
                        node = node.GetNextNode();
                        //Avanzar hasta que no encuentre fin de secuencia
                        while (node != null && node.GetValue() == "¬" && node != lastNode)
                        {
                            StructureValidator.instance.lineNumber++;
                            node = node.GetNextNode();
                        }
                        return;
                    }
                    //poner los errores
                    StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Falta cerrar un paréntesis\n";
                    StructureValidator.instance.isBalanced = false;
                    return;
                }

                else if (node != null && node.GetValue() == "{")
                {
                    node = node.GetNextNode();
                    StructureValidator.instance.node = node;
                    StructureValidator.instance.S();
                    node = StructureValidator.instance.node;
                    if (node != null && node.GetValue() == "}")
                    {
                        node = node.GetNextNode();
                        //Avanzar hasta que no encuentre fin de secuencia
                        while (node != null && node.GetValue() == "¬" && node != lastNode)
                        {
                            StructureValidator.instance.lineNumber++;
                            node = node.GetNextNode();
                        }

                        if (node != null && node.GetValue() == "¬" && node == lastNode)
                        {
                            StructureValidator.instance.lineNumber++;
                        }
                        return;
                    }
                    //poner los errores
                    StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Falta cerrar una llave\n";
                    StructureValidator.instance.isBalanced = false;
                    return;
                }

                if (node != null && (node.GetValue() == "{" || node.GetValue() == "}" ||
                    node.GetValue() == "[" || node.GetValue() == "]"))
                {
                    StructureValidator.instance.hasStrangeSymbol = true;
                }

                //poner los errores
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "- Faltan llaves o paréntesis\n";
                return;

            case "FinSecuencia":
                //ERROR
                StructureValidator.instance.errors = StructureValidator.instance.errors
                   + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Faltan llaves o paréntesis\n";
                return;

            case "TipoDato":
                //ERROR
                StructureValidator.instance.errors = StructureValidator.instance.errors
                   + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Faltan llaves o Paréntesis después de Palabra Reservada\n";
                StructureValidator.instance.node = node;
                StructureValidator.instance.S();
                node = StructureValidator.instance.node;
                return;

            case "Variable":
                //ERROR
                StructureValidator.instance.errors = StructureValidator.instance.errors
                   + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Faltan llaves o Paréntesis después de Palabra Reservada\n";
                StructureValidator.instance.node = node;
                StructureValidator.instance.S();
                node = StructureValidator.instance.node;
                return;

            default:
                //Poner los errores
                StructureValidator.instance.errors = StructureValidator.instance.errors
                   + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Falta cerrar paréntesis\n" + "Fin de lectura";
                StructureValidator.instance.isBalanced = false;
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
                    while (node != null && node.GetValue() == "¬" && node != lastNode)
                    {
                        StructureValidator.instance.lineNumber++;
                        node = node.GetNextNode();
                    }
                    return;
                }

                //poner los errores
                return;

            default:
                //Poner los errores
                StructureValidator.instance.errors = StructureValidator.instance.errors
                   + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: No hay punto y coma ni igual ó dos o más variables seguidas\n";
                break;
        }
    }
}
