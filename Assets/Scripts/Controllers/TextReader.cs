using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextReader : MonoBehaviour
{
    #region singleton
    public static TextReader instance;
    public VariableGrammar varGram;
    public CycleGrammar cycGram;

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

    private void Start()
    {
        varGram = new VariableGrammar();
        cycGram = new CycleGrammar();
    }

    public int lineNumber = 0;

    public void Recorrer(string _lineToRead)
    {
        string line = _lineToRead;
        string aLine = null;
        System.IO.StringReader strReader = new StringReader(line);

        SinglyLinkedListController.instance.CreateSinglyLinkedList();
        lineNumber = 0;
        ErrorController.instance.SetLineHasError(false);
        while (true)
        {
            lineNumber += 1;
            aLine = strReader.ReadLine();
            if (aLine == null) break;
            UIController.instance.CreateContainer();
            TokenDetector.instance.Parse(aLine, lineNumber);
        }

        bool lineHasError = ErrorController.instance.GetLineHasError();

        StructureValidator.instance.StructureValidation();
        if(StructureValidator.instance.errors != null)
        {
            Destroy(UIController.instance.temporalContainer);
            UIController.instance.distanceY = UIController.instance.distanceY - 5;
            SinglyLinkedListController.instance.ResetSinglyLinkedList();
        }

        if (lineHasError)
        {
            Destroy(UIController.instance.temporalContainer);
            UIController.instance.distanceY = UIController.instance.distanceY - 5;
            SinglyLinkedListController.instance.ResetSinglyLinkedList();
        }
    }
}