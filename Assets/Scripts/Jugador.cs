using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Jugador : MonoBehaviour
{
    #region Variables
    public SphereCollider Damage;
    bool gettingDamage = false;
    public bool safe = false;
    public bool OnTrigger = false;
    public GameObject Trigger;
    public Light flashlight;
    bool IsOn = false;
    #endregion

    #region Ganar
    public GameObject Puerta1;
    public GameObject Puerta2;
    #endregion

    #region Tick Set
    public float health, sanity, battery;
    public float healthTick, sanityTick, batteryTick, sDmgTick, hDmgTick;
    public float healthMax, sanityMax, batteryMax;
    public Text healthNumber;
    public Text batteryNumber;
    public Slider healthBar;
    public Slider sanityBar;
    public Slider batteryBar;
    #endregion

    #region inventario
    public int coleccionables, baterias, coleccionablesMax;
    public TextMeshProUGUI tColeccionables, tBaterias;
    public GameObject Panel;
    #endregion

    private void Start()
    {
        #region Bar Set
        healthBar.maxValue = healthMax;
        sanityBar.maxValue = sanityMax;
        #endregion

        #region Inventory Set
        baterias = 0;
        coleccionables = 0;
        #endregion
    }

    private void Update()
    {
        #region Bars
        if (health < healthMax && sanity >= (sanityMax / 2)) health += Time.deltaTime / healthTick;
        if (gettingDamage == true) sanity -= Time.deltaTime * sDmgTick;
        if (sanity <= 0) health -= Time.deltaTime * hDmgTick;
        if (sanity < sanityMax && safe) sanity += Time.deltaTime / sanityTick;
        if (sanity >= 0 && !safe) sanity -= Time.deltaTime / sanityTick;
        if (battery >= 0 && IsOn) battery -= Time.deltaTime / batteryTick;

        healthBar.value = health;
        sanityBar.value = sanity;
        batteryBar.value = battery;
        healthNumber.text = Mathf.Round(health).ToString();
        batteryNumber.text = Mathf.Round(battery).ToString();
        #endregion

        #region Inventario
        tColeccionables.text = "Coleccionable(s): " + coleccionables.ToString() + " de " + coleccionablesMax.ToString();
        tBaterias.text = "Baterías Extra: " + baterias.ToString();
        #endregion

        #region Actions
        // Mostrar Inventario
        Panel.SetActive(Input.GetKey(KeyCode.Tab));
        if (health <= 0)
        {
            Die();
        }
        if (Input.GetKeyDown(KeyCode.E) && OnTrigger)
        {
            Interact();
        }
        if (Input.GetKeyDown(KeyCode.F) && battery >= 0)
        {
            Linterna();
        }
        if (Input.GetKeyDown(KeyCode.R) && baterias > 0 && battery < (batteryMax * 0.99f))
        {
            RecargarLinterna();
        }
        if (battery <= 0 && IsOn)
        {
            flashlight.gameObject.SetActive(false);
            IsOn = false;
        }
        if (coleccionables == coleccionablesMax)
        {
            AbrirPuerta();
        }

        #endregion
    }

    #region Functions
    void Interact()
    {
        Trigger.SendMessage("interactuar");
    }
    void Linterna()
    {
        if (IsOn)
        {
            flashlight.gameObject.SetActive(false);
            IsOn = false;
            safe = false;
        }
        else
        {
            flashlight.gameObject.SetActive(true);
            IsOn = true;
            safe = true;
        }
    }
    void RecargarLinterna()
    {
        baterias -= 1;
        battery = 100;
    }
    void Die()
    {
        SceneManager.LoadScene("DeathScene");
    }
    void AbrirPuerta()
    {
        Puerta1.SetActive(false);
        Puerta2.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gettingDamage = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gettingDamage = false;
        }
    }
    #endregion

}

