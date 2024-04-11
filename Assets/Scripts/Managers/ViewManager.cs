using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private List<View> _views;

    private void Start()
    {
        foreach (var view in _views)
        {
            view.Init();
        }
    }
}
