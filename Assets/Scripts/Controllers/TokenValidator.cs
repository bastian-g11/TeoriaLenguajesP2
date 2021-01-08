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
        Debug.Log("Se procesa: " + value + " y su Tipo es: " + classType);
        switch(classType)
        {
            case "KeyWord":
                if (!TokenDetector.instance.isKeyword(value))
                    errors = "- No es una palabra reservada\n";
                break;

            case "TipoDato":
                if (!TokenDetector.instance.isDataType(value))
                    errors = "- Error en tipo de dato\n";
                break;

            case "Numero":
                if (!double.TryParse(value, out double number) && !value.Contains("E"))
                    errors = "- Error en número\n";

                break;

            case "Boolean":
                if (!TokenDetector.instance.isBoolean(value))
                    errors = "- Error en operador booleano\n";
                break;

            case "Variable":
                if (TokenDetector.instance.isKeyword(value))
                {
                    errors = "- Alguna variable contiene una palabra reservada\n";
                }
                else if (TokenDetector.instance.isDataType(value))
                {
                    errors = "- Nombre de alguna variable es un tipo de dato\n";
                }
                else if (!TokenDetector.instance.ValidIdentifier(value))
                {
                    errors = "- Nombre de alguna variable empieza de forma incorrecta\n";
                }
                break;

            case "Operador":
                if (!TokenDetector.instance.isOperator(value[0]))
                    errors = "- Error en algún operador\n";
                break;

            case "Separador":
                if (value[0] == ';' || value[0] == '{' || value[0] == '}' ||
                    value[0] == '(' || value[0] == ')' || value[0] == ';' ||
                    value[0] == ',' || value[0] == '[' || value[0] == ']')
                {
                    errors = null;
                }
                else
                {
                    errors = "- Error en algún separador\n";
                }
                break;
        }
        return errors;
    }
}
