using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    public Volume volume; // ������� ��� Volume � ����������

    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private PaniniProjection paniniProjection;

    private float chromaticAberrationTarget = 1f;
    private float lensDistortionTarget = -0.55f;
    private float paniniProjectorTarget = 1f;

    private float transitionDuration = 20f; // ������������ �������� � ��������

    public bool startTransition; // ������ �������

    private void Start()
    {
        startTransition = false;
        // �������� ���������� ChromaticAberration � LensDistortion �� Volume
        if (!volume.profile.TryGet(out chromaticAberration))
        {
            chromaticAberration = volume.profile.Add<ChromaticAberration>();
        }

        if (!volume.profile.TryGet(out lensDistortion))
        {
            lensDistortion = volume.profile.Add<LensDistortion>();
        }

        if (!volume.profile.TryGet(out paniniProjection))
        {
            paniniProjection = volume.profile.Add<PaniniProjection>();
        }
    }

    private void Update()
    {
        if (startTransition)
        {
            StartCoroutine(ChangeParametersOverTime());
            startTransition = false; // ����� ����� ������ ��������
        }
    }

    private IEnumerator ChangeParametersOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float progress = elapsedTime / transitionDuration;

            // �������� ������������ �� �������� �������� � ��������
            chromaticAberration.intensity.value = Mathf.Lerp(0f, chromaticAberrationTarget, progress);
            lensDistortion.intensity.value = Mathf.Lerp(0f, lensDistortionTarget, progress);
            paniniProjection.distance.value = Mathf.Lerp(0f, paniniProjectorTarget, progress);

            elapsedTime += Time.deltaTime;


            yield return null;
        }

        // ��������� �������� �������� ����� ���������� ��������
        chromaticAberration.intensity.value = chromaticAberrationTarget;
        lensDistortion.intensity.value = lensDistortionTarget;
        paniniProjection.distance.value = paniniProjectorTarget;

        // ����� ����� ��������� ��������

    }

    // ������ ��� ��������� ������� ��������
    public void SetChromaticAberrationTarget(float value)
    {
        chromaticAberrationTarget = value;
    }

    public void SetLensDistortionTarget(float value)
    {
        lensDistortionTarget = value;
    }

    public void SetPaniniProjectorTarget(float value)
    {
        paniniProjectorTarget = value;
    }
}
