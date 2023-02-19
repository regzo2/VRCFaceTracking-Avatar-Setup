using BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.VRCFTParameterContainers;

namespace VRCFaceTracking.Tools.Avatar_Setup.Containers
{
    public interface IUserConversionParameter
    {
        string Name { get; set; }
        List<FTParameter> ToUnifiedParameters();
    }

    [Serializable]
    public class UserConversionParameter : IUserConversionParameter
    {
        [SerializeField]
        public string Name { get; set; }
        [SerializeField]
        public List<UnifiedExpressions> UnifiedConnections;

        public List<FTParameter> ToUnifiedParameters()
        {
            List<FTParameter> parameters = new List<FTParameter>();

            try
            {
                foreach (var connection in UnifiedConnections)
                    foreach (var param in AllUnifiedBaseParameters)
                        if (param.Name == connection.ToString())
                        {
                            parameters.Add(param);
                            break;
                        }
            }
            catch (NullReferenceException)
            {
                return new List<FTParameter>();
            }
            /*
            Parallel.ForEach(UnifiedConnections, rep =>
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
        [SerializeField]
        public string lookUpShape;
        [SerializeField]
        public string lookDownShape;
        [SerializeField]
        public string lookLeftShape;
        [SerializeField]
        public string lookRightShape;

        public List<FTParameter> ToUnifiedParameters()
        {
            List<FTParameter> parameters = new List<FTParameter>();

            try
            {
                foreach (var param in AllUnifiedBaseParameters)
                {
                    if (param.Name.Contains(Name))
                        parameters.Add(param);
                    if (parameters.Count == 2)
                        break;
                }
            }
            catch (NullReferenceException)
            {
                return new List<FTParameter>();
            }
            /*
            Parallel.ForEach(UnifiedConnections, rep =>
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
