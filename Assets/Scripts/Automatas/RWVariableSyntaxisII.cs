using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RWVariableSyntaxisII : MonoBehaviour
{
    public AutomataType FindReservedWordInVariable(string lineaToRead, int _index)
    {
        string line = lineaToRead;
        string state = "IN";
        int index = _index;
        int inicio = 0;
        int length = -1;
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
                    if (character.Equals('i'))
                    {
                        state = "I12";
                        inicio = i;
                    }

                    else if (character.Equals('f'))
                    {
                        state = "F1";
                        inicio = i;

                    }

                    else if (character.Equals('e'))
                    {
                        state = "E1";
                        inicio = i;

                    }

                    else if (character.Equals('S'))
                    {
                        state = "S2";
                        inicio = i;

                    }

                    else if (character.Equals('b'))
                    {
                        state = "B1";
                        inicio = i;

                    }

                    else if (character.Equals('c'))
                    {
                        state = "C";
                        inicio = i;

                    }

                    else if (character.Equals('d'))
                    {
                        state = "D";
                        inicio = i;

                    }

                    else if (character.Equals(' '))
                    {
                        state = "IN";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";

                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- 1 Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }

                    else if (Char.IsDigit(character))
                    {
                       
                        state = "EV";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";
                    }
                    length = length + 1;
                    break;

                case "I12":
                    if (character.Equals('n'))
                    {
                        state = "N1";
                    }

                    else if (character.Equals('f'))
                    {
                        state = "F2";
                    }

                    /*Si no es ninguno de las letras de arriba
                     *entonces todas las demás letras van a 
                     * mandar al otro autómata
                     * */
                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);

                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);

                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "N1":
                    if (character.Equals('t'))
                    {
                        state = "T1";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);

                    }

                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);

                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }
                    length = length + 1;

                    break;

                case "T1":

                    if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);

                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);

                    }

                    else if (character.Equals(' '))
                    {
                        errors = errors + "- Error en declaración, nombre de variable contiene palabra reservada\n";

                    }

                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "F1":

                    if (character.Equals('l'))
                    {
                        state = "L1";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%'))
                    {
                        errors = "- 12Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "L1":
                    if (character.Equals('o'))
                    {
                        state = "O1";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "O1":

                    if (character.Equals('a'))
                    {
                        state = "A1";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "A1":

                    if (character.Equals('t'))
                    {
                        state = "T2";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }
                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "T2":

                    if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(' '))
                    {
                        errors = errors + "- Error en declaración, nombre de variable contiene palabra reservada\n";
                        //state = "E";

                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                       )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "F2":

                    if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(' '))
                    {
                        errors = errors + "- Error en declaración, nombre de variable contiene palabra reservada\n";
                        //state = "E";

                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "B1":

                    if (character.Equals('o'))
                    {
                        state = "O2";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "O2":

                    if (character.Equals('o'))
                    {
                        state = "O3";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "O3":

                    if (character.Equals('l'))
                    {
                        state = "L2";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "L2":

                    if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(' '))
                    {
                        errors = errors + "- Error en declaración, nombre de variable contiene palabra reservada\n";
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }
                    length = length + 1;

                    break;

                case "E1":

                    if (character.Equals('l'))
                    {
                        state = "L3";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                       )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";
                    }
                    length = length + 1;

                    break;

                case "L3":

                    if (character.Equals('s'))
                    {
                        state = "S1";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "S1":

                    if (character.Equals('e'))
                    {
                        state = "E2";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "E2":

                    if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(' '))
                    {
                        errors = errors + "- Error en declaración, nombre de variable contiene palabra reservada\n";
                        //state = "E";

                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "S2":

                    if (character.Equals('t'))
                    {
                        state = "T3";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "T3":

                    if (character.Equals('r'))
                    {
                        state = "R1";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "R1":

                    if (character.Equals('i'))
                    {
                        state = "I3";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "I3":

                    if (character.Equals('n'))
                    {
                        state = "N2";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "N2":

                    if (character.Equals('g'))
                    {
                        state = "G";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "G":

                    if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(' '))
                    {
                        errors = errors + "- Error en declaración, nombre de variable contiene palabra reservada\n";
                        //state = "E";

                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "C":

                    if (character.Equals('h'))
                    {
                        state = "H";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "H":

                    if (character.Equals('a'))
                    {
                        state = "A2";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }


                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "A2":

                    if (character.Equals('r'))
                    {
                        state = "R2";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }


                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "R2":

                    if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(' '))
                    {
                        errors = errors + "- Error en declaración, nombre de variable contiene palabra reservada\n";
                        //state = "E";

                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "D":

                    if (character.Equals('o'))
                    {
                        state = "O4";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);

                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;

                    break;

                case "O4":

                    if (character.Equals('u'))
                    {
                        state = "U";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);

                    }


                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                       )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;
                    break;

                case "U":

                    if (character.Equals('b'))
                    {
                        state = "B2";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                    }


                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;
                    break;

                case "B2":

                    if (character.Equals('l'))
                    {
                        state = "L4";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                       )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;
                    break;

                case "L4":

                    if (character.Equals('e'))
                    {
                        state = "E3";
                    }

                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                       )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";

                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }
                    length = length + 1;
                    break;

                case "E3":

                    if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(' '))
                    {
                        errors = errors + "- Error en declaración, nombre de variable contiene palabra reservada\n";
                        //state = "E";

                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                       )
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }

                    length = length + 1;
                    break;

                case "SS":
                    if (character.Equals(';'))
                    {
                        state = "VAE";
                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (character.Equals(','))
                    {
                        state = "EV";
                        
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }

                    break;

                case "EV":
                    
                    /*Cuando detecta que lo que lo trajo acá fue una coma
                     * Lo que hace es que inserta la coma y la variable y pasa i
                     * para que se analice lo que sigue después de la coma, 
                     * pero si lo que lo trajo acá no fue una coma, significa que fue 
                     * porque encontró una letra diferente a las reservadas 
                     * */
                    if (line[i - 1].Equals(','))
                    {
                        AutomataController.instance.index = i;
                        InsertarVariable(index, i - 1, line); 
                        InsertarOperador(i - 1, line);
                        return AutomataType.RW2VariableSyntax;
                    }

                    AutomataController.instance.index = i - 1;

                    //
                        if(line[i - 1].Equals(';'))
                    {
                        InsertarVariable(index, i - 1, line);
                        InsertarOperador(i - 1, line);
                        AutomataController.instance.index = i;
                        return AutomataType.MainStructure;
                    }

                    if(character.Equals(';'))
                    {
                        InsertarVariable(index, i, line);
                        InsertarOperador(i, line);
                        AutomataController.instance.index = i;
                        return AutomataType.MainStructure;
                    }
                    //

                    if (errors != null)
                    {
                        ErrorController.instance.SetErrorMessage(errors);
                        ErrorController.instance.SetLineHasError(true);
                    }

                    return AutomataType.DTCVariableSyntax;

                case "VAE":

                    AutomataController.instance.index = i - 1;
                    InsertarVariable(index, i - 1, line);
                    InsertarOperador(i - 1, line);
                    if (errors != null)
                    {
                        ErrorController.instance.SetErrorMessage(errors);
                        ErrorController.instance.SetLineHasError(true);
                    }
                    return AutomataType.MainStructure;

               

                case "E":
                    Debug.Log("Entró a error en RWV2");
                    return AutomataType.Error;

                default:
                    Debug.Log("Algo raro pasa");
                    break;
            }
        }

        AutomataController.instance.index = line.Length;

        if (state.Equals("T1") || state.Equals("T2") || state.Equals("F2")
            || state.Equals("L2") || state.Equals("E2") || state.Equals("G")
            || state.Equals("R2") || state.Equals("E3"))
        {
            errors = errors + "- Error en declaración, " +
             "nombre de variable contiene palabra reservada\n";
        }

        else if (line[line.Length - 1].Equals(';'))
        {
            InsertarVariable(index, line.Length - 1, line);
            InsertarOperador(line.Length - 1, line);
            return AutomataType.MainStructure;
        }

        else if (state.Equals("IN"))
        {
            errors = errors + "- Expresión incompleta\n";
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
            return AutomataType.Error;
        }

        if (errors != null)
        {
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
        }

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
        SinglyLinkedListController.instance.AddNode("Variable", variable);
        UIController.instance.CreateUINode();
    }

    public void InsertarVariable2(int index, int i, string line)
    {
        int length = i - index;
        string variable = line.Substring(index, length);
        string s = AutomataController.instance.exp;
        SinglyLinkedListController.instance.AddNode("Variable", s +variable);
        UIController.instance.CreateUINode();
    }

    public void InsertarOperador(int i, string line)
    {
        string operador = line.Substring(i, 1);
        SinglyLinkedListController.instance.AddNode("Operador", operador);
        UIController.instance.CreateUINode();
    }
}
