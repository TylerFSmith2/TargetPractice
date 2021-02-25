using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveBall : MonoBehaviour
{

    //SO that holds the value for if the next ball should be snapped
    public BoolVar deleteBallSO;

    public UIBarControl barCD;
    public IntVar barCDMax;

    public Texture2D crosshair;

    void Start()
    {
        barCD = this.GetComponent<UIBarControl>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RemoveBallActive();
            Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);
            Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);
        }
    }

    public void RemoveBallActive()
    {
        if(barCD.cooldownBar <= 1.0f)
        {
            deleteBallSO.value = true;
        }
    }
}
