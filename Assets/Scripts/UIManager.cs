using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text mobsLeftText;

    void Update()
    {
        mobsLeftText.text = MobGenerator.singleton.getNumberOfMobs().ToString();
    }
}
