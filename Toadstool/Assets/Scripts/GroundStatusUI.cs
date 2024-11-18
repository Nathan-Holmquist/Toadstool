using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundStatusUI : MonoBehaviour
{
    public Image groundStatusIndicator; // UI rectangle to indicate ground status

    public Color onGroundColor = Color.green; // Color when grounded
    public Color offGroundColor = Color.red; // Color when not grounded

    // Update is called once per frame
    void Update()
    {
        // Ensure the groundStatusIndicator is not null
        if (groundStatusIndicator != null)
        {
            // Access the static isGrounded variable from PlayerMovement
            groundStatusIndicator.color = PlayerMovement.isGrounded ? onGroundColor : offGroundColor;
        }
    }
}