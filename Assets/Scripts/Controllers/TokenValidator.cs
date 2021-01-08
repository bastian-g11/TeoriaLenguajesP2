using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenValidator : MonoBehaviour
{
    #region singleton
    public static TokenValidator instance;
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

    public string TokenValidation(Node node)
    {
        string classType = node.GetClassType();
        string value = node.GetValue();
        string errors = null;
        switch(classType)
        {
            case "KeyWord":
                if (!TokenDetector.instance.isKeyword(value))
                    errors = ""
                break;

            case "TipoDato":
                if (TokenDetector.instance.isDataType(value))
                    return true;
                break;

            case "Numero":
                if (double.TryParse(value, out double number))
                    return true;
                break;

            case "Boolean":
                if (TokenDetector.instance.isBoolean(value))
                    return true;
                break;

            case "Variable":
                if (TokenDetector.instance.isKeyword(value))
                {
                    Debug.Log("FALLÓ, NO ES UNA VARIABLE, ES UNA KW");
                    return false;
                }
                else if (TokenDetector.instance.isDataType(value))
                {
                    Debug.Log("FALLÓ, NO ES UNA VARIABLE, ES UN TIPO DE DATO");
                    return false;
                }
                break;

            case "Operador":
                if (TokenDetector.instance.isOperator(value[0]))
                    return true;
                break;

            case "Separador":
                if (value == ";" || value == "{" || value == "}" ||
                    value == "(" || value == ")" || value == ";")
                    return true;
                break;
        }
        return false;
    }
}
