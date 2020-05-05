using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFader : MonoBehaviour
{
         
        public Vector2 varRange = Vector2.up; 
        public float time = 1f;
        public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        public bool fadeToMaxOnStart = true;
        Vector3 originalScale;
        private void OnEnable()
        {
            originalScale = transform.localScale;
            if (fadeToMaxOnStart) FadeToMax();
        }

        public void FadeToMax () {
            StartCoroutine(_FadeToMax());
        }
    
        private IEnumerator _FadeToMax() {
            float currTime = 0f;
            while (currTime < time) {
                currTime += Time.deltaTime;
                float lerpVal = curve.Evaluate(Mathf.InverseLerp(0f, time, currTime));
                //r.material.SetFloat(propertyName, Mathf.Lerp(varRange.x, varRange.y, lerpVal));
                transform.localScale = originalScale * lerpVal;
                yield return new WaitForEndOfFrame();
            }
        }
    
        public void FadeToMin () {
            StartCoroutine(_FadeToMin());
        }
        private IEnumerator _FadeToMin() {
            float currTime = 0f;
            while (currTime < time) {
                currTime += Time.deltaTime;
                float lerpVal = curve.Evaluate(Mathf.InverseLerp(0f, time, currTime));
                //r.material.SetFloat(propertyName, Mathf.Lerp(varRange.y, varRange.x, lerpVal));
                transform.localScale = originalScale * (1f-lerpVal);
                yield return new WaitForEndOfFrame();
            }
        }
}
