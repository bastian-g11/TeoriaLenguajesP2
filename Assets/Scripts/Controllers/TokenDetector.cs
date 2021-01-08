using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenDetector : MonoBehaviour
{

    #region singleton
    public static TokenDetector instance;
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

    public bool flagDT = false;

    // Returns 'true' if the character is a DELIMITER. 
    public bool isDelimiter(char ch)
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
    public bool isOperator(char ch)
    {
        if (ch == '+' || ch == '-' || ch == '*' ||
            ch == '/' || ch == '>' || ch == '<' ||
            ch == '=' || ch == '%' || ch == '^')
            return (true);
        return (false);
    }

    public bool isBoolean(string ch)
    {
        if (ch == "&&" || ch == "||" || ch == "true" || ch == "false")
            return (true);
        return (false);
    }

    // Returns 'true' if the string is a VALID IDENTIFIER. 
    public bool ValidIdentifier(string str)
    {
        if (str[0] == '0' || str[0] == '1' || str[0] == '2' ||
            str[0] == '3' || str[0] == '4' || str[0] == '5' ||
            str[0] == '6' || str[0] == '7' || str[0] == '8' ||
            str[0] == '9' || isDelimiter(str[0]) == true)
            return (false);
        return (true);
    }

    // Returns 'true' if the string is a KEYWORD. 
    public bool isKeyword(string str)
    {
        if (str.Equals("if") || str.Equals("else") || str.Equals("while"))
        {
            Debug.Log("Es palabra clave");
            return (true);
        }
        return (false);
    }

    public bool isDataType(string str)
    {
        if (str.Equals("boolean") || str.Equals("String") ||
            str.Equals("int") || str.Equals("float"))
        {
            Debug.Log("Es tipo de dato");
            return (true);
        }
        return (false);
    }

    // Returns 'true' if the string is an INTEGER. 
    public bool isInteger(string str)
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
    public bool isRealNumber(string str)
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
    public void Parse(string str, int lineNumber)
    {
        int left = 0, right = 0;
        int len = str.Length;
        bool isEnd = false;
        string errors = null;
        string tag = null;
        string subStr = null;
        bool lineHasError = ErrorController.instance.GetLineHasError();
        Node currentNode = null;
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
                    if (subStr == ";" || subStr == "{" || subStr == "}")
                        flagDT = false;

                    if (isOperator(str[right]) == true)
                    {
                        Debug.Log("'%c' IS AN OPERATOR: \n" + str[right]);
                        tag = "Operador";
                        if (!lineHasError)
                        {
                            currentNode = CreateNode(tag, subStr);
                        }
                    }
                    else if (str[right] != (' '))
                    {
                        Debug.Log("'%c' IS A DELIMITER: \n" + str[right]);
                        tag = "Separador";
                        if (!lineHasError)
                        {
                            currentNode = CreateNode(tag, subStr);
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

                    else if (!flagDT && isDataType(subStr) == true)
                    {
                        Debug.Log("'%s' IS A KEYWORD\n" + subStr);
                        tag = "TipoDato";
                        flagDT = true;
                    }

                    else if (isInteger(subStr) == true)
                    {
                        Debug.Log("'%s' IS AN INTEGER\n" + subStr);
                        tag = "Numero";
                    }

                    else if (isRealNumber(subStr) == true)
                    {
                        Debug.Log("'%s' IS A REAL NUMBER\n" + subStr);
                        tag = "Numero";
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
                        currentNode = CreateNode(tag, subStr);
                    }
                }
            }
            else
            {
                if (isDelimiter(str[right - 1]) == true && left == right)
                {
                    subStr = str[right].ToString();
                    if (subStr == ";" || subStr == "{" || subStr == "}")
                        flagDT = false;

                    if (isOperator(str[right]) == true)
                    {
                        Debug.Log("'%c' IS AN OPERATOR: \n" + str[right]);
                        tag = "Operador";
                        if (!lineHasError)
                        {
                            currentNode = CreateNode(tag, subStr);
                        }
                    }
                    else if (str[right] != (' '))
                    {
                        Debug.Log("'%c' IS A DELIMITER: \n" + str[right]);
                        tag = "Separador";
                        if (!lineHasError)
                        {
                             currentNode =CreateNode(tag, subStr);
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

                    else if (!flagDT && isDataType(subStr) == true)
                    {
                        Debug.Log("'%s' IS A KEYWORD\n" + subStr);
                        tag = "TipoDato";
                        flagDT = true;
                    }

                    else if (isInteger(subStr) == true)
                    {
                        Debug.Log("'%s' IS AN INTEGER\n" + subStr);
                        tag = "Numero";
                    }

                    else if (isRealNumber(subStr) == true)
                    {
                        Debug.Log("'%s' IS A REAL NUMBER\n" + subStr);
                        tag = "Numero";
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
                        currentNode = CreateNode(tag, subStr);
                    }
                }
            }
        }

        TokenValidator.instance.TokenValidation(currentNode);

        if (errors != null)
        {
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
            UIController.instance.SetErrorText(lineNumber);
        }
        return;
    }

    public Node CreateNode(string type, string line)
    {
        SinglyLinkedListController.instance.AddNode(type, line);
        UIController.instance.CreateUINode();
        return SinglyLinkedListController.instance.singlyLinkedList.GetLastNode();
    }
}
