using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    Transform target;
    public Vector3 minValue, maxValue;

    void Start() {
        if (GameObject.FindGameObjectWithTag("Player")) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void FixedUpdate() {
        Follow();
    }

    void Follow() {
        if (target != null) {
            Vector3 targetPosition = target.position + new Vector3(0, 0, -10);

            Vector3 boundPosition = new Vector3(
                Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x),
                Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),
                Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z)
            );

            Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, 3.0f * Time.deltaTime);
            transform.position = smoothPosition;
        }
    }
}
