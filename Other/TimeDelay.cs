using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets
{
    public class TimeDelay
    {
        public static bool IsLoadSaveDelayEnd { get; set; }
        public static bool IsSaveConfirmDelayEnd { get; set; }

        public static IEnumerator WaitForLoadSaveDelay(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            IsLoadSaveDelayEnd = true;
        }

        public static IEnumerator WaitForSaveConfirmationDelay(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            IsSaveConfirmDelayEnd = true;
        }
    }
}
