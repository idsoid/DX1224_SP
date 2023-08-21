using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<string> dialogueLines;

    [SerializeField]
    private TMP_Text text;

    private int current;
    private int strIndex;

    private string line;

    private bool bFinishedLine;
    void Start()
    {
        line = "";
        current = 0;
        strIndex = 0;
        bFinishedLine = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && bFinishedLine)
        {
            Next();
            
        }
    }

    private void FixedUpdate()
    {
        if(!bFinishedLine)
        {
            line += dialogueLines[current][strIndex];
            text.text = line;
            Debug.Log(line);
            if (line == dialogueLines[current])
            {
                bFinishedLine = true;
            }
            else
            {
                strIndex++;
            }
        }
    }
    private void Next()
    {
        current++;
        if (current == dialogueLines.Count)
        {
            Destroy(gameObject);
        }
        strIndex = 0;
        line = "";
        bFinishedLine = false;
    }
}
