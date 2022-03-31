using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ferret.InGame.Presentation.View
{
    public sealed class VolumeView : MonoBehaviour
    {
        [SerializeField] private Slider bgmSlider = default;
        [SerializeField] private Slider seSlider = default;

        public IObservable<float> bgmValueChanged => bgmSlider.OnValueChangedAsObservable();
        public IObservable<float> seValueChanged => seSlider.OnValueChangedAsObservable();

        public IObservable<PointerEventData> bgmSliderPointerUp => bgmSlider.OnPointerUpAsObservable();
        public IObservable<PointerEventData> seSliderPointerUp => seSlider.OnPointerUpAsObservable();

        public float bgmVolume => bgmSlider.value;
        public float seVolume => seSlider.value;

        public void SetBgmVolume(float value) => bgmSlider.value = value;
        public void SetSeVolume(float value) => seSlider.value = value;

        public void SetVolume((float bgm, float se) volume)
        {
            SetBgmVolume(volume.bgm);
            SetSeVolume(volume.se);
        }
    }
}