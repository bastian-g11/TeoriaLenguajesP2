using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VariableSyntax 
{
    public AutomataType CheckVariableSyntax(string lineToRead, int _index)
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

                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/'))
                    {
                        state = "F";
                        InsertarVariable(index, i, line);
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                        InsertarVariable(index, i, line);
                    }

                    else if (character.Equals('='))
                    {
                        state = "VAP";
                        InsertarVariable(index, i, line);
                        InsertarOperador(i, line);
                    }

                    else
                    {
                        errors = "- Error en nombramiento de variable\n";
                        //state = "E";
                    }
                    break;

                case "A":
                    if (Char.IsLetterOrDigit(character))
                    {
                        state = "A";
                    }

                    else if (character.Equals('+') || character.Equals('-') ||
                       character.Equals('*') || character.Equals('/') || character.Equals('%'))
                    {
                        state = "F";
                        InsertarVariable(index, i, line);
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                        InsertarVariable(index, i, line);
                    }

                    else if (character.Equals('='))
                    {
                        state = "VAP";
                        InsertarVariable(index, i, line);
                        InsertarOperador(i, line);
                    }

                    else
                    {
                        errors = "- Error en nombramiento de variable\n";
                    }
                    break;

                case "SS":
                    if(character.Equals('+') || character.Equals('-') ||
                         character.Equals('*') || character.Equals('/') || character.Equals('%'))
                    {
                        state = "F";
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (character.Equals('='))
                    {
                        state = "VAP";
                        InsertarOperador(i, line);
                    }

                    else
                    {
                        errors = "- Error en nombramiento de variable\n";

                        //state = "E";
                    }
                    break;

                case "F":
                    if (character.Equals('='))
                    {
                        state = "VAP";
                        InsertarOperador(i, line);
                    }

                    else
                    {
                        errors = "- Error en nombramiento de variable\n";
                    }
                    break;

                case "VAP":

                    //Se pasa solo la i para no procesar el = 

                    AutomataController.instance.index = i;
                    if (errors != null)
                    {
                        ErrorController.instance.SetErrorMessage(errors);
                        ErrorController.instance.SetLineHasError(true);
                    }
                    return AutomataType.StackAutomata;

                case "E":
                    Debug.Log("Entró a error en VariableSyntax");
                    return AutomataType.Error;
            }
        }

        errors = errors + "- Expresión incompleta\n";
        ErrorController.instance.SetErrorMessage(errors);
        ErrorController.instance.SetLineHasError(true);
        return AutomataType.Error;
    }

    public void InsertarVariable(int index, int i, string line)
    {
        int length = i - index;
        string s = AutomataController.instance.exp;
        string variable = line.Substring(index, length);
        SinglyLinkedListController.instance.AddNode("Variable", s + variable);
        UIController.instance.CreateUINode();
        AutomataController.instance.exp = "";
    }

    public void InsertarOperador(int i, string line)
    {
        string operador = line.Substring(i, 1);
        SinglyLinkedListController.instance.AddNode("Operador", operador);
        UIController.instance.CreateUINode();
    }
}
