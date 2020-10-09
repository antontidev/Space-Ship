using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Rocket
{
    public enum RocketPartType
    {
        Top,
        Middle, 
        Bottom,
    }

    public class RocketPart : MonoBehaviour
    {
        public Vector3 OnRocketPosition
        {
            get; private set;
        }
    }
}