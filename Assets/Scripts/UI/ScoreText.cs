using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = this.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "Score: " + GLOBAL.score.ToString();
    }
}
