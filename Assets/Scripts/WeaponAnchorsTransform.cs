using UnityEngine;

public enum AnimationType
{
    RunWithRifle,
    RifleFire
}

[CreateAssetMenu(menuName = "WeaponAnimAnchors")]
public class WeaponAnchorsTransform : ScriptableObject
{
    public AnimationType AnimationType;
    public Vector3 FirstPosition;
    public Vector3 FirstRotation;
    public Vector3 SecondPosition;
    public Vector3 SecondRotation;
}
