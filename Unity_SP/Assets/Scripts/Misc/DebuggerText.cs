using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class DebuggerText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text debugtext;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private EnemyData enemyData;

    [SerializeField]
    private Sprite testsprite;

    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //debugtext.text = playerData.PrintSelf();
    }
}
