using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelectUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    public Difficulty GetSelectedDifficulty()
    {
        return (Difficulty)dropdown.value;
    }
}
