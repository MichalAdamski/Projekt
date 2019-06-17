using Assets.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Control
{
    public static class Inertia
    {
        private static Vector2 inertiaInputRightHand;
        private static List<Vector2> inputListRightHand;

        private static Vector2 inertiaInputLeftHand;
        private static List<Vector2> inputListLeftHand;

        static Inertia()
        {
            inertiaInputRightHand = Vector2.zero;
            inputListRightHand = new List<Vector2>();

            inertiaInputLeftHand = Vector2.zero;
            inputListLeftHand = new List<Vector2>();
        }

        public static Vector2 PerformInertiaRightHand(Vector2 input)
        {
            if (input.x == 0 || input.y == 0)
            {
                CalculateInertiaRight();
                return inertiaInputRightHand;
            }
            else
            {
                SaveInputRight(input);
                return Vector2.zero;
            }
        }

        public static Vector2 PerformInertiaLeftHand(Vector2 input)
        {
            if (input.x == 0 || input.y == 0)
            {
                CalculateInertiaLeft();
                return inertiaInputLeftHand;
            }
            else
            {
                SaveInputLeft(input);
                return Vector2.zero;
            }
        }

        private static void SaveInputLeft(Vector2 input)
        {

            inputListLeftHand.Add(input);
            if (inputListLeftHand.Count > 4)
            {
                inputListLeftHand.RemoveAt(0);
            }

            inertiaInputLeftHand = inputListLeftHand[0];
        }

        private static void SaveInputRight(Vector2 input)
        {
            inputListRightHand.Add(input);
            if (inputListRightHand.Count > 4)
            {
                inputListRightHand.RemoveAt(0);
            }

            inertiaInputRightHand = inputListRightHand[0];
        }

        private static void CalculateInertiaRight()
        {
            CalculateInertiaRightX();
            CalculateInertiaRightY();
        }

        private static void CalculateInertiaRightY()
        {
            if (inertiaInputRightHand.y > 0)
            {
                inertiaInputRightHand.y -= ControlParameters.stopSpeedRightHand;
                if (inertiaInputRightHand.y < 0)
                {
                    inertiaInputRightHand.y = 0;
                }
            }
            else if (inertiaInputRightHand.y < 0)
            {
                inertiaInputRightHand.y += ControlParameters.stopSpeedRightHand;
                if (inertiaInputRightHand.y > 0)
                {
                    inertiaInputRightHand.y = 0;
                }
            }
        }

        private static void CalculateInertiaRightX()
        {
            if (inertiaInputRightHand.x > 0)
            {
                inertiaInputRightHand.x -= ControlParameters.stopSpeedRightHand;
                if (inertiaInputRightHand.x < 0)
                {
                    inertiaInputRightHand.x = 0;
                }
            }
            else if (inertiaInputRightHand.x < 0)
            {
                inertiaInputRightHand.x += ControlParameters.stopSpeedRightHand;
                if (inertiaInputRightHand.x > 0)
                {
                    inertiaInputRightHand.x = 0;
                }
            }
        }

        private static void CalculateInertiaLeft()
        {
            CalculateInertiaLeftX();
            CalculateInertiaLeftY();
        }

        private static void CalculateInertiaLeftY()
        {
            if (inertiaInputLeftHand.y > 0)
            {
                inertiaInputLeftHand.y -= ControlParameters.stopSpeedLeftHand;
                if (inertiaInputLeftHand.y < 0)
                {
                    inertiaInputLeftHand.y = 0;
                }
            }
            else if (inertiaInputLeftHand.y < 0)
            {
                inertiaInputLeftHand.y += ControlParameters.stopSpeedLeftHand;
                if (inertiaInputLeftHand.y > 0)
                {
                    inertiaInputLeftHand.y = 0;
                }
            }
        }

        private static void CalculateInertiaLeftX()
        {
            if (inertiaInputLeftHand.x > 0)
            {
                inertiaInputLeftHand.x -= ControlParameters.stopSpeedLeftHand;
                if (inertiaInputLeftHand.x < 0)
                {
                    inertiaInputLeftHand.x = 0;
                }
            }
            else if (inertiaInputLeftHand.x < 0)
            {
                inertiaInputLeftHand.x += ControlParameters.stopSpeedLeftHand;
                if (inertiaInputLeftHand.x > 0)
                {
                    inertiaInputLeftHand.x = 0;
                }
            }
        }
    }
}
