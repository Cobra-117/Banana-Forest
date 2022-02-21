using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    public GLOBAL global;

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
        txt.text = global.money.ToString();
    }
}
