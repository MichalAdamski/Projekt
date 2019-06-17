using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Control
{
    public static class ControlParameters
    {

        public static float SkullRotationSpeed { get; private set; }
        public static float PlayerRotationSpeed { get; private set; }
        public static float MovementSpeed { get; private set; }
        public static bool Inertia { get; set; }
        public static float stopSpeedLeftHand { get; set; }
        public static float stopSpeedRightHand { get; set; }

        static ControlParameters()
        {
            SkullRotationSpeed = 40;
            PlayerRotationSpeed = 20;
            MovementSpeed = 20;
            Inertia = true;
            stopSpeedLeftHand = 0.02f;
            stopSpeedRightHand = 0.02f;
        }

        private static void ChangeSkullRotationSpeed(float value)
        {
            SkullRotationSpeed += value;

            if (SkullRotationSpeed > 200)
                SkullRotationSpeed = 200;

            else if (SkullRotationSpeed < 0)
                SkullRotationSpeed = 0;
        }

        private static void ChangePlayerRotationSpeed(float value)
        {
            PlayerRotationSpeed += value;

            if (PlayerRotationSpeed > 100)
                PlayerRotationSpeed = 100;

            else if (SkullRotationSpeed < 0)
                SkullRotationSpeed = 0;
        }

        private static void ChangeMovementSpeed(float value)
        {
            MovementSpeed += value;

            if (MovementSpeed > 100)
                MovementSpeed = 100;

            else if (MovementSpeed < 0)
                MovementSpeed = 0;
        }

        public static void PlayerSpeedIncrease()
        {
            ChangePlayerRotationSpeed(2);
            ChangeMovementSpeed(2);
        }

        public static void PlayerSpeedDecrease()
        {
            ChangePlayerRotationSpeed(-2);
            ChangeMovementSpeed(-2);
        }

        public static void SkullSpeedIncrease()
        {
            ChangeSkullRotationSpeed(4);
        }

        public static void SkullSpeedDecrease()
        {
            ChangeSkullRotationSpeed(-4);
        }
    }
}
