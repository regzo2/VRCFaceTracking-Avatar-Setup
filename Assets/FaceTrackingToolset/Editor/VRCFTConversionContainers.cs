using BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.VRCFTParameterContainers;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.FTAnimationContainers;

namespace VRCFaceTracking.Tools.Avatar_Setup.Containers
{
    public interface IUserConversionParameter
    {
        string Name { get; set; } // name of conversion that will populate avatar setup fields.
        IFTAnimation Animation { get; set; } // associated animation for said conversion (usually is directly a blendshape).
        List<FTParameter> ToUnifiedParameters(); // converts parameter to Unified parameters that exist in the VRCFTParameterContainers.
    }

    [Serializable]
    public class UserConversionParameter : IUserConversionParameter
    {
        [SerializeField]
        public string Name { get; set; } // Name of conversion. This will be the basis of the shape's name, especially in the case of assigning blendshapes from an avatar.

        [SerializeField]
        public IFTAnimation Animation { get; set; } // Assigned by user, either through an 'Assign Blendshape' or 'Assign Animation' field (TBD).

        [SerializeField]
        public List<UnifiedExpressions> UnifiedConnections; // Configured in editor util (and baked in the config). Tells the avatar setup what this shape assigns to in the Unified parameter system.

        public List<FTParameter> ToUnifiedParameters()
        {
            List<FTParameter> parameters = new List<FTParameter>();

            try
            {
                Parallel.ForEach(UnifiedConnections.AsParallel().AsOrdered(), rep =>
                {
                    foreach (var param in AllUnifiedBaseParameters)
                        if (param.Name == rep.ToString())
                        {
                            parameters.Add(param);
                            break;
                        }
                });
            }
            catch (NullReferenceException)
            {
                return parameters;
            }

            FTParameterHandlers.CombineBaseParameters(ref parameters, AllUnifiedCombinedExpressions, 0); // if this conversion contains any known Unified parameter combinations, use that instead.
            return parameters;
        }

    }

    public enum UserEye
    {
        Left,
        Right,
    }

    [Serializable]
    public class UserEyeParameter : IUserConversionParameter
    {
        [SerializeField]
        public string Name { get; set; }
        public IFTAnimation Animation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [SerializeField]
        public string lookUpShape;
        [SerializeField]
        public string lookDownShape;
        [SerializeField]
        public string lookLeftShape;
        [SerializeField]
        public string lookRightShape;

        public bool showGazes = false;

        [SerializeField]
        public string pupilDiameter;
        [SerializeField]
        public string openness;

        public List<FTParameter> ToUnifiedParameters()
        {
            List<FTParameter> parameters = new List<FTParameter>();

            try
            {
                foreach (var param in AllUnifiedBaseParameters)
                {
                    if (param.Name.Contains(Name))
                    {

                        if (param.Name.Contains("Open"))
                            parameters.Add(param);
                    }
                }
            }
            catch (NullReferenceException)
            {
                return new List<FTParameter>();
            }
            /*
            Parallel.ForEach(UnifiedConnections.AsParallel().AsOrdered(), rep =>
            {
                foreach (var param in AllUnifiedBaseParameters)
                    if (param.Name == rep.ToString())
                    {
                        parameters.Add(param);
                        break;
                    }
            });
            */
            return parameters;
        }
    }
}
