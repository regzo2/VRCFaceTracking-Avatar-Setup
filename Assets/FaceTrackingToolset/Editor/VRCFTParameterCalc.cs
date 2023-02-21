using System;
using System.Collections.Generic;
using System.Linq;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRCFaceTracking.Tools.Avatar_Setup.Containers;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.VRCFTParameterContainers;
using UnityEngine;

namespace VRCFaceTracking.Tools.Avatar_Setup.Containers
{
    public static class FTParameterHandlers 
    {
        public enum ParameterHandlerBehavior
        {
            Default,
            NoCompression,
            NoCombined,
            NoImportance,
        }

        public static int availableBits;
        public static int qualityThreshold;
        public static int importanceThreshold = 1;
        public static int importanceBias;

        public static List<FTParameter> RecommendParameters(List<FTParameter> parameters, List<CombinedFTParameter> combinedParameters, ParameterHandlerBehavior behavior = ParameterHandlerBehavior.Default)
        {
            List<FTParameter> parametersToOutput = parameters.Where(p => p.Size <= availableBits && p.Importance >= importanceThreshold).ToList();

            if (behavior != ParameterHandlerBehavior.NoCombined)
                CombineBaseParameters(ref parametersToOutput, combinedParameters, qualityThreshold); // Sort parameters by importance

            if (behavior != ParameterHandlerBehavior.NoImportance)
                parametersToOutput = parametersToOutput.OrderByDescending(p => p.Importance).ToList(); // Sort parameters by importance

            if (behavior == ParameterHandlerBehavior.NoCompression)
                return parametersToOutput;

            return CompressParameters(parametersToOutput);

        }

        public static void CombineBaseParameters(ref List<FTParameter> parametersToOutput, List<CombinedFTParameter> combinedParameters, int quality)
        {

            List<CombinedFTParameter> combinedParametersToOutput = combinedParameters.Where(p => p.Size <= availableBits).OrderBy(cp => cp.Quality).ToList();
            List<FTParameter> _parametersToRemove = new List<FTParameter>();

            foreach (var combinedParameter in combinedParametersToOutput) // incorporates combined parameters by replacing existing combininations.
            {
                if (combinedParameter.Quality < quality)
                    continue;

                _parametersToRemove.Clear();
                try
                {
                    for (int i = 0; i < combinedParameter.Parameters.Count; i++) // Check if all sub-parameters of the combined parameter are already in the list of parameters
                    {
                        var result = parametersToOutput.First(p => p.Name == combinedParameter.Parameters[i].Name);
                        //Debug.Log("Parameter match: " + result.Name + " in: " + combinedParameter.Name);

                        if (result != null)
                        {
                            if (combinedParameter.Importance < result.Importance) // Pull up combined importance to overall base parameters' highest importance.
                                combinedParameter.Importance = result.Importance;
                            combinedParameter.Parameters[i].Animations = result.Animations; // use results data.
                            combinedParameter.Parameters[i].Importance = result.Importance;
                            combinedParameter.Parameters[i].MinimumSize = result.MinimumSize;
                            combinedParameter.Parameters[i].MaximumSize = result.MaximumSize;
                            _parametersToRemove.Add(result);
                        }
                    }
                }
                catch
                {
                    //Debug.Log("A parameter match in " + combinedParameter.Name + " is not in base params anymore.");
                    continue;
                }

                //Debug.Log("Replaced parameters with " + combinedParameter.Name + " q: " + combinedParameter.Quality);
                foreach (var _parameter in _parametersToRemove)
                    parametersToOutput.Remove(_parameter);
                parametersToOutput.Add(combinedParameter);
            }
        }

        public static List<FTParameter> CompressParameters(List<FTParameter> parametersToOutput)
        {
            List<FTParameter> recommendedParameters = new List<FTParameter>();

            int tries = 0;
            int highestImportance = 11;
            int _importanceBias = importanceBias;
            int minimumTotalSize = 0;

            int maximumBits = 0;
            parametersToOutput.ForEach(p => maximumBits += p.MaximumSize);

            int totalBits = 0;
            while (totalBits < availableBits && totalBits < maximumBits && tries < 30)
            {
                foreach (var parameter in parametersToOutput.Where(p => p.Importance >= highestImportance)) // Add parameters and stop if we hit the cap.
                {
                    if (totalBits + parameter.MinimumSize <= availableBits && !recommendedParameters.Contains(parameter))
                    {
                        if (minimumTotalSize < parameter.MinimumSize)
                            minimumTotalSize = parameter.MinimumSize;
                        parameter.Size = parameter.MinimumSize;
                        recommendedParameters.Add(parameter);
                        totalBits += parameter.MinimumSize;
                    }
                }

                if (_importanceBias == 1)  // Forces more important parameters to gain more bits.
                {
                    highestImportance--;
                    _importanceBias = importanceBias;
                }
                else if (_importanceBias == 0)  // Loop through all parameters starting at minimum size instead of importance.
                {
                    var minIter = 0;
                    while (minIter <= minimumTotalSize)
                    {
                        foreach (var parameter in recommendedParameters.Where(p => p.Size <= minIter))
                        {
                            if (totalBits + 1 <= availableBits && parameter.Size < parameter.MaximumSize)
                            {
                                parameter.Size++;
                                totalBits++;
                            }
                        }
                        minIter++;
                        _importanceBias--;
                    }

                }
                else _importanceBias--;

                recommendedParameters.ForEach(p => maximumBits += p.MaximumSize);

                foreach (var parameter in recommendedParameters) // Add parameters and stop if we hit the cap.
                {
                    if (totalBits + 1 <= availableBits && parameter.Size < parameter.MaximumSize)
                    {
                        parameter.Size += 1;
                        totalBits += 1;
                    }
                }

                tries++;
            }

            return recommendedParameters;
        }
    }
}
