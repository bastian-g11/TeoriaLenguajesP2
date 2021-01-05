using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StackAutomata
{
    Stack stack = new Stack();

    public AutomataType CheckRightSideStructure(string lineToRead, int _index)
    {
        string line = lineToRead;
        int index = _index;
        string symbol;
        char character;
        string errors = null;
        int inicio = 0;

        stack.Clear();
        stack.Push("T"); //Triangulo de pila vacía
        stack.Push("C");

        for (int i = index; i < line.Length; i++)
        {
            character = line[i];
            symbol = (string)stack.Peek();


            if (character.Equals('{') || character.Equals('}')
                || character.Equals('(') || character.Equals(')')
                || character.Equals('[') || character.Equals(']'))
            {
                continue;
            }

            switch (symbol)
            {
                case "C":
                    if (Char.IsLetter(character) || character.Equals('$') || character.Equals('_'))
                    {
                        Replace("V");
                        inicio = i;
                    }

                    else if (Char.IsDigit(character))
                    {
                        Replace("N");
                        inicio = i;
                    }

                    else if (character.Equals('"') || character.Equals('\''))
                    {
                        Replace("CM");
                        inicio = i;
                    }

                    else if (character.Equals('+') || character.Equals('-')) 
                    {
                        Replace("-");
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals(' '))
                    {
                        inicio = i;
                    }

                    else
                    {
                        errors = errors + "- La asignación empieza de forma inválida\n";
                    }
                    break;

                case "W":

                    if (Char.IsLetter(character) || character.Equals('$') || character.Equals('_'))
                    {
                        Replace("V");
                        inicio = i;
                    }

                    else if (Char.IsDigit(character))
                    {
                        Replace("N");
                        inicio = i;

                    }

                    else if (character.Equals('"') || character.Equals('\''))
                    {
                        Replace("CM");
                        inicio = i;

                    }

                    else if (character.Equals('+') || character.Equals('-'))
                    {
                        Replace("-");
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals(' '))
                    {
                        inicio = i;
                    }

                    else
                    {
                        errors = errors + "- La asignación tiene 2 operadores seguidos";
                        //Replace("ER");
                    }
                    break;
        

                case "K":

                    if (character.Equals('+'))
                    {
                        Replace("+");
                        InsertarOperador(i, line);
                        inicio = i;

                    }

                    else if (character.Equals('-'))
                    {
                        Replace("-");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('*') || character.Equals('/') || character.Equals('%'))
                    {
                        Replace("S");
                        InsertarOperador(i, line);
                        inicio = i;

                    }

                    else if (character.Equals('<') || character.Equals('>') || character.Equals('!'))
                    {
                        Replace("B");
                        InsertarOperador(i, line);
                        inicio = i;

                    }

                    else if (character.Equals('='))
                    {
                        Replace("=");
                        InsertarOperador(i, line);
                        inicio = i;

                    }

                    else if (character.Equals('|'))
                    {
                        Replace("|");
                        InsertarOperador(i, line);
                        inicio = i;

                    }

                    else if (character.Equals('&'))
                    {
                        Replace("&");
                        InsertarOperador(i, line);
                        inicio = i;

                    }

                    else if (character.Equals(';') || character.Equals(','))
                    {
                        Replace("VAE");
                        InsertarSeparador(i, line);

                        inicio = i;
                    }

                    else if (character.Equals(' '))
                    {
                        inicio = i;
                    }

                    else
                    {
                        errors = errors + "- Expresión inválida después de número\n";

                        //Replace("ER");
                    }
                    break;

                case "T":

                    if (character.Equals('+'))
                    {
                        Replace("+");
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals('-'))
                    {
                        Replace("-");
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals('*') || character.Equals('/') || character.Equals('%'))
                    {
                        Replace("S");
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals('<') || character.Equals('>') || character.Equals('!'))
                    {
                        Replace("B");
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals('='))
                    {
                        Replace("=");
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals('|'))
                    {
                        Replace("|");
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals('&'))
                    {
                        Replace("&");
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals(' '))
                    {
                        inicio = i;
                    }

                    else if (character.Equals(';') || character.Equals(','))
                    {
                        Replace("VAE");
                        InsertarSeparador(i, line);

                        inicio = i;
                    }

                    else
                    {
                        errors = errors + "- Símbolo inválido después de cierre de comillas\n";

                        //Replace("ER");
                    }
                    break;
                case "V":

                    if (Char.IsLetter(character) || character.Equals('$') || character.Equals('_')
                        || Char.IsDigit(character))
                    {
                        Debug.Log("Avance en V");
                    }

                    else if (character.Equals('+'))
                    {
                        Replace("+");
                        InsertarClase(inicio, i, line, "Variable");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('-'))
                    {
                        Replace("-");
                        InsertarClase(inicio, i, line, "Variable");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('*') || character.Equals('/') || character.Equals('%'))
                    {
                        Replace("S");
                        InsertarClase(inicio, i, line, "Variable");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('<') || character.Equals('>') || character.Equals('!'))
                    {
                        Replace("B");
                        InsertarClase(inicio, i, line, "Variable");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('='))
                    {
                        Replace("=");
                        InsertarClase(inicio, i, line, "Variable");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('|'))
                    {
                        Replace("|");
                        InsertarClase(inicio, i, line, "Variable");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('&'))
                    {
                        Replace("&");
                        InsertarClase(inicio, i, line, "Variable");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals(';') || character.Equals(','))
                    {
                        Replace("VAE");
                        InsertarClase(inicio, i, line, "Variable");
                        InsertarSeparador(i, line);

                    }

                    else if (character.Equals(' '))
                    {
                        Replace("K");
                        InsertarClase(inicio, i, line, "Variable");
                        inicio = i;
                    }

                    else
                    {
                        errors = errors + "- Error en sintaxis de alguna variable\n";
                        //Replace("ER");
                    }
                    break;

                case "N":

                    if (Char.IsDigit(character))
                    {
                        Debug.Log("Avance en N");
                    }

                    else if (character.Equals('+'))
                    {
                        Replace("+");
                        InsertarClase(inicio, i, line, "Número");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('-'))
                    {
                        Replace("-");
                        InsertarClase(inicio, i, line, "Número");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('.'))
                    {
                        Replace(".");
                    }

                    else if (character.Equals('*') || character.Equals('/') || character.Equals('%'))
                    {
                        Replace("S");
                        InsertarClase(inicio, i, line, "Número");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('<') || character.Equals('>') || character.Equals('!'))
                    {
                        Replace("B");
                        InsertarClase(inicio, i, line, "Número");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('='))
                    {
                        Replace("=");
                        InsertarClase(inicio, i, line, "Número");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('|'))
                    {
                        Replace("|");
                        InsertarClase(inicio, i, line, "Número");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals('&'))
                    {
                        Replace("&");
                        InsertarClase(inicio, i, line, "Número");
                        InsertarOperador(i, line);
                        inicio = i;
                    }

                    else if (character.Equals(' '))
                    {
                        Replace("K");
                        InsertarClase(inicio, i, line, "Número");
                        inicio = i;
                    }

                    else if (character.Equals('E') || character.Equals('e'))
                    {
                        Replace("E");
                    }

                    else if (character.Equals(';') || character.Equals(','))
                    {
                        Replace("VAE");
                        InsertarClase(inicio, i, line, "Número");
                        InsertarSeparador(i, line);

                        inicio = i;
                    }

                    else
                    {
                        errors = errors + "- Error de sintaxis en número \n";

                        //Replace("ER");
                    }

                    break;

                case "CM":

                    if(character.Equals('"') || character.Equals('\''))
                    {
                        stack.Pop();
                        InsertarClase(inicio, i + 1, line, "Término");
                        inicio = i;
                    }
                    break;

                case "S":

                    if (Char.IsLetter(character) || character.Equals('$') || character.Equals('_'))
                    {
                        Replace("V");
                        inicio = i;
                    }

                    else if (Char.IsDigit(character))
                    {
                        Replace("N");
                        inicio = i;

                    }

                    else if (character.Equals('"') || character.Equals('\''))
                    {
                        Replace("CM");
                        inicio = i;
                    }

                    else if (character.Equals('.'))
                    {
                        Replace(".");
                    }

                    else if (character.Equals(' '))
                    {
                        inicio = i;
                    }

                    else
                    {
                        errors = errors + "- Dos operadores seguidos o símbolo inesperado\n";
                        //Replace("ER");
                    }
                    break;

                case ".":

                    if (Char.IsDigit(character))
                    {
                        Replace("N");
                    }

                    else if (character.Equals('E') || character.Equals('e'))
                    {
                        Replace("E");
                    }

                    else
                    {
                        errors = errors + "- Error de sintaxis después de punto (.)\n";

                        //Replace("ER");
                    }
                    break;

                case "+":

                    if (Char.IsLetter(character) || character.Equals('$') || character.Equals('_'))
                    {
                        Replace("V");
                        inicio = i;
                    }
                    
                    else if(Char.IsDigit(character))
                    {
                        Replace("N");
                        inicio = i;
                    }

                    else if (character.Equals('"') || character.Equals('\''))
                    {
                        Replace("CM");
                        inicio = i;

                    }

                    else if (character.Equals('.'))
                    {
                        Replace(".");
                    }

                    else if (character.Equals(' '))
                    {
                        inicio = i;

                    }

                    else
                    {
                        errors = errors + "- Dos operadores seguidos\n";

                        //Replace("ER");
                    }

                    break;

                case "-":

                    if (Char.IsLetter(character) || character.Equals('$') || character.Equals('_'))
                    {
                        Replace("V");
                        inicio = i;
                    }

                    else if(Char.IsDigit(character))
                    {
                        Replace("N");
                        inicio = i;

                    }

                    else if (character.Equals('.'))
                    {
                        Replace(".");
                    }

                    else if (character.Equals(' '))
                    {
                        inicio = i;
                    }

                    else
                    {
                        errors = errors + "- Dos operadores seguidos\n";
                        //Replace("ER");
                    }
                    break;

                case "B":

                    if (Char.IsLetter(character) || character.Equals('$') || character.Equals('_'))
                    {
                        Replace("V");
                        inicio = i;

                    }

                    else if (Char.IsDigit(character))
                    {
                        Replace("N");
                        inicio = i;

                    }

                    else if (character.Equals('='))
                    {
                        Replace("W");
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals(' '))
                    {
                        Replace("W");
                        inicio = i;
                    }

                    else
                    {
                        errors = errors + "- Símbolo inesperado luego de operador booleano\n";

                        //Replace("ER");
                    }

                    break;

                case "=":

                    if (character.Equals('='))
                    {
                        Replace("W");
                        InsertarOperador(i, line);
                    }

                    else
                    {
                        errors = errors + "- Símbolo inesperado después de igual (=) \n";

                        //Replace("ER");
                    }
                    break;

                case "&":

                    if (character.Equals('&'))
                    {
                        Replace("W");
                        InsertarOperador(i, line);
                    }

                    else
                    {
                        errors = errors + "- Símbolo inesperado después de ampersand(&) \n";

                        //Replace("ER");
                    }

                    break;

                case "|":

                    if (character.Equals('|'))
                    {
                        Replace("W");
                        InsertarOperador(i, line);
                    }

                    else
                    {
                        errors = errors + "- Símbolo inesperado después de símbolo de pica (|) \n";
                        //Replace("ER");
                    }
                    break;

                case "E":

                    if(char.IsDigit(character))
                    {
                        Replace("Z");

                    }

                    else if (character.Equals('+'))
                    {
                        Replace("Y");
                        InsertarOperador(i, line);
                    }

                    else if (character.Equals('-'))
                    {
                        Replace("Y");
                        InsertarOperador(i, line);
                    }

                    else
                    {
                        errors = errors + "- Símbolo inesperado después de símbolo de notación científica \n";
                        //Replace("ER");
                    }

                    break;

                case "Y":

                    if (char.IsDigit(character))
                    {
                        Replace("Z");
                    }

                    else
                    {
                        errors = errors + "- Símbolo inesperado después símbolo signo de la parte de notación científica\n";

                        //Replace("ER");
                    }
                    break;

                case "Z":

                    if (char.IsDigit(character))
                    {
                        Debug.Log("Avance en Z");
                    }

                    else if (character.Equals('+'))
                    {
                        Replace("+");
                        InsertarOperador(i, line);

                    }

                    else if (character.Equals('-'))
                    {
                        Replace("-");
                        InsertarOperador(i, line);

                    }

                    else if (character.Equals('*') || character.Equals('/') || character.Equals('%'))
                    {
                        Replace("S");
                        InsertarOperador(i, line);

                    }

                    else if (character.Equals('<') || character.Equals('>') || character.Equals('!'))
                    {
                        Replace("B");
                        InsertarOperador(i, line);

                    }

                    else if (character.Equals('='))
                    {
                        Replace("=");
                        InsertarOperador(i, line);

                    }

                    else if (character.Equals('|'))
                    {
                        Replace("|");
                        InsertarOperador(i, line);

                    }

                    else if (character.Equals('&'))
                    {
                        Replace("&");
                        InsertarOperador(i, line);

                    }

                    else if (character.Equals(' '))
                    {
                        Replace("K");
                        inicio = i;
                    }

                    else if (character.Equals(';') || character.Equals(','))
                    {
                        Replace("VAE");
                        InsertarClase(inicio, i, line, "Número");
                        InsertarSeparador(i, line);
                    }

                    else
                    {
                        errors = errors + "- Exponente de notación científica son sintaxis incorrecta\n";

                        //Replace("ER");
                    }

                    break;
                case "VAE":
                    AutomataController.instance.index = i;
                    if (errors != null)
                    {
                        ErrorController.instance.SetErrorMessage(errors);
                        ErrorController.instance.SetLineHasError(true);
                    }
                    return AutomataType.MainStructure;

                case "ER":
                    //return AutomataType.Error;
                    break;
            }

        }

        Debug.Log("Final de secuencia, esto quedó en el tope: " + stack.Peek());
        AutomataController.instance.index = line.Length;

        if (stack.Peek().Equals("CM"))
        {
            errors = errors + "- Falta cerrar comillas\n";
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
            return AutomataType.Error;
        }

        else if (stack.Peek().Equals("VAE"))
        {
            if (errors != null)
            {
                ErrorController.instance.SetErrorMessage(errors);
                ErrorController.instance.SetLineHasError(true);
            }
            return AutomataType.MainStructure;
        }

        else if (stack.Peek().Equals("K") || stack.Peek().Equals("T") ||
            stack.Peek().Equals("V") || stack.Peek().Equals("N") || stack.Peek().Equals("Z"))
        {
            errors = errors + "- Falta punto y coma (;) \n";
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
        }
        else
        {
            errors = errors + "- Hay un error al final de la línea \n";
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
        }
        return AutomataType.MainStructure;
    }
    //Con i procesamos el símbolo siguiente al que nos hace cambiar de estado
    //Con i-1 procesamos el símbolo que nos hace cambiar de estado en el otro Autómata


    public void Replace(string symbol)
    {
        stack.Pop();
        stack.Push(symbol);
    }



    public void InsertarClase(int index, int i, string line, string clase)
    {
        int length = i - index;
        string operador = line.Substring(index, length);
        SinglyLinkedListController.instance.AddNode(clase, operador);
        UIController.instance.CreateUINode();
    }

    public void InsertarOperador(int i, string line)
    {
        string operador = line.Substring(i, 1);
        SinglyLinkedListController.instance.AddNode("Operador", operador);
        UIController.instance.CreateUINode();
    }

    public void InsertarSeparador(int i, string line)
    {
        string operador = line.Substring(i, 1);
        SinglyLinkedListController.instance.AddNode("Separador", operador);
        UIController.instance.CreateUINode();
    }
}
