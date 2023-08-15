using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebuggerText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text debugtext;

    [SerializeField]
    private PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        debugtext.text = playerData.PrintSelf();
    }
}
