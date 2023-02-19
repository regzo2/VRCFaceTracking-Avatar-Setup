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
        public UserEyeParameter RightGaze = new UserEyeParameter 
        { 
            Name = "EyeRight",
            lookUpShape = "EyeLookUpRight",
            lookDownShape = "EyeLookDownRight",
            lookRightShape = "EyeLookOutRight",
            lookLeftShape = "EyeLookInRight"
        };
        [SerializeField]
        public UserEyeParameter LeftGaze = new UserEyeParameter 
        { 
            Name = "EyeLeft",
            lookUpShape = "EyeLookUpLeft",
            lookDownShape = "EyeLookDownLeft",
            lookRightShape = "EyeLookInLeft",
            lookLeftShape = "EyeLookOutLeft"
        };
        [SerializeField]
        public List<UserConversionParameter> Parameters = new List<UserConversionParameter>();
        public List<IUserConversionParameter> GetAllParameters() => new List<IUserConversionParameter> { RightGaze, LeftGaze }.Concat(Parameters).ToList();
    }
}
