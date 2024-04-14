using UnityEngine;

public enum FireType
{
    Spray,
    Single
}
[CreateAssetMenu(menuName="WeaponData")]
public class WeaponData : ScriptableObject
{
    public string Name;
    public int Index;
    public Sprite Sprite;
    public int MaxAmmoIn;
    public int AllAmmo;
    public float FireRate;
    public float ReloadTime;
    public bool WideScope;
    public FireType FireType;
}
