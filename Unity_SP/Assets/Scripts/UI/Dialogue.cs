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

    private float fSpeed;

    private float fTime_elapsed;
    void Start()
    {
        line = "";
        current = 0;
        strIndex = 0;
        bFinishedLine = false;
        fTime_elapsed = 0f;
        SetSpeed(0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && bFinishedLine)
        {
            Next();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            bFinishedLine = true;
            text.text = dialogueLines[current];
        }

        if (!bFinishedLine)
        {
            fTime_elapsed += Time.deltaTime;
            if (fTime_elapsed > fSpeed)
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
                fTime_elapsed = 0f;
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

    public void SetSpeed(float sec)
    {
        fSpeed = sec;
    }
}
