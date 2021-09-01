using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Math
{
    public static int GetElementIndexByWeight(List<int> weights)
    {
        int randomNumber = Random.Range(0, weights.Sum() + 1);

        int indexOf = -1;
        int currentWeight = 0;
        for (int i = 0; i < weights.Count; i++)
        {
            if (i == 0)
            {
                if (randomNumber >= 0 && randomNumber <= weights[0])
                {
                    indexOf = 0;
                    break;
                }
            }
            else
            {
                if (randomNumber > currentWeight && randomNumber <= currentWeight + weights[i])
                {
                    indexOf = i;
                    break;
                }
            }
            currentWeight += weights[i];
        }

        return indexOf;
    }
}
