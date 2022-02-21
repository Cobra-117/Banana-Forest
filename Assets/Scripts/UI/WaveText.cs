using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveText : MonoBehaviour
{
    private GLOBAL global;
    private TMPro.TextMeshProUGUI txt;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.FindGameObjectWithTag("Global").GetComponent<GLOBAL>();
        txt = this.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (global.maxWave == -1)
            txt.text = "Wave: " + global.curWave.ToString();
        else
            txt.text = "Wave: " + global.curWave.ToString() + "/" + global.maxWave.ToString();
    }
}
