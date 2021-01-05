using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainStructure
{

    public AutomataType ReadStructure(string lineToRead, int _index)
    {
        string line = lineToRead;
        int index = _index;
        char character;
        string error;

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

            if (Char.IsLetter(character))
            {
                AutomataController.instance.index = i;
                return AutomataType.ReservedWord;
            }

            else if (character.Equals(' '))
            {
                Debug.Log("Espacio");
            }

            else if (character.Equals(';'))
            {
                Debug.Log("Entró un ;");
            }
            else if (Char.IsDigit(character))
            {
                error = "- La línea empieza con número\n";
                ErrorController.instance.SetErrorMessage(error);
                ErrorController.instance.SetLineHasError(true);
                return AutomataType.Error;
            }
            else
            {
                error = "- La línea empieza de forma incorrecta\n";
                ErrorController.instance.SetErrorMessage(error);
                ErrorController.instance.SetLineHasError(true);
                return AutomataType.Error;
            }
        }
        SinglyLinkedListController.instance.singlyLinkedList.TraverseLinkedList();
        return AutomataType.None;
    }
}
