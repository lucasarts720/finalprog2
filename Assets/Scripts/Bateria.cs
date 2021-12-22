using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bateria : MonoBehaviour
{
    private GameObject DaBaterias;
    private Jugador player;
    public int cantDada = 3;

    private void Start()
    {
        player = FindObjectOfType<Jugador>(true);
        DaBaterias = this.gameObject;
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
            player.baterias += cantDada;
            Destroy(this);
            Destroy(DaBaterias);
        }
    }
}
