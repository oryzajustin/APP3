using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LeaderBoardScript : MonoBehaviour
{

    protected FileInfo file = null;
    protected StreamReader reader = null;
    protected string text = " "; // assigned to allow first line to be read below
    public Text UItext;
    public ScrollRect scrollzone;

    void Start()
    {
        file = new FileInfo("./Assets/Scripts/scorefile.txt");
        reader = file.OpenText();
        //UItext = GetComponent<Text>();
        //scrollzone = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (text != null)
        {
            text = reader.ReadLine();
            UItext.text = UItext.text + text + "\n";
        }
        if (scrollzone.verticalNormalizedPosition > 0.5f)
        {
            scrollzone.verticalNormalizedPosition = 0.5f;
        }
    }
}
