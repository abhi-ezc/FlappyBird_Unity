using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class UtilityFunctions
{
    public static List<int> CovertNumbersToDigits(int number)
    {
        List<int> digits = new List<int>();
        for (int i = 0; i < GetNumberOfDigits(number); i++)
        {
            int digit = (int)((number / Mathf.Pow(10, i)) % 10);
            digits[i] = digit;
        }

        return digits;
    }

    public static void CovertNumbersToImage(int number, List<Sprite> digitImages, List<Image> gameObjects, Transform parent = null)
    {
        for (int i = 0; i < GetNumberOfDigits(number); i++)
        {
            int digit = (int)((number / Mathf.Pow(10, i)) % 10);
            Image image = gameObjects[i];
            image.sprite = digitImages[digit];
            image.SetNativeSize();
            if(parent != null)
            {
                image.gameObject.transform.SetParent(parent, false);
            }
        }
    }

    public static int GetNumberOfDigits(int num)
    {
        return num > 0 ? ((int)Mathf.Floor(Mathf.Log10(num * 1)) + 1) : 1;
    }
}
