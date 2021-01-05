using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextReader : MonoBehaviour
{
    #region singleton
    public static TextReader instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public void SetLinkedList()
    {
        SinglyLinkedListController.instance.CreateSinglyLinkedList();
    }

    public void AddNode(string _dataType, string _value)
    {
        SinglyLinkedListController.instance.AddNode(_dataType, _value);
    }

    public void ResetLinkedList()
    {
        SinglyLinkedListController.instance.ResetSinglyLinkedList();
    }

    public void Recorrer(string _lineToRead)
    {
        string s = _lineToRead;
        parse(s);
    }

    // Returns 'true' if the character is a DELIMITER. 
    bool isDelimiter(char ch)
    {
        if (ch == ' ' || ch == '+' || ch == '-' || ch == '*' ||
            ch == '/' || ch == ',' || ch == ';' || ch == '>' ||
            ch == '<' || ch == '=' || ch == '(' || ch == ')' ||
            ch == '[' || ch == ']' || ch == '{' || ch == '}')
        {
            //Debug.Log("Es un separador");
            return (true);
        }
        return (false);
    }

    // Returns 'true' if the character is an OPERATOR. 
    bool isOperator(char ch)
    {
        if (ch == '+' || ch == '-' || ch == '*' ||
            ch == '/' || ch == '>' || ch == '<' ||
            ch == '=')
            return (true);
        return (false);
    }

    // Returns 'true' if the string is a VALID IDENTIFIER. 
    bool validIdentifier(string str)
    {
        if (str[0] == '0' || str[0] == '1' || str[0] == '2' ||
            str[0] == '3' || str[0] == '4' || str[0] == '5' ||
            str[0] == '6' || str[0] == '7' || str[0] == '8' ||
            str[0] == '9' || isDelimiter(str[0]) == true)
            return (false);
        return (true);
    }

    // Returns 'true' if the string is a KEYWORD. 
    bool isKeyword(string str)
    {
        if (str.Equals("if") || str.Equals("else") || str.Equals("while"))
        {
            Debug.Log("Es palabra clave");
            return (true);
        }
        return (false);
    }

    // Returns 'true' if the string is an INTEGER. 
    bool isInteger(string str)
    {
        int i, len = str.Length;

        if (len == 0)
            return (false);
        for (i = 0; i < len; i++)
        {
            if (str[i] != '0' && str[i] != '1' && str[i] != '2'
                && str[i] != '3' && str[i] != '4' && str[i] != '5'
                && str[i] != '6' && str[i] != '7' && str[i] != '8'
                && str[i] != '9' || (str[i] == '-' && i > 0))
                return (false);
        }
        return (true);
    }

    // Returns 'true' if the string is a REAL NUMBER. 
    bool isRealNumber(string str)
    {
        int i, len = str.Length;
        bool hasDecimal = false;

        if (len == 0)
            return (false);
        for (i = 0; i < len; i++)
        {
            if (str[i] != '0' && str[i] != '1' && str[i] != '2'
                && str[i] != '3' && str[i] != '4' && str[i] != '5'
                && str[i] != '6' && str[i] != '7' && str[i] != '8'
                && str[i] != '9' && str[i] != '.' ||
                (str[i] == '-' && i > 0))
                return (false);
            if (str[i] == '.')
                hasDecimal = true;
        }
        return (hasDecimal);
    }

    // Extracts the SUBSTRING. 
    string subString(string str, int left, int right)
    {
        int i;
        //string subStr = (char*)malloc(
        //            sizeof(char) * (right - left + 2));
        string subStr = "";
        for (i = left; i <= right; i++)
            subStr = subStr + str[i];
        return (subStr);
    }

    // Parsing the input STRING. 
    void parse(string str)
    {
        int left = 0, right = 0;
        int len = str.Length;
        bool isEnd = false;
        Debug.Log("Valor Tamaño: " + len);
        while (right < len && left <= right)
        {
            if (isDelimiter(str[right]) == false)
            {
                right++;
                if (right == len) isEnd = true;
            }

            //Debug.Log("Valor Left: " + left);
            //Debug.Log("Valor Right: " + right);
            if (!isEnd)
            {
                if (isDelimiter(str[right]) == true && left == right)
                {
                    if (isOperator(str[right]) == true)
                        Debug.Log("'%c' IS AN OPERATOR: \n" + str[right]);

                    right++;
                    left = right;
                }
                else if ((right == len && left != right)
                         || isDelimiter(str[right]) == true && left != right)
                {
                    string subStr = subString(str, left, right - 1);

                    if (isKeyword(subStr) == true)
                        Debug.Log("'%s' IS A KEYWORD\n" + subStr);

                    else if (isInteger(subStr) == true)
                        Debug.Log("'%s' IS AN INTEGER\n" + subStr);

                    else if (isRealNumber(subStr) == true)
                        Debug.Log("'%s' IS A REAL NUMBER\n" + subStr);

                    else if (validIdentifier(subStr) == true
                            && isDelimiter(str[right - 1]) == false)
                        Debug.Log("'%s' IS A VALID IDENTIFIER\n" + subStr);

                    else if (validIdentifier(subStr) == false
                            && isDelimiter(str[right - 1]) == false)
                        Debug.Log("'%s' IS NOT A VALID IDENTIFIER\n" + subStr);
                    left = right;
                }
            }
            else
            {
                if (isDelimiter(str[right - 1]) == true && left == right)
                {
                    if (isOperator(str[right]) == true)
                        Debug.Log("'%c' IS AN OPERATOR: \n" + str[right]);

                    right++;
                    left = right;
                }
                else if ((right == len && left != right)
                         || isDelimiter(str[right - 1]) == true && left != right)
                {
                    string subStr = subString(str, left, right - 1);

                    if (isKeyword(subStr) == true)
                        Debug.Log("'%s' IS A KEYWORD\n" + subStr);

                    else if (isInteger(subStr) == true)
                        Debug.Log("'%s' IS AN INTEGER\n" + subStr);

                    else if (isRealNumber(subStr) == true)
                        Debug.Log("'%s' IS A REAL NUMBER\n" + subStr);

                    else if (validIdentifier(subStr) == true
                            && isDelimiter(str[right - 1]) == false)
                        Debug.Log("'%s' IS A VALID IDENTIFIER\n" + subStr);

                    else if (validIdentifier(subStr) == false
                            && isDelimiter(str[right - 1]) == false)
                        Debug.Log("'%s' IS NOT A VALID IDENTIFIER\n" + subStr);
                    left = right;
                }
            }
        }
        return;
    }
}