using UnityEngine;

public class LerpMovement : MonoBehaviour 
{
    private Vector3 endPosition = new Vector3(0, 0, 5);
    private Vector3 startPosition;
    private float desiredDuration = 3f;
    private float elapsedTime;
    private bool movingForward = true; // Flag to track movement direction
    [SerializeField]
    private AnimationCurve curve;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (movingForward)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;

            // Use AnimationCurve to adjust interpolation curve
            float curveValue = curve.Evaluate(percentageComplete);
            // Perform the interpolation
            transform.position = Vector3.Lerp(startPosition, endPosition, curveValue);

            // Check if movement is complete
            if (percentageComplete >= 1.0f)
            {
                // Reset elapsed time and change direction
                elapsedTime = 0f;
                movingForward = false;
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;

            // Use AnimationCurve to adjust interpolation curve
            float curveValue = curve.Evaluate(percentageComplete);
            // Perform the interpolation
            transform.position = Vector3.Lerp(endPosition, startPosition, curveValue);

            // Check if movement is complete
            if (percentageComplete >= 1.0f)
            {
                // Reset elapsed time and change direction
                elapsedTime = 0f;
                movingForward = true;
            }
        }
    }
}
