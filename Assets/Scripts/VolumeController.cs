using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    public Volume volume; // Укажите ваш Volume в инспекторе

    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private PaniniProjection paniniProjection;

    private float chromaticAberrationTarget = 1f;
    private float lensDistortionTarget = -0.55f;
    private float paniniProjectorTarget = 1f;

    private float transitionDuration = 20f; // Длительность перехода в секундах

    public bool startTransition; // Начать переход

    private void Start()
    {
        startTransition = false;
        // Получаем компоненты ChromaticAberration и LensDistortion из Volume
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
            startTransition = false; // Сброс флага начала перехода
        }
    }

    private IEnumerator ChangeParametersOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float progress = elapsedTime / transitionDuration;

            // Линейная интерполяция от текущего значения к целевому
            chromaticAberration.intensity.value = Mathf.Lerp(0f, chromaticAberrationTarget, progress);
            lensDistortion.intensity.value = Mathf.Lerp(0f, lensDistortionTarget, progress);
            paniniProjection.distance.value = Mathf.Lerp(0f, paniniProjectorTarget, progress);

            elapsedTime += Time.deltaTime;


            yield return null;
        }

        // Установка конечных значений после завершения корутины
        chromaticAberration.intensity.value = chromaticAberrationTarget;
        lensDistortion.intensity.value = lensDistortionTarget;
        paniniProjection.distance.value = paniniProjectorTarget;

        // Сброс флага остановки перехода

    }

    // Методы для установки целевых значений
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
