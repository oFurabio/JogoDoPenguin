using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selecao : MonoBehaviour
{
    public GameObject menPri, config;

    

    public void AbriuConfig()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(config);
    }

}
