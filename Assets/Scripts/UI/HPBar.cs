using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] private List<GameObject> _HPoints;
    [SerializeField] private List<GameObject> _APoints;

    private void OnEnable()
    {
        Player.OnHealthChanged.AddListener(SetHPAmount);
        Player.OnArmorChanged.AddListener(SetArmorAmount);
    }

    public void SetHPAmount(int amount)
    {
        if (amount > _HPoints.Count)
            Debug.LogAssertion("Too many HP");
        for (int i = 0; i < _HPoints.Count; i++)
        {
            if (i < amount)
                _HPoints[i].SetActive(true);
            else
                _HPoints[i].SetActive(false);
        }
    }

    public void SetArmorAmount(int amount)
    {
        if (amount > _APoints.Count)
            Debug.LogAssertion("Too many Armor");
        for (int i = 0; i < _APoints.Count; i++)
        {
            if (i < amount)
                _APoints[i].SetActive(true);
            else
                _APoints[i].SetActive(false);
        }
    }
}
