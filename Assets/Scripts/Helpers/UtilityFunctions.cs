using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public static class UtilityFunctions
{
    public static int[] CovertNumbersToDigits(int number)
    {
        int numberOfDigits = (int)Mathf.Floor(Mathf.Log10(number * 1)) + 1;
        int[] digits = new int[numberOfDigits];
        for (int i = 0; i < numberOfDigits; i++)
        {
            int digit = (int)((number / Mathf.Pow(10, i)) % 10);
            digits[i] = digit;
        }

        return digits;
    }

    public static void CovertNumbersToImage(int number, Sprite[] digitImages, Image[] gameObjects)
    {
        int numberOfDigits = (int)Mathf.Floor(Mathf.Log10(number * 1)) + 1;
        for (int i = 0; i < numberOfDigits; i++)
        {
            int digit = (int)((number / Mathf.Pow(10, i)) % 10);
            Image image = gameObjects[i];
            image.sprite = digitImages[digit];
            image.SetNativeSize();
        }
    }
}
