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

    

    // Returns 'true' if the character is a DELIMITER. 
    public bool isSeparator(char ch)
    {
        if (ch == ' ' || ch == '+' || ch == '-' || ch == '*' ||
            ch == '/' || ch == ',' || ch == ';' || ch == '>' ||
            ch == '<' || ch == '=' || ch == '(' || ch == ')' ||
            ch == '[' || ch == ']' || ch == '{' || ch == '}' ||
            ch == '%' || ch == '^' || ch == '\"' || ch == '\''||
            ch == '&' || ch == '|' || ch == '!')
        {
            return (true);
        }
        return (false);
    }

    public bool isBooleanSeparator(char ch)
    {
        if (ch == '&' || ch == '|' || ch == '!' ||
            ch == '<' || ch == '>' || ch == '=')
        {
            return (true);
        }
        return (false);
    }

    public bool isDelimiter(char ch)
    {
        if (ch == '(' || ch == ')' ||
            ch == '[' || ch == ']' || 
            ch == '{' || ch == '}' || 
            ch == '\"' || ch == '\'')
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
        if (ch == "&&" || ch == "||" || ch == "true" || ch == "false" ||
            ch == "<=" || ch == ">=" || ch == "==" || ch == "!=")
            return (true);
        return (false);
    }

    // Returns 'true' if the string is a VALID IDENTIFIER. 
    public bool ValidIdentifier(string str)
    {
        if (str[0] == '0' || str[0] == '1' || str[0] == '2' ||
            str[0] == '3' || str[0] == '4' || str[0] == '5' ||
            str[0] == '6' || str[0] == '7' || str[0] == '8' ||
            str[0] == '9' || isSeparator(str[0]) == true  || 
            isBooleanSeparator(str[0]) == true)
            return (false);
        return (true);
    }

    // Returns 'true' if the string is a KEYWORD. 
    public bool isKeyword(string str)
    {
        if (str.Equals("if") || str.Equals("else") || str.Equals("while"))
        {
            //Debug.Log("Es palabra clave");
            return (true);
        }
        return (false);
    }

    public bool isDataType(string str)
    {
        if (str.Equals("boolean") || str.Equals("String") ||
            str.Equals("int") || str.Equals("float"))
        {
            //Debug.Log("Es tipo de dato");
            return (true);
        }
        return (false);
    }

    // Returns 'true' if the string is an INTEGER. 
    public bool isInteger(string str)
    {
        int i, len = str.Length;
        bool hasE = false;
        //Debug.Log("<color=blue> Linea a procesar: </color>" + str);
        if (len == 0)
            return (false);
        for (i = 0; i < len; i++)
        {
            if (str[i] != '0' && str[i] != '1' && str[i] != '2'
                && str[i] != '3' && str[i] != '4' && str[i] != '5'
                && str[i] != '6' && str[i] != '7' && str[i] != '8'
                && str[i] != '9' && char.ToLower(str[i]) != 'e' && str[i] != '-'
                && str[i] != '+'
                || (str[i] == '-' && i > 0 && char.ToLower(str[i - 1]) != 'e')
                || (str[i] == '+' && i > 0 && char.ToLower(str[i - 1]) != 'e'))
                //|| (str[i] == '-' && i > 0))
                return (false);

            if (char.ToLower(str[i]) == 'e' && hasE)
                return (false);

            if (char.ToLower(str[len - 1]) == 'e' || 
                str[len - 1] == '+' || str[len - 1] == '-')
                return (false);
        }
        
        return (true);
    }

    // Returns 'true' if the string is a REAL NUMBER. 
    public bool isRealNumber(string str)
    {
        int i, len = str.Length;
        bool hasDecimal = false;
        bool hasE = false;

        if (len == 0)
            return (false);
        for (i = 0; i < len; i++)
        {
            if (str[i] != '0' && str[i] != '1' && str[i] != '2'
                && str[i] != '3' && str[i] != '4' && str[i] != '5'
                && str[i] != '6' && str[i] != '7' && str[i] != '8'
                && str[i] != '9' && str[i] != '.' && str[i] != 'E' 
                && str[i] != 'e' && str[i] != '-' && str[i] != '+'
                || (str[i] == '-' && i > 0 && char.ToLower(str[i - 1]) != 'e')
                || (str[i] == '+' && i > 0 && char.ToLower(str[i - 1]) != 'e'))
                //|| (str[i] == '-' && i > 0))
                return (false);

            if (str[i] == '.')
                hasDecimal = true;

            if ((str[i] == 'E' || str[i] == 'e') && hasE)
                return (false);

            if (char.ToLower(str[len - 1]) == 'e' ||
                str[len - 1] == '+' || str[len - 1] == '-')
                return (false);
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
        bool flagComilla = false;
        bool flagApostrofe = false;
        bool flagDT = false;
        string errors = null;
        string tag = null;
        string subStr = null;
        Node currentNode = null;

        while (right < len && left <= right)
        {
            
            if (isSeparator(str[right]) == false)
            {
                right++;
                if (right == len) isEnd = true;
            }
            
            if (!isEnd)
            {

                if (right > 0 && char.ToLower(str[right - 1]) == 'e'
                    && (str[right] == '-' || str[right] == '+'))
                        right++;

                if (isSeparator(str[right]) == true && left == right)
                {
                    subStr = str[right].ToString();
                    if ((right + 1 < len) && isBooleanSeparator(str[right]) && 
                        isBooleanSeparator(str[right + 1]))
                    {
                        subStr = str[right].ToString();
                        subStr = subStr + str[right + 1].ToString();
                        tag = "Boolean";
                        if (flagComilla || flagApostrofe)
                        {
                            tag = "Termino";
                        }
                        currentNode = CreateNode(tag, subStr);
                        errors = errors + TokenValidator.instance.TokenValidation(currentNode);
                        right = right + 2;
                        left = right;
                    }
                    else
                    {
                        if (subStr == ";" || subStr == "{" || subStr == "}")
                            flagDT = false;

                        if (isOperator(str[right]) == true)
                        {
                            //Debug.Log("'%c' IS AN OPERATOR: \n" + str[right]);
                            tag = "Operador";
                            if (flagComilla || flagApostrofe)
                            {
                                tag = "Termino";
                            }
                            currentNode = CreateNode(tag, subStr);
                            errors = errors + TokenValidator.instance.TokenValidation(currentNode);
                        }
                        else if (str[right] != (' '))
                        {
                            //Debug.Log("'%c' IS A DELIMITER: \n" + str[right]);
                            if (isDelimiter(str[right]))
                            {
                                tag = "Delimitador";
                                if (str[right] == '\"' && !flagApostrofe)
                                    flagComilla = !flagComilla;

                                if(str[right] == '\'' && !flagComilla)
                                    flagApostrofe = !flagApostrofe;

                                if ((str[right] == '\'' && flagComilla)
                                    || str[right] == '\"' && flagApostrofe)
                                {
                                    tag = "Termino";
                                }
                            }
                            else
                            {
                                tag = "Separador";
                                if (flagComilla || flagApostrofe)
                                {
                                    tag = "Termino";
                                }
                            }
                            currentNode = CreateNode(tag, subStr);
                            errors = errors + TokenValidator.instance.TokenValidation(currentNode);
                        }
                        right++;
                        left = right;
                    }

                    
                }
                else if ((right == len && left != right)
                         || (isSeparator(str[right]) == true && left != right)
                         || (isBooleanSeparator(str[right]) == true && left != right))
                {

                    subStr = SubString(str, left, right - 1);

                        if (isKeyword(subStr) == true)
                    {
                        //Debug.Log("'%s' IS A KEYWORD\n" + subStr);
                        tag = "KeyWord";
                    }

                    else if (!flagDT && isDataType(subStr) == true)
                    {
                        //Debug.Log("'%s' IS A KEYWORD\n" + subStr);
                        tag = "TipoDato";
                        flagDT = true;
                    }

                    else if (isInteger(subStr) == true)
                    {
                        //Debug.Log("'%s' IS AN INTEGER\n" + subStr);
                        tag = "Numero";
                    }

                    else if (isRealNumber(subStr) == true)
                    {
                        //Debug.Log("'%s' IS A REAL NUMBER\n" + subStr);
                        tag = "Numero";
                    }

                    else if (isBoolean(subStr) == true)
                    {
                        //Debug.Log("'%s' IS A BOOLEAN OPERATOR\n" + subStr);
                        tag = "Boolean";
                    }

                    else if (ValidIdentifier(subStr) == true
                            && isSeparator(str[right - 1]) == false)
                    {
                        //Debug.Log("'%s' IS A VALID IDENTIFIER\n" + subStr);
                        tag = "Variable";
                    }

                    else if (ValidIdentifier(subStr) == false
                            && isSeparator(str[right - 1]) == false)
                    {
                        //Debug.Log("'%s' IS NOT A VALID IDENTIFIER\n" + subStr);
                        tag = "Variable";
                    }

                    if (flagComilla || flagApostrofe)
                    {
                        tag = "Termino";
                    }

                    left = right;
                    currentNode = CreateNode(tag, subStr);
                    errors = errors + TokenValidator.instance.TokenValidation(currentNode);
                }
            }

//************************************************************************

            else
            {
                if (right > 0 && right < len && char.ToLower(str[right - 1]) == 'e'
                    && (str[right] == '-' || str[right] == '+'))
                {
                    //Debug.Log("<color=green> Es científico </color>");
                    right++;
                }

                if (isSeparator(str[right - 1]) == true && left == right)
                {
                    subStr = str[right].ToString();

                    if (isBooleanSeparator(str[right]) && 
                        isBooleanSeparator(str[right + 1]))
                    {
                        subStr = str[right].ToString();
                        subStr = subStr + str[right + 1].ToString();
                        tag = "Boolean";
                        if (flagComilla || flagApostrofe)
                        {
                            tag = "Termino";
                        }
                        currentNode = CreateNode(tag, subStr);
                        errors = errors + TokenValidator.instance.TokenValidation(currentNode);
                        right = right + 2;
                        left = right;
                    }
                    else
                    {
                        if (subStr == ";" || subStr == "{" || subStr == "}")
                            flagDT = false;

                        if (isOperator(str[right]) == true)
                        {
                            //Debug.Log("'%c' IS AN OPERATOR: \n" + str[right]);
                            tag = "Operador";
                            if (flagComilla || flagApostrofe)
                            {
                                tag = "Termino";
                            }
                            currentNode = CreateNode(tag, subStr);
                            errors = errors + TokenValidator.instance.TokenValidation(currentNode);
                        }
                        else if (str[right] != (' '))
                        {
                            //Debug.Log("'%c' IS A DELIMITER: \n" + str[right]);
                            if (isDelimiter(str[right]))
                            {
                                tag = "Delimitador";
                                if (str[right] == '\"' && !flagApostrofe)
                                    flagComilla = !flagComilla;

                                if (str[right] == '\'' && !flagComilla)
                                    flagApostrofe = !flagApostrofe;

                                if ((str[right] == '\'' && flagComilla)
                                    || str[right] == '\"' && flagApostrofe)
                                {
                                    tag = "Termino";
                                }
                            }
                            else
                            {
                                tag = "Separador";
                                if (flagComilla || flagApostrofe)
                                {
                                    tag = "Termino";
                                }
                            }
                            currentNode = CreateNode(tag, subStr);
                            errors = errors + TokenValidator.instance.TokenValidation(currentNode);
                        }
                        right++;
                        left = right;
                    }
                   
                }
                else if ((right == len && left != right)
                         || isSeparator(str[right - 1]) == true && left != right
                         || (isBooleanSeparator(str[right - 1]) == true && left != right))
                {

                    subStr = SubString(str, left, right - 1);

                    if (isKeyword(subStr) == true)
                    {
                        //Debug.Log("'%s' IS A KEYWORD\n" + subStr);
                        tag = "KeyWord";
                    }

                    else if (!flagDT && isDataType(subStr) == true)
                    {
                        //Debug.Log("'%s' IS A KEYWORD\n" + subStr);
                        tag = "TipoDato";
                        flagDT = true;
                    }

                    else if (isInteger(subStr) == true)
                    {
                        //Debug.Log("'%s' IS AN INTEGER\n" + subStr);
                        tag = "Numero";
                    }

                    else if (isRealNumber(subStr) == true)
                    {
                        //Debug.Log("'%s' IS A REAL NUMBER\n" + subStr);
                        tag = "Numero";
                    }

                    else if (isBoolean(subStr) == true)
                    {
                        //Debug.Log("'%s' IS A BOOLEAN OPERATOR\n" + subStr);
                        tag = "Boolean";
                    }

                    else if (ValidIdentifier(subStr) == true
                            && isSeparator(str[right - 1]) == false)
                    {
                        //Debug.Log("'%s' IS A VALID IDENTIFIER\n" + subStr);
                        tag = "Variable";
                    }

                    else if (ValidIdentifier(subStr) == false
                            && isSeparator(str[right - 1]) == false)
                    {
                        //Debug.Log("'%s' IS NOT A VALID IDENTIFIER\n" + subStr);
                        tag = "Variable";
                    }

                    if (flagComilla || flagApostrofe)
                    {
                        tag = "Termino";
                    }
                    left = right;
                    currentNode = CreateNode(tag, subStr);
                    errors = errors + TokenValidator.instance.TokenValidation(currentNode);
                }
            }
        }

        if (!string.IsNullOrEmpty(errors))
        {
            ErrorController.instance.SetErrorMessage(errors);
            ErrorController.instance.SetLineHasError(true);
            UIController.instance.SetErrorText(lineNumber);
            ErrorController.instance.SetLineHasError(false);
        }
        CreateNode("FinSecuencia", "¬");
        return;
    }

    public Node CreateNode(string type, string line)
    {
        SinglyLinkedListController.instance.AddNode(type, line);
        UIController.instance.CreateUINode();
        return SinglyLinkedListController.instance.singlyLinkedList.GetLastNode();
    }

}
