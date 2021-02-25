using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{

    public GameObject instructionPanel;
    public Button openMenuButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseInstructions()
    {
        instructionPanel.SetActive(false);
        openMenuButton.gameObject.SetActive(true);
    }
    public void OpenInstructions()
    {
        openMenuButton.gameObject.SetActive(false);
        instructionPanel.SetActive(true);
    }
}
