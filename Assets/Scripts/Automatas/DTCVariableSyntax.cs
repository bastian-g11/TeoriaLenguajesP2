using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DTCVariableSyntax : MonoBehaviour
{
    public AutomataType CheckDataTypeVariableSyntax(string lineToRead, int _index)
    {
        string line = lineToRead;
        string state = "IN";
        int index = _index;
        char character;
        string errors = null;

        for (int i = index; i < line.Length; i++)
        {
            character = line[i];


            if (character.Equals('{') || character.Equals('}')
                || character.Equals('(') || character.Equals(')')
                || character.Equals('[') || character.Equals(']')
                || character.Equals('<') || character.Equals('>'))
            {
                continue;
            }

            switch (state)
            {
                case "IN":

                    if (Char.IsLetterOrDigit(character))
                    {
                        state = "A";
                    }

                    else if (character.Equals(' '))
                    {
                        state = "IN";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                        InsertarSeparador(i, line);
                    }

                    else
                    {
                        errors = "-  El nombre de la variable empieza de manera incorrecta\n";
                    }
                    break;

                case "A":

                    if (Char.IsLetterOrDigit(character))
                    {
                        state = "A";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "A";
                    }

                    else if (character.Equals(','))
                    {
                        state = "EC";
                        //index-1 para que tome el símbolo que estaba antes de este (que se procesó en RWVS2)
                        InsertarVariable(index - 1, i, line);
                        InsertarSeparador(i, line);
                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                        InsertarVariable(index - 1, i, line);
                        InsertarSeparador(i, line);
                    }

                    else
                    {
                        errors = "- El nombre de la variable empieza de manera incorrecta\n";
                    }
                    break;

                case "D":

                    if (character.Equals('+') || character.Equals('-') ||
                       character.Equals('*') || character.Equals('/') || character.Equals('%'))
                    {
                        state = "F";

                    }

                    else if (character.Equals(','))
                    {
                        state = "EC";
                    }

                    else if (character.Equals(' '))
                    {
                        state = "D";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- El nombre de la variable empieza de manera incorrecta\n";
                    }
                    break;

                case "SS":

                    if (character.Equals(','))
                    {
                        state = "EC";
                        InsertarSeparador(i, line);
                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                        InsertarSeparador(i, line);

                    }

                    else
                    {
                        errors = "- Expresión de declaración contiene símbolo inválido\n";
                    }
                    break;

                case "F":
                    if (character.Equals(';'))
                    {
                        state = "VAE";
                        InsertarSeparador(i, line);
                    }
                    else
                    {
                        errors = "- Expresión de declaración contiene símbolo inválido\n";

                    }
                    break;

                case "EC":
                    state = "RWVS2";
                    break;

                case "VAE":

                    AutomataController.instance.index = i - 1;

                    if (errors != null)
                    {
                        ErrorController.instance.SetErrorMessage(errors);
                        ErrorController.instance.SetLineHasError(true);
                    }
                    return AutomataType.MainStructure;

                case "RWVS2":
                    /*i-1 para coger lo que está justo después de la coma, ya que cuando 
                    *se detecta una coma, se avanza y se va a EC, y luego en EC a RWVS2
                    * por lo que queremos justo lo que está cuando estabamos en EC
                    * */
                    AutomataController.instance.index = i - 1;

                    if (errors != null)
                    {
                        ErrorController.instance.SetErrorMessage(errors);
                        ErrorController.instance.SetLineHasError(true);
                    }
                    return AutomataType.RW2VariableSyntax;

                case "E":
                    return AutomataType.Error;
            }
        }
        AutomataController.instance.index = line.Length - 1;

        if (state.Equals("IN"))
        {
            errors = errors + "- Expresión incompleta\n";
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
            return AutomataType.Error;
        }

        else if (state.Equals("VAE") || line[line.Length-1].Equals(';'))
        {
            if (errors != null)
            {
                ErrorController.instance.SetErrorMessage(errors);
                ErrorController.instance.SetLineHasError(true);
            }
            

            return AutomataType.MainStructure;
        }

        errors = errors + "- Expresión incompleta\n";
        ErrorController.instance.SetErrorMessage(errors);
        ErrorController.instance.SetLineHasError(true);
        return AutomataType.Error;
    }

    public void InsertarNodo(int index, int i, string line)
    {
        int length = (i - 1) - index;
        string variable = line.Substring(index, length);
        SinglyLinkedListController.instance.AddNode("tipo", variable);
       
        UIController.instance.CreateUINode();
    }

    public void InsertarVariable(int index, int i, string line)
    {
        int length = i - index;
        string variable = line.Substring(index, length);
        string s = AutomataController.instance.exp;
        SinglyLinkedListController.instance.AddNode("Variable", s + variable);
        UIController.instance.CreateUINode();
        AutomataController.instance.exp = "";
    }

    public void InsertarSeparador(int i, string line)
    {
        string separador = line.Substring(i, 1);
        SinglyLinkedListController.instance.AddNode("Separador", separador);
        UIController.instance.CreateUINode();
    }
}
