using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides utility functions for number-to-digit and number-to-image conversions.
/// </summary>
public static class UtilityFunctions
{
    /// <summary>
    /// Converts an integer number into a list of its digits.
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <returns>List of digits representing the number.</returns>
    public static List<int> CovertNumbersToDigits(int number)
    {
        List<int> digits = new List<int>();
        for (int i = 0; i < GetNumberOfDigits(number); i++)
        {
            int digit = (int)((number / Mathf.Pow(10, i)) % 10);
            digits[i] = digit; // Assigns the digit at the correct position
        }
        return digits;
    }

    /// <summary>
    /// Converts a number to digit images and assigns them to UI Image components.
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <param name="digitImages">List of digit sprites (0-9).</param>
    /// <param name="gameObjects">List of Image components to update.</param>
    /// <param name="parent">Optional parent transform for the images.</param>
    public static void CovertNumbersToImage(int number, List<Sprite> digitImages, List<Image> gameObjects, Transform parent = null)
    {
        for (int i = 0; i < GetNumberOfDigits(number); i++)
        {
            int digit = (int)((number / Mathf.Pow(10, i)) % 10);
            Image image = gameObjects[i];
            image.sprite = digitImages[digit];
            image.SetNativeSize();
            if (parent != null)
            {
                image.gameObject.transform.SetParent(parent, false);
            }
        }
    }

    /// <summary>
    /// Returns the number of digits in an integer.
    /// </summary>
    /// <param name="num">The number to check.</param>
    /// <returns>Number of digits.</returns>
    public static int GetNumberOfDigits(int num)
    {
        return num > 0 ? ((int)Mathf.Floor(Mathf.Log10(num * 1)) + 1) : 1;
    }
}
