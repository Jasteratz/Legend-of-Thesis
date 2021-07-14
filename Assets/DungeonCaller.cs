using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCaller : MonoBehaviour
{
    DungeonLoader instance;



    private void Start()
    {
        instance = gameObject.GetComponent<DungeonLoader>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        instance.LoadNextLevel();
    }
}
