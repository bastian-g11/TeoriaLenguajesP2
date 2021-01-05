using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RWVariableSyntaxis : MonoBehaviour
{
    public AutomataType FindReservedWordInVariable(string lineaToRead, int _index)
    {
        string line = lineaToRead;
        string state = "IN";
        int index = _index;
        int inicio = 0;
        int length = 0;
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                    }

                    else if (Char.IsDigit(character))
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
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

             
                    else if (Char.IsLetter(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);

                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);

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
                        state = "RWVS2";
                        
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);

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
                        state = "RWVS2";
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
                        //Se tuvo que agregar para seguir buscando errores
                        state = "SS";
                        //state = "E";
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
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
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
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

                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";

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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        //Se tuvo que agregar para seguir buscando errores
                        state = "SS";
                        //state = "E";
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
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
                        //Se tuvo que agregar para seguir buscando errores
                        state = "SS";
                        //state = "E";
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(' '))
                    {
                        state = "SS";
                    }

                    else if (character.Equals(','))
                    {
                        state = "RWVS2";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";
                    }


                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        //Se tuvo que agregar para seguir buscando errores
                        state = "SS";
                        //state = "E";
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        //Se tuvo que agregar para seguir buscando errores
                        state = "SS";
                        //state = "E";

                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        //Se tuvo que agregar para seguir buscando errores
                        state = "SS";
                        //state = "E";

                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";

                    }

                    else if (Char.IsDigit(character))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }

                    else if (character.Equals(' '))
                    {
                        errors = errors + "- Error en declaración, nombre de variable contiene palabra reservada\n";
                        //Se tuvo que agregar para seguir buscando errores
                        state = "SS";
                        //state = "E";
                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
                    }


                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        || character.Equals('=') || character.Equals(';'))
                    {
                        state = "EV";
                        AutomataController.instance.exp = line.Substring(inicio, length - 1);
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
                        state = "RWVS2";
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
                        //Se tuvo que agregar para seguir buscando errores
                        state = "SS";
                        //state = "E";


                    }

                    //Operadores finales
                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%')
                        || character.Equals('=') || character.Equals(';'))
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
                    if (character.Equals('='))
                    {
                        state = "VAP";
                    }
                    else if (character.Equals(' '))
                    {
                        InsertarVariable(index, i, line);
                        state = "SS";
                    }

                    else if (character.Equals(','))
                    {
                        state = "RWVS2";
                    }

                    else if (character.Equals(';'))
                    {
                        state = "VAE";

                    }

                    else if (character.Equals('+') || character.Equals('-') ||
                        character.Equals('*') || character.Equals('/') || character.Equals('%'))
                    {
                        state = "FF";
                    }

                    else
                    {
                        errors = "- Error en declaración, nombre de variable contiene símbolo inválido\n";
                        //state = "E";
                    }

                    break;

                case "FF":
                    if(character.Equals('='))
                    {
                        AutomataController.instance.index = i + 1;

                        InsertarVariable(index, i - 1, line);
                        InsertarOperador(i - 1, line);

                        if (errors != null)
                        {
                            ErrorController.instance.SetErrorMessage(errors);
                            ErrorController.instance.SetLineHasError(true);
                        }
                        return AutomataType.StackAutomata;
                    }

                    else
                    {
                        errors = "- Error en declaración, expresión contiene símbolo inválido\n";
                    }

                    break;

                case "EV":
                    AutomataController.instance.index = i - 1;

                    //

                    if (line[i-1].Equals('+') || line[i - 1].Equals('-') ||
                        line[i - 1].Equals('*') || line[i - 1].Equals('/') || line[i - 1].Equals('%')
                        || line[i - 1].Equals('=') || line[i - 1].Equals(';'))
                    {
                        AutomataController.instance.index = i - 2;
                    }

                    if (line[i - 1].Equals(';'))
                    {
                        InsertarVariable(index, i - 1, line);
                        InsertarOperador(i - 1, line);
                        AutomataController.instance.index = i;
                        return AutomataType.MainStructure;
                    }

                    if (character.Equals(';') )
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
                    return AutomataType.DTVariableSyntax;

                case "VAE":

                    AutomataController.instance.index = i - 1;
                    InsertarVariable(index, i - 1, line);
                    InsertarOperador(i -1, line);
                    if (errors != null)
                    {
                        ErrorController.instance.SetErrorMessage(errors);
                        ErrorController.instance.SetLineHasError(true);
                    }
                    return AutomataType.MainStructure;

                case "VAP":
                    AutomataController.instance.index = i;

                    //
                    InsertarVariable(index, i - 1, line);
                    InsertarOperador(i - 1, line);
                    //

                    if (errors != null)
                    {
                        ErrorController.instance.SetErrorMessage(errors);
                        ErrorController.instance.SetLineHasError(true);
                    }
                    return AutomataType.StackAutomata;

                case "RWVS2":
                    //
                    if (line[i - 1].Equals(','))
                    {
                        AutomataController.instance.index = i;
                        InsertarVariable(index, i - 1, line);
                        InsertarOperador(i - 1, line);
                        return AutomataType.RW2VariableSyntax;
                    }
                    //
                    AutomataController.instance.index = i;

                    InsertarVariable(index, i - 1, line);
                    InsertarOperador(i - 1, line);

                    if (errors != null)
                    {
                        ErrorController.instance.SetErrorMessage(errors);
                        ErrorController.instance.SetLineHasError(true);
                    }
                    return AutomataType.RW2VariableSyntax;

                case "E":
                    Debug.Log("Entró a error en RWV");
                    return AutomataType.Error;

                default:
                    Debug.Log("Algo raro pasa");
                    break;
            }
        }


        AutomataController.instance.index = line.Length;

        if(state.Equals("VAE") || line[line.Length - 1].Equals(';'))
        {
            if (errors != null)
            {
                ErrorController.instance.SetErrorMessage(errors);
                ErrorController.instance.SetLineHasError(true);
            }
            //
            InsertarVariable(index, line.Length - 1, line);
            InsertarOperador(line.Length - 1, line);
            //
            return AutomataType.MainStructure;
        }

        else if (state.Equals("T1") || state.Equals("T2") || state.Equals("F2")
            || state.Equals("L2") || state.Equals("E2") || state.Equals("G") 
            || state.Equals("R2") || state.Equals("E3"))
        {
            errors = errors + "- Error en declaración, " +
             "nombre de variable contiene palabra reservada\n";
        }

        else if (state.Equals("IN"))
        {
            errors = errors + "- Expresión incompleta\n";
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
            return AutomataType.Error;
        }

        errors = errors + "- Falta punto y coma (;) \n";
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

    public void InsertarOperador(int i, string line)
    {
        string operador = line.Substring(i, 1);
        SinglyLinkedListController.instance.AddNode("Operadordasda", operador);
        UIController.instance.CreateUINode();
    }
}
