using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrocarTutorial : MonoBehaviour {
    private GameObject[] tutoriais;

    private void Awake() {
        int numTutoriais = gameObject.transform.childCount;
        tutoriais = new GameObject[numTutoriais];

        for (int i = 0; i < numTutoriais; i++)
        {
            tutoriais[i] = transform.GetChild(i).gameObject;
        }

        Debug.Log("Número de tutoriais " + tutoriais.Length);
    }

    public void Trocas(int i) {
        tutoriais[i].SetActive(true);
        GameState.GerenteEstado(1);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(GetComponentInChildren<Button>().gameObject);
    }
}
