using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject SelectedGameob { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        SelectedGameob = null;
    }
}
