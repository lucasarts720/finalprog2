using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlMgr : MonoBehaviour
{
    public string pNombreNivel;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void OnApplicationFocus(bool ApplicationIsBack)
    {
        if (ApplicationIsBack == true)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    public void CargaNivel(string pNombreNivel)
    {
        SceneManager.LoadScene(pNombreNivel);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salio del juego");
    }
}
 