using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCurrency : MonoBehaviour
{
    public int count;
    public TextMeshProUGUI text;

    void Start() {
        text.SetText("$" + count.ToString());
    }

    public void changeMoney(int change) {
        count += change;
        text.SetText("$" + count.ToString());
    }
}
