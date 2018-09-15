using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCountHandler : MonoBehaviour {

    [SerializeField] int numberOfShields = 5;
    [SerializeField] int currentShieldCount;

    public static ShieldCountHandler Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentShieldCount = numberOfShields;

        Instance.UpdateShieldCountInUI();
    }

    void Update()
    {
        UpdateShieldCountInUI();
    }

    public void UseUpOneShield()
    {
        currentShieldCount -= 1;
        Instance.UpdateShieldCountInUI();
    }

    public void ResetShields()
    {
        currentShieldCount = numberOfShields;
        Instance.UpdateShieldCountInUI();
    }

    public int GetCurrentShieldCount()
    {
        return currentShieldCount;
    }

    public void UpdateShieldCountInUI()
    {
        if (GameObject.FindGameObjectWithTag("ShieldText") != null)
        {
            GameObject.FindGameObjectWithTag("ShieldText").GetComponent<TMPro.TextMeshProUGUI>().text = currentShieldCount.ToString();
        }

        //for(var i = 0; i < numberOfShields - currentShieldCount; i++)
        //{
        //    Destroy(instantiatedShields[instantiatedShields.Count - i - 1]);
        //}
    }
}
