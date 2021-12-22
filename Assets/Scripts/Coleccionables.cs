using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionables : MonoBehaviour
{
    private GameObject Coleccionable;
    private Jugador player;

    private void Start()
    {
        player = FindObjectOfType<Jugador>(true);
        Coleccionable = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.coleccionables += 1;
            Destroy(this);
            Destroy(Coleccionable);
        }
    }
}