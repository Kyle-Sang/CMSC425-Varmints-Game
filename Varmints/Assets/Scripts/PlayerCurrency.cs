using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCurrency : MonoBehaviour
{
    public int count;
    public TextMeshProUGUI text;

    private void Update() {
        text.SetText(count.ToString());
    }
}
