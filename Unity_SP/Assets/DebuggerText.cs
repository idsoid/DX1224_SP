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
        if(Input.GetKeyDown(KeyCode.V))
        {
            enemyData.Init(100,10, testsprite, "test enemy");
            enemyData.Save();
            playerData.SavePos(player.transform.position);
            SceneManager.LoadScene("CombatScene");
        }
        debugtext.text = playerData.PrintSelf();
    }
}
