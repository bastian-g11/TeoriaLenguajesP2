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

    public int lineNumber = 0;

    public void Recorrer(string _lineToRead)
    {
        string line = _lineToRead;
        string aLine = null;
        System.IO.StringReader strReader = new StringReader(line);
        CreateLinkedList();
        lineNumber = 0;
        ErrorController.instance.SetLineHasError(false);

        while (true)
        {
            lineNumber += 1;
            aLine = strReader.ReadLine();
            if (aLine == null) break;
            UIController.instance.CreateContainer();
            Parse(aLine);
        }

        bool lineHasError = ErrorController.instance.GetLineHasError();
        if (lineHasError)
        {
            Destroy(UIController.instance.temporalContainer);
            UIController.instance.distanceY = UIController.instance.distanceY - 5;
            ResetLinkedList();
        }
    }

    // Returns 'true' if the character is a DELIMITER. 
    bool isDelimiter(char ch)
    {
        if (ch == ' ' || ch == '+' || ch == '-' || ch == '*' ||
            ch == '/' || ch == ',' || ch == ';' || ch == '>' ||
            ch == '<' || ch == '=' || ch == '(' || ch == ')' ||
            ch == '[' || ch == ']' || ch == '{' || ch == '}' ||
            ch == '%' || ch == '^')
        {
            return (true);
        }
        return (false);
    }

    // Returns 'true' if the character is an OPERATOR. 
    bool isOperator(char ch)
    {
        if (ch == '+' || ch == '-' || ch == '*' ||
            ch == '/' || ch == '>' || ch == '<' ||
            ch == '=' || ch == '%' || ch == '^')
            return (true);
        return (false);
    }

    bool isBoolean(string ch)
    {
        if (ch == "&&" || ch == "||" || ch == "true" || ch == "false")
            return (true);
        return (false);
    }

    // Returns 'true' if the string is a VALID IDENTIFIER. 
    bool ValidIdentifier(string str)
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
        if (str.Equals("if") || str.Equals("else") || str.Equals("while") ||
            str.Equals("bool") || str.Equals("String") || str.Equals("int") ||
            str.Equals("float"))
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
    string SubString(string str, int left, int right)
    {
        int i;
        string subStr = "";
        for (i = left; i <= right; i++)
            subStr = subStr + str[i];
        return (subStr);
    }

    // Parsing the input STRING. 
    void Parse(string str)
    {
        int left = 0, right = 0;
        int len = str.Length;
        bool isEnd = false;
        string errors = null;
        string tag = null;
        string subStr = null;
        bool lineHasError = ErrorController.instance.GetLineHasError();
        Debug.Log("Valor Tamaño: " + len);
        while (right < len && left <= right)
        {
            if (isDelimiter(str[right]) == false)
            {
                right++;
                if (right == len) isEnd = true;
            }

            if (!isEnd)
            {
                if (isDelimiter(str[right]) == true && left == right)
                {
                    subStr = str[right].ToString();
                    if (isOperator(str[right]) == true)
                    {
                        Debug.Log("'%c' IS AN OPERATOR: \n" + str[right]);
                        tag = "Operador";
                        if (!lineHasError)
                        {
                            CreateNode(tag, subStr);
                        }
                    }
                    else if (str[right] != (' '))
                    {
                        Debug.Log("'%c' IS A DELIMITER: \n" + str[right]);
                        tag = "Separador";
                        if (!lineHasError)
                        {
                            CreateNode(tag, subStr);
                        }
                    }
                    right++;
                    left = right;
                }
                else if ((right == len && left != right)
                         || isDelimiter(str[right]) == true && left != right)
                {
                    subStr = SubString(str, left, right - 1);
                    if (isKeyword(subStr) == true)
                    {
                        Debug.Log("'%s' IS A KEYWORD\n" + subStr);
                        tag = "KeyWord";
                    }

                    else if (isInteger(subStr) == true)
                    {
                        Debug.Log("'%s' IS AN INTEGER\n" + subStr);
                        tag = "Número";
                    }

                    else if (isRealNumber(subStr) == true)
                    {
                        Debug.Log("'%s' IS A REAL NUMBER\n" + subStr);
                        tag = "Número";
                    }

                    else if (isBoolean(subStr) == true)
                    {
                        Debug.Log("'%s' IS A BOOLEAN OPERATOR\n" + subStr);
                        tag = "Boolean";
                    }

                    else if (ValidIdentifier(subStr) == true
                            && isDelimiter(str[right - 1]) == false)
                    {
                        Debug.Log("'%s' IS A VALID IDENTIFIER\n" + subStr);
                        tag = "Variable";
                    }

                    else if (ValidIdentifier(subStr) == false
                            && isDelimiter(str[right - 1]) == false)
                    {
                        errors = errors + "Nombre de variable incorrecto\n";
                        Debug.Log("'%s' IS NOT A VALID IDENTIFIER\n" + subStr);
                    }
                    left = right;
                    if (!lineHasError)
                    {
                        CreateNode(tag, subStr);
                    }
                }
            }
            else
            {
                if (isDelimiter(str[right - 1]) == true && left == right)
                {
                    subStr = str[right].ToString();
                    if (isOperator(str[right]) == true)
                    {
                        Debug.Log("'%c' IS AN OPERATOR: \n" + str[right]);
                        tag = "Operador";
                        if (!lineHasError)
                        {
                            CreateNode(tag, subStr);
                        }
                    }
                    else if (str[right] != (' '))
                    {
                        Debug.Log("'%c' IS A DELIMITER: \n" + str[right]);
                        tag = "Separador";
                        if (!lineHasError)
                        {
                            CreateNode(tag, subStr);
                        }
                    }
                    right++;
                    left = right;
                }
                else if ((right == len && left != right)
                         || isDelimiter(str[right - 1]) == true && left != right)
                {

                    subStr = SubString(str, left, right - 1);
                    if (isKeyword(subStr) == true)
                    {
                        Debug.Log("'%s' IS A KEYWORD\n" + subStr);
                        tag = "KeyWord";

                    }

                    else if (isInteger(subStr) == true)
                    {
                        Debug.Log("'%s' IS AN INTEGER\n" + subStr);
                        tag = "Número";
                    }

                    else if (isRealNumber(subStr) == true)
                    {
                        Debug.Log("'%s' IS A REAL NUMBER\n" + subStr);
                        tag = "Número";
                    }

                    else if (isBoolean(subStr) == true)
                    {
                        Debug.Log("'%s' IS A BOOLEAN OPERATOR\n" + subStr);
                        tag = "Boolean";
                    }

                    else if (ValidIdentifier(subStr) == true
                            && isDelimiter(str[right - 1]) == false)
                    {
                        Debug.Log("'%s' IS A VALID IDENTIFIER\n" + subStr);
                        tag = "Variable";
                    }

                    else if (ValidIdentifier(subStr) == false
                            && isDelimiter(str[right - 1]) == false)
                    {
                        errors = errors + "Nombre de variable incorrecto\n";
                        Debug.Log("'%s' IS NOT A VALID IDENTIFIER\n" + subStr);
                    }

                    left = right;
                    if (!lineHasError)
                    {
                        CreateNode(tag, subStr);
                    }
                }
            }
        }

        if (errors != null)
        {
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
            UIController.instance.SetErrorText(lineNumber);
        }
        return;
    }

    public void CreateLinkedList()
    {
        SinglyLinkedListController.instance.CreateSinglyLinkedList();
    }

    public void ResetLinkedList()
    {
        SinglyLinkedListController.instance.ResetSinglyLinkedList();
    }

public void CreateNode(string type, string line)
    {
        //if (char.IsWhiteSpace(line[0])) return;
        SinglyLinkedListController.instance.AddNode(type, line);
        UIController.instance.CreateUINode();
    }
}