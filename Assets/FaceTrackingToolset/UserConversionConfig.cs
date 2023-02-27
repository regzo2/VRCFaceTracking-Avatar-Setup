using System;
using System.Collections.Generic;
using UnityEngine;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.UserConversionParameter;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VRCFaceTracking.Tools.Avatar_Setup.Containers
{
    [Serializable]
    public class UserConversionConfig : ScriptableObject
    {
        [SerializeField]
        public UserEyeParameter RightEye = new UserEyeParameter 
        { 
            Name = "Right",
            lookUpShape = "EyeLookUpRight",
            lookDownShape = "EyeLookDownRight",
            lookRightShape = "EyeLookOutRight",
            lookLeftShape = "EyeLookInRight",
            pupilDiameter = "PupilDiameterRight",
            openness = "EyeClosedRight"
        };
        [SerializeField]
        public UserEyeParameter LeftEye = new UserEyeParameter 
        { 
            Name = "Left",
            lookUpShape = "EyeLookUpLeft",
            lookDownShape = "EyeLookDownLeft",
            lookRightShape = "EyeLookInLeft",
            lookLeftShape = "EyeLookOutLeft",
            pupilDiameter = "PupilDiameterLeft",
            openness = "EyeClosedLeft"
        };
        [SerializeField]
        public List<UserConversionParameter> Parameters = new List<UserConversionParameter>();
        public List<IUserConversionParameter> GetAllParameters() => new List<IUserConversionParameter> { RightEye, LeftEye }.Concat(Parameters).ToList();
    }
}
