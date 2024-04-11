using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] private List<GameObject> _HPoints;

    private void OnEnable()
    {
        Player.OnHealthChanged.AddListener(SetHPAmount);
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
}
