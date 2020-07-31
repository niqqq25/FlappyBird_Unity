using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class NumberConverter
{

    static Image[] numbers;

    public static void Initialize()
    {
        numbers = Resources.LoadAll<Image>("Prefabs/UI/Numbers");
    }

    public static List<Image> Convert(int number)
    {
        List<Image> numberObjects = new List<Image>();

        if (number == 0)
        {
            numberObjects.Add(Object.Instantiate(numbers[0]));
        }
        else
        {
            while (number != 0)
            {
                int numberIndex = number % 10;
                numberObjects.Add(Object.Instantiate(numbers[numberIndex]));
                number /= 10;
            }
            numberObjects.Reverse();
        }

        return numberObjects;
    }
}
