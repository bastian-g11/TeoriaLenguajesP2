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
                return;

            default:
                //Poner los errores
                StructureValidator.instance.node = node;

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

                Debug.Log("Falló en B Valor con: " + node.GetValue());
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
                Debug.Log("Falló en B Variable con: " + node.GetValue());
                break;
        }
    }

    public void ListaE()
    {
        Debug.Log("Entró a ListaE de B con: " + node.GetValue());
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
                Debug.Log("Fin de secuencia B en ListaE");
                return;

            default:

                //Poner los errores
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
                //ERROR
                Debug.Log("Dos operadores juntos: Boolean después de otro operador");
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
                Debug.Log("Dos o más operadores seguidos");
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
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Falta cerrar comillas, apóstrofe o paréntesis\n";
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
                Debug.Log("Nombre de variable contiene palabra reservada");
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Nombre de variable contiene palabra reservada\n";
                node = node.GetNextNode();
                return;

            default:
                //Poner los errores
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
                        //Avanzar hasta que no encuentre fin de secuencia
                        while (node != null && node.GetValue() == "¬" && node != lastNode)
                        {
                            StructureValidator.instance.lineNumber++;
                            Debug.Log("<color=green>Sumé </color>" + " tengo: " + StructureValidator.instance.lineNumber);
                            node = node.GetNextNode();
                        }
                        return;
                    }
                    //poner los errores
                    Debug.Log("Falló en Delimitador B, falta cerrar un Paréntesis");
                    StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Falta cerrar un paréntesis\n";
                    return;
                }

                else if (node != null && node.GetValue() == "{")
                {
                    node = node.GetNextNode();
                    StructureValidator.instance.node = node;
                    StructureValidator.instance.S();
                    node = StructureValidator.instance.node;
                    Debug.Log("Abre Llave en B Delimitador");
                    Debug.Log(node.GetValue() == "}");
                    if (node != null && node.GetValue() == "}")
                    {
                        node = node.GetNextNode();
                        //Avanzar hasta que no encuentre fin de secuencia
                        while (node != null && node.GetValue() == "¬" && node != lastNode)
                        {
                            StructureValidator.instance.lineNumber++;
                            Debug.Log("<color=green>Sumé x </color>" + " tengo: " + StructureValidator.instance.lineNumber);
                            node = node.GetNextNode();
                        }

                        if(node != null && node.GetValue() == "¬" && node == lastNode)
                        {
                            StructureValidator.instance.lineNumber++;
                            Debug.Log("<color=green>Sumé AL FINAL </color>" + " tengo: " + StructureValidator.instance.lineNumber);
                        }
                        return;
                    }
                    //poner los errores
                    Debug.Log("Falló en Delimitador B, falta cerrar una LLave");
                    StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Falta cerrar una llave\n";
                    return;
                }

                //poner los errores
                Debug.Log("Faltan llaves o Paréntesis");
                StructureValidator.instance.errors = StructureValidator.instance.errors
                    + "- Faltan llaves o paréntesis\n";
                return;

            case "FinSecuencia":
                //ERROR
                Debug.Log("Faltan llaves o Paréntesis");
                StructureValidator.instance.errors = StructureValidator.instance.errors
                   + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Faltan llaves o paréntesis\n";
                return;

            case "TipoDato":
                //ERROR
                Debug.Log("Faltan llaves o Paréntesis después de Palabra Reservada");
                StructureValidator.instance.errors = StructureValidator.instance.errors
                   + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Faltan llaves o Paréntesis después de Palabra Reservada\n";
                StructureValidator.instance.node = node;
                StructureValidator.instance.S();
                node = StructureValidator.instance.node;
                return;

            case "Variable":
                //ERROR
                Debug.Log("Faltan llaves o Paréntesis después de Palabra Reservada");
                StructureValidator.instance.errors = StructureValidator.instance.errors
                   + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: Faltan llaves o Paréntesis después de Palabra Reservada\n";
                StructureValidator.instance.node = node;
                StructureValidator.instance.S();
                node = StructureValidator.instance.node;
                return;

            default:

                //Poner los errores
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
                    while (node != null && node.GetValue() == "¬" && node != lastNode)
                    {
                        StructureValidator.instance.lineNumber++;
                        Debug.Log("<color=green>Sumé x </color>" + " tengo: " + StructureValidator.instance.lineNumber);
                        node = node.GetNextNode();
                    }
                    return;
                }

                //poner los errores
                return;

            default:
                //Poner los errores
                Debug.Log("Falló en B Separador con: " + node.GetValue() + " no hay punto y coma ni igual ó dos o más variables seguidas");
                StructureValidator.instance.errors = StructureValidator.instance.errors
                   + "<b>Línea " + (StructureValidator.instance.lineNumber + 1).ToString() + "</b>: No hay punto y coma ni igual ó dos o más variables seguidas\n";
                break;
        }
    }
}
