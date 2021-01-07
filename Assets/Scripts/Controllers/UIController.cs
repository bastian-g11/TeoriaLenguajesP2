using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text txt;
    public Text errorText;
    public string lineaTexto;
    public GameObject go_uiNode;
    public Node createdNode;
    public GameObject errorsCanvas;
    public GameObject listsCanvas;
    public Vector3 cameraPosition;
    public CameraMovement cameraMovement;
    public bool isFile = false;
    public GameObject temporalContainerPrefab;
    public GameObject temporalContainer;
    public GameObject prefabListContainer;
    public GameObject listContainer;
    public int distanceX = 4;
    public int distanceY = 0;


    #region singleton
    public static UIController instance;
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

    public void SetText()
    {
        lineaTexto = txt.text;
        lineaTexto.ToString();
        Debug.Log(lineaTexto);
        //AutomataController.instance.index = 0;
        errorText.text = " ";
        distanceY = 0;
        Destroy(temporalContainer);
        temporalContainer = Instantiate(temporalContainerPrefab);
        isFile = true;
        TextReader.instance.Recorrer(lineaTexto);
    }

    public void SetErrorText(int lineNumber)
    {
        if (ErrorController.instance.GetLineHasError())
        {
            errorText.text = errorText.text + "Errores en la línea: " + lineNumber + " \n" +
                        ErrorController.instance.GetLineErrors();
            ErrorController.instance.RestartErrors();
        }
    }

    public void ShowLinkedLists()
    {
        if(isFile)
        {
            Camera.main.transform.position = cameraPosition;
            if(temporalContainer != null)
                temporalContainer.SetActive(true);
            errorsCanvas.SetActive(false);
            listsCanvas.SetActive(true);
            cameraMovement.enabled = true;
        }
    }

    public void ShowLineErrors()
    {
        if (temporalContainer != null)
            temporalContainer.SetActive(false);
        errorsCanvas.SetActive(true);
        listsCanvas.SetActive(false);
        cameraMovement.enabled = false;
    }

    public void CreateUINode()
    {
        GameObject _go = Instantiate(go_uiNode, new Vector3(1 * distanceX, 1 * -distanceY, 0), Quaternion.identity, listContainer.transform);
        distanceX = distanceX + 8;
        UINode _uiNode = _go.GetComponent<UINode>();
        createdNode.SetUINode(_uiNode);
        _uiNode.SetUINode(createdNode);
    }

    public void CreateContainer()
    {
        distanceX = 4;
        listContainer = Instantiate(prefabListContainer, temporalContainer.transform);
        distanceY = distanceY + 5;
    }
}
