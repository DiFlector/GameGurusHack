using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKControl : MonoBehaviour
{
    [SerializeField] private Transform _rightHandAnchor;
    [SerializeField] private Transform _leftHandAnchor;

    private Animator _animator;
    [SerializeField] private bool _IKActive;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if (_IKActive)
        {
            if (_rightHandAnchor != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandAnchor.position);
                _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandAnchor.rotation);
            }
            if (_leftHandAnchor != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandAnchor.position);
                _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandAnchor.rotation);
            }
        }
        else
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }
    }
}
