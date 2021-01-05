using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorController : MonoBehaviour
{
    [SerializeField]
    private string lineErrors;
    [SerializeField]
    private bool lineHasError;

    #region singleton
    public static ErrorController instance;

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


    public void SetErrorMessage(string error)
    {
        lineErrors = lineErrors + error;
    }

    public void RestartErrors()
    {
        lineHasError = false;
        lineErrors = null;
    }

    public bool GetLineHasError()
    {
        return lineHasError;
    }

    public void SetLineHasError(bool b)
    {
        lineHasError = b; 
    }

    public string GetLineErrors()
    {
        return lineErrors;
    }

    public void SetLineErrors(string s)
    {
        lineErrors = s;
    }
}
