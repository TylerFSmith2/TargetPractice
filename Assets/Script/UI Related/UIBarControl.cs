using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarControl : MonoBehaviour
{

    public Slider slider;
    public Image fill;


    public float cooldownBar;
    public BoolVar timePauseSO;
    public IntVar cdSO;
    public BoolVar gameActiveVar;

    public IntVar BounceAllTimerSO;
    public IntVar RemoveBallTimerSO;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        switch (name)
        {
            case "RemoveBallBar":
                cooldownBar = RemoveBallTimerSO.value;
                break;
            case "BounceAllBar":
                cooldownBar = BounceAllTimerSO.value;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameActiveVar.value) { return; }
        if (cooldownBar > cdSO.value)
        {
            cooldownBar = cdSO.value;
        }
        float timeLeft = 0.1f + (cooldownBar - cdSO.value) * (0.1f - 1.0f) / (cdSO.value - 1);

        if (timeLeft < 1.0f)
        {
            cooldownBar -= Time.deltaTime;
            fill.color = new Color(0.55f, 0f, 0f);
            slider.value = timeLeft;
        }
        else
        {
            if (timeLeft > 1.0f && !timePauseSO.value)
            {
                fill.color = new Color(0f, 0.37f, 0f);
            }
        }

        slider.value = timeLeft;

    }


    public void ResetCD(float dur)
    {
        cooldownBar = dur;
    }
}
