using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.FTAnimationContainers;

namespace VRCFaceTracking.Tools.Avatar_Setup.Containers
{
    public static class VRCFTParameterContainers
    {
        public enum UnifiedExpressions
        {
            #region Eye Gaze Expressions

            // These are currently unused for expressions and used in the UnifiedEye structure.
            // EyeLookOutRight,
            // EyeLookInRight,
            // EyeLookUpRight,
            // EyeLookDownRight,
            // EyeLookOutLeft,
            // EyeLookInLeft,
            // EyeLookUpLeft,
            // EyeLookDownLeft,

            #endregion

            #region Eye Expressions

            // 'Biometrically' accurate data that is included with UnifiedEye
            //EyeClosedRight, // Closes the right eyelid. Basis on the overall constriction of the palpebral part of orbicularis oculi.
            //EyeClosedLeft, // Closes the left eyelid. Basis on the overall constriction of the palpebral part of orbicularis oculi.
            //EyeDilationRight, // Dilates the right eye's pupil
            //EyeDilationLeft, // Dilates the left eye's pupil
            //EyeConstrictRight, // Constricts the right eye's pupil
            //EyeConstrictLeft, // Constricts the left eye's pupil

            EyeSquintRight, // Squeezes the right eye socket muscles, causing the lower eyelid to constrict a little bit as well. Basis on the mostly lower constriction of the inner parts of the orbicularis oculi and the stressing of the muscle group as the eyelid is closed.
            EyeSquintLeft, // Squeezes the left eye socket muscles, causing the lower eyelid to constrict a little bit as well. Basis on the mostly lower constriction of the inner parts of the orbicularis oculi and the stressing of the muscle group as the eyelid is closed.
            EyeWideRight, // Right eyelid widens beyond the eyelid's relaxed position. Basis on the action of the levator palpebrae superioris.
            EyeWideLeft, // Left eyelid widens beyond the eyelid's relaxed position. Basis on the action of the levator palpebrae superioris.

            #endregion

            #region Eyebrow Expressions

            BrowPinchRight, // Inner right eyebrow pulls diagnally inwards and downwards slightly. Basis on the constriction of the corrugator supercilii muscle.
            BrowPinchLeft, // Inner left eyebrow pulls diagnally inwards and downwards slightly. Basis on the constriction of the corrugator supercilii muscle.
            BrowLowererRight, // Outer right eyebrow pulls downward. Basis on depressor supercilii, procerus, and partially the upper orbicularis oculi muscles action of lowering the eyebrow.
            BrowLowererLeft, // Outer Left eyebrow pulls downward. Basis on depressor supercilii, procerus, and partially the upper orbicularis oculi muscles action of lowering the eyebrow.
            BrowInnerUpRight, // Inner right eyebrow pulls upward. Basis on the inner grouping action of the frontal belly of the occipitofrontalis.
            BrowInnerUpLeft, // Inner left eyebrow pulls upward. Basis on the inner grouping action of the frontal belly of the occipitofrontalis.
            BrowOuterUpRight, // Outer right eyebrow pulls upward. Basis on the outer grouping action of the frontal belly of the occipitofrontalis.
            BrowOuterUpLeft, // Outer left eyebrow pulls upward. Basis on the outer grouping action of the frontal belly of the occipitofrontalis.

            #endregion

            #region Nose Expressions

            NasalDilationRight, // Right side nose's canal dilates. Basis on the alar nasalis muscle.
            NasalDilationLeft, // Left side nose's canal dilates. Basis on the alar nasalis muscle.
            NasalConstrictRight, // Right side nose's canal constricts. Basis on the transverse nasalis muscle.
            NasalConstrictLeft, // Left side nose's canal constricts. Basis on the transverse nasalis muscle.

            #endregion

            #region Cheek Expressions

            CheekSquintRight, // Raises the right side cheek. Basis on the main action of the lower outer part of the orbicularis oculi.
            CheekSquintLeft, // Raises the left side cheek. Basis on the main action of the lower outer part of the orbicularis oculi.
            CheekPuffRight, // Puffs the right side cheek. Basis on the cheeks' ability to stretch orbitally.
            CheekPuffLeft, // Puffs the left side cheek. Basis on the cheeks' ability to stretch orbitally.
            CheekSuckRight, // Sucks in the right side cheek. Basis on the cheeks' ability to stretch inwards from suction.
            CheekSuckLeft, // Sucks in the left side cheek. Basis on the cheeks' ability to stretch inwards from suction.

            #endregion

            #region Jaw Exclusive Expressions

            JawOpen, // Opens the jawbone. Basis of the general action of the jaw opening by the masseter and temporalis muscle grouping.
            JawRight, // Pushes the jawbone right. Basis on medial pterygoid and lateral pterygoid's general action of shifting the jaw sideways.
            JawLeft, // Pushes the jawbone left. Basis on medial pterygoid and lateral pterygoid's general action of shifting the jaw sideways.
            JawForward, // Pushes the jawbone forward. Basis on the lateral pterygoid's ability to shift the jaw forward.
            JawBackward, // Pulls the jawbone backwards slightly. Based on the retraction of the temporalis muscle.
            JawClench, // Specific jaw muscles that forces the jaw closed. Causes the masseter muscle (visible close to the back of the jawline) to visibly flex.
            JawMandibleRaise, // Raises mandible (jawbone).

            MouthClosed, // Closes the mouth relative to JawOpen. Basis on the complex tightening action of the orbicularis oris muscle.

            #endregion

            #region Lip Expressions

            // 'Lip Push/Pull' group
            LipSuckUpperRight, // Upper right part of the lip gets tucked inside the mouth. No direct muscle basis as this action is caused from many indirect movements of tucking the lips.
            LipSuckUpperLeft, // Upper left part of the lip gets tucked inside the mouth. No direct muscle basis as this action is caused from many indirect movements of tucking the lips.
            LipSuckLowerRight, // Lower right part of the lip gets tucked inside the mouth. No direct muscle basis as this action is caused from many indirect movements of tucking the lips.
            LipSuckLowerLeft, // Lower left part of the lip gets tucked inside the mouth. No direct muscle basis as this action is caused from many indirect movements of tucking the lips.

            LipSuckCornerRight, // The right corners of the lips fold inward and into the mouth. Basis on the lips ability to stretch inwards from suction.
            LipSuckCornerLeft, // The left corners of the lips fold inward and into the mouth. Basis on the lips ability to stretch inwards from suction.

            LipFunnelUpperRight, // Upper right part of the lip pushes outward into a funnel shape. Basis on the orbicularis oris orbital muscle around the mouth pushing outwards and puckering.
            LipFunnelUpperLeft, // Upper left part of the lip pushes outward into a funnel shape. Basis on the orbicularis oris orbital muscle around the mouth pushing outwards and puckering.
            LipFunnelLowerRight, // Lower right part of the lip pushes outward into a funnel shape. Basis on the orbicularis oris orbital muscle around the mouth pushing outwards and puckering.
            LipFunnelLowerLeft, // Lower left part of the lip pushes outward into a funnel shape. Basis on the orbicularis oris orbital muscle around the mouth pushing outwards and puckering.

            LipPuckerUpperRight, // Upper right part of the lip pinches inward and pushes outward. Basis on complex action of the orbicularis-oris orbital muscle around the lips.
            LipPuckerUpperLeft, // Upper left part of the lip pinches inward and pushes outward. Basis on complex action of the orbicularis-oris orbital muscle around the lips.
            LipPuckerLowerRight, // Lower right part of the lip pinches inward and pushes outward. Basis on complex action of the orbicularis-oris orbital muscle around the lips.
            LipPuckerLowerLeft, // Lower left part of the lip pinches inward and pushes outward. Basis on complex action of the orbicularis-oris orbital muscle around the lips.

            // 'Upper lip raiser' group
            MouthUpperInnerUpRight, // Upper inner right part of the lip is pulled upward. Basis on the levator labii superioris muscle.
            MouthUpperInnerUpLeft, // Upper inner left part of the lip is pulled upward. Basis on the levator labii superioris muscle.
            MouthUpperDeepenRight, // Upper outer right part of the lip is pulled upward, backward, and rightward. Basis on the zygomaticus minor muscle.
            MouthUpperDeepenLeft, // Upper outer left part of the lip is pulled upward, backward, and rightward. Basis on the zygomaticus minor muscle.
            NoseSneerRight, // The right side face pulls upward into a sneer and raises the inner part of the lips at extreme ranges. Based on levator labii superioris alaeque nasi muscle.
            NoseSneerLeft, // The right side face pulls upward into a sneer and raises the inner part of the lips slightly at extreme ranges. Based on levator labii superioris alaeque nasi muscle.

            // 'Lower lip depressor' group
            MouthLowerDownRight, // Lower right part of the lip is pulled downward. Basis on the depressor labii inferioris muscle.
            MouthLowerDownLeft, // Lower left part of the lip is pulled downward. Basis on the depressor labii inferioris muscle.

            // 'Mouth Direction' group
            MouthUpperRight, // Moves upper lip right. Basis on the general horizontal movement action of the upper orbicularis oris orbital, levator anguli oris, and buccinator muscle grouping.
            MouthUpperLeft, // Moves upper lip left. Basis on the general horizontal movement action of the upper orbicularis oris orbital, levator anguli oris, and buccinator muscle grouping.
            MouthLowerRight, // Moves lower lip right. Basis on the general horizontal movement action of the lower orbicularis oris orbital, risorius, depressor labii inferioris, and buccinator muscle grouping.
            MouthLowerLeft, // Moves lower lip left. Basis on the general horizontal movement action of the lower orbicularis oris orbital, risorius, depressor labii inferioris, and buccinator muscle grouping.

            // 'Smile' group
            MouthCornerPullRight, // Right side of the lip is pulled diagnally upwards and rightwards significantly. Basis on the action of the levator anguli oris muscle.
            MouthCornerPullLeft, // :eft side of the lip is pulled diagnally upwards and leftwards significantly. Basis on the action of the levator anguli oris muscle.
            MouthCornerSlantRight, // Right corner of the lip is pulled upward slightly. Basis on the action of the levator anguli oris muscle.
            MouthCornerSlantLeft, // Left corner of the lip is pulled upward slightly. Basis on the action of the levator anguli oris muscle.

            // 'Sad' group
            MouthFrownRight, // Right corner of the lip is pushed downward. Basis on the action of the depressor anguli oris muscle. Directly opposes the levator muscles.
            MouthFrownLeft, // Left corner of the lip is pushed downward. Basis on the action of the depressor anguli oris muscle. Directly opposes the levator muscles.
            MouthStretchRight, // Stretches the right side lips together horizontally and thins them vertically slightly. Basis on the risorius muscle.
            MouthStretchLeft,  // Stretches the left side lips together horizontally and thins them vertically slightly. Basis on the risorius muscle.

            MouthDimpleRight, // Right corner of the lip is pushed backwards into the face, creating a dimple. Basis on buccinator muscle structure.
            MouthDimpleLeft, // Left corner of the lip is pushed backwards into the face, creating a dimple. Basis on buccinator muscle structure.

            MouthRaiserUpper, // Raises the upper part of the mouth in response to MouthRaiserLower. No muscular basis.
            MouthRaiserLower, // Raises the lower part of the mouth. Based on the complex lower pushing action of the mentalis muscle.
            MouthPressRight, // Squeezes the right side lips together vertically and flattens them. Basis on the complex tightening action of the orbicularis oris muscle.
            MouthPressLeft, // Squeezes the left side lips together vertically and flattens them. Basis on the complex tightening action of the orbicularis oris muscle.
            MouthTightenerRight, // Squeezes the right side lips together horizontally and thickens them vertically slightly. Basis on the complex tightening action of the orbicularis oris muscle.
            MouthTightenerLeft, // Squeezes the right side lips together horizontally and thickens them vertically slightly. Basis on the complex tightening action of the orbicularis oris muscle.

            #endregion

            #region Tongue Expressions

            TongueOut, // Combined LongStep1 and LongStep2 into one shape, as it can be emulated in-animation

            // Based on SRanipal tracking standard's tongue tracking.
            TongueUp, // Tongue points in an upward direction.
            TongueDown, // Tongue points in an downward direction.
            TongueRight, // Tongue points in an rightward direction.
            TongueLeft, // Tongue points in an leftward direction.

            // Based on https://www.naun.org/main/NAUN/computers/2018/a042007-060.pdf
            TongueRoll, // Rolls up the sides of the tongue into a 'hotdog bun' shape.
            TongueBendDown, // Pushes tip of the tongue below the rest of the tongue in an arch.
            TongueCurlUp, // Pushes tip of the tongue above the rest of the tongue in an arch.
            TongueSquish, // Tongue becomes thinner width-wise and slightly thicker height-wise.
            TongueFlat, // Tongue becomes thicker width-wise and slightly thinner height-wise.

            TongueTwistRight, // Tongue tip rotates clockwise from POV with the rest of the tongue following gradually.
            TongueTwistLeft, // Tongue tip rotates counter-clockwise from POV with the rest of the tongue following gradually.

            #endregion

            #region Throat/Neck Expressions

            SoftPalateClose, // Visibly lowers the back of the throat (soft palate) inside the mouth to close off the throat.
            ThroatSwallow, // Visibly causes the Adam's apple to pull upward into the throat as if swallowing.

            NeckFlexRight, // Flexes the Right side of the neck and face (causes the right corner of the face to stretch towards.)
            NeckFlexLeft, // Flexes the left side of the neck and face (causes the left corner of the face to stretch towards.)

            #endregion

            Max
        }
        public enum BlendTreeDriveType
        {
            Positive,
            Negative,
            PositiveNegative,
        }
        public enum ParameterState
        {
            Shown,
            Hidden,
        }

        public class FTParameter
        {
            public static int GLOBAL_MINIMUM_SIZE = 3;
            public static int GLOBAL_MAXIMUM_SIZE = 8; // VRChat's networked float size, will create animations as floats first then control them via binary conversions in animator.

            // Relevant to organizing and compacting the expression parameters.
            private int minimumSize;
            private int maximumSize = GLOBAL_MAXIMUM_SIZE; // 8 = float parameter, <8 is a binary parameter.

            public string Name { get; set; }
            public int Size { get; set; }
            public int MinimumSize // Minimum byte size this parameter can subjectively give good tracking at.
            {
                get { return minimumSize; }
                set
                {
                    minimumSize = value;
                    if (value < GLOBAL_MINIMUM_SIZE)
                        minimumSize = GLOBAL_MINIMUM_SIZE;
                    if (value > GLOBAL_MAXIMUM_SIZE)
                        minimumSize = GLOBAL_MAXIMUM_SIZE;
                }
            }
            public int MaximumSize { get { return maximumSize; } set { maximumSize = GLOBAL_MAXIMUM_SIZE; } }
            public int Importance { get; set; }
            public bool IsUsed { get; set; }

            // Relevant to generating animations.
            public List<IFTAnimation> Animations { get; set; } // animations connected to this parameter.
        }

        public class FTCombinedParameterPart : FTParameter
        {
            public BlendTreeDriveType CombinedDriver { get; set; }
            public float minRange = 0;
            public float maxRange = 1; 
        }

        public class CombinedFTParameter : FTParameter
        {
            public List<FTCombinedParameterPart> Parameters { get; set; } // Base parameters named that exist in here will get their derived base data transplanted into this container.
            public int Quality { get; set; }
        }

        public static readonly List<FTParameter> AllUnifiedBaseParameters = new List<FTParameter>
        {
            new FTParameter { Name = "EyeLeftX", Importance = 11,MinimumSize = 4 },
            new FTParameter { Name = "EyeRightX", Importance = 11, MinimumSize = 4 },
            new FTParameter { Name = "EyeLeftY", Importance = 11, MinimumSize = 4 },
            new FTParameter { Name = "EyeRightY", Importance = 11, MinimumSize = 4 },
            new FTParameter { Name = "PupilDiameterLeft", Importance = 6, MinimumSize = 2 },
            new FTParameter{ Name = "PupilDiameterRight", Importance = 6, MinimumSize = 2 },
            new FTParameter{ Name = "EyeOpenLeft", Importance = 10, MinimumSize = 4 },
            new FTParameter{ Name = "EyeOpenRight", Importance = 10, MinimumSize = 4 },

            new FTParameter{ Name = "EyeSquintRight", Importance = 8, MinimumSize = 2 },
            new FTParameter{ Name = "EyeSquintLeft", Importance = 8, MinimumSize = 2 },
            new FTParameter{ Name = "EyeWideRight", Importance = 10, MinimumSize = 2 },
            new FTParameter{ Name = "EyeWideLeft", Importance = 10, MinimumSize = 2 },

            new FTParameter{ Name = "BrowPinchRight", Importance = 9, MinimumSize = 4 },
            new FTParameter{ Name = "BrowPinchLeft", Importance = 9, MinimumSize = 4 },
            new FTParameter{ Name = "BrowLowererRight", Importance = 9, MinimumSize = 4 },
            new FTParameter{ Name = "BrowLowererLeft", Importance = 9, MinimumSize = 4 },
            new FTParameter{ Name = "BrowInnerUpRight", Importance = 9, MinimumSize = 4 },
            new FTParameter{ Name = "BrowInnerUpLeft", Importance = 9, MinimumSize = 4 },
            new FTParameter{ Name = "BrowOuterUpRight", Importance = 9, MinimumSize = 4 },
            new FTParameter{ Name = "BrowOuterUpLeft", Importance = 9, MinimumSize = 4 },

            new FTParameter{ Name = "NasalDilationRight", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "NasalDilationLeft", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "NasalConstrictRight", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "NasalConstrictLeft", Importance = 0, MinimumSize = 2 },

            new FTParameter{ Name = "CheekSquintLeft", Importance = 6, MinimumSize = 2 },
            new FTParameter{ Name = "CheekSquintRight", Importance = 6, MinimumSize = 2 },
            new FTParameter{ Name = "CheekPuffRight", Importance = 6, MinimumSize = 3 },    
            new FTParameter{ Name = "CheekPuffLeft", Importance = 6, MinimumSize = 3 }, 
            new FTParameter{ Name = "CheekSuckRight", Importance = 6, MinimumSize = 3 },
            new FTParameter{ Name = "CheekSuckLeft", Importance = 6, MinimumSize = 3 },

            new FTParameter{ Name = "JawOpen", Importance = 10, MinimumSize = 4 },
            new FTParameter{ Name = "JawLeft", Importance = 7, MinimumSize = 3 },
            new FTParameter{ Name = "JawRight", Importance = 7, MinimumSize = 3 },
            new FTParameter{ Name = "JawForward", Importance = 4, MinimumSize = 2 },
            new FTParameter{ Name = "JawBackward", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "JawClench", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "JawMandibleRaise", Importance = 0, MinimumSize = 2 },

            new FTParameter{ Name = "MouthClosed", Importance = 10, MinimumSize = 4 },

            new FTParameter{ Name = "MouthCornerPullLeft", Importance = 10, MinimumSize = 3 },
            new FTParameter{ Name = "MouthCornerPullRight", Importance = 10, MinimumSize = 3 },

            new FTParameter{ Name = "MouthCornerSlantLeft", Importance = 10, MinimumSize = 3 },
            new FTParameter{ Name = "MouthCornerSlantRight", Importance = 10, MinimumSize = 3 },

            new FTParameter{ Name = "MouthFrownLeft", Importance = 10, MinimumSize = 3 },
            new FTParameter{ Name = "MouthFrownRight", Importance = 10, MinimumSize = 3 },

            new FTParameter{ Name = "LipPuckerLowerLeft", Importance = 10, MinimumSize = 2 },
            new FTParameter{ Name = "LipPuckerUpperLeft", Importance = 10, MinimumSize = 2 },
            new FTParameter{ Name = "LipPuckerUpperRight", Importance = 10, MinimumSize = 2 },
            new FTParameter{ Name = "LipPuckerLowerRight", Importance = 10, MinimumSize = 2 },

            new FTParameter{ Name = "LipFunnelUpperLeft", Importance = 9, MinimumSize = 2 },
            new FTParameter{ Name = "LipFunnelLowerLeft", Importance = 9, MinimumSize = 2 },
            new FTParameter{ Name = "LipFunnelUpperRight", Importance = 9, MinimumSize = 2 },
            new FTParameter{ Name = "LipFunnelLowerRight", Importance = 9, MinimumSize = 2 },

            new FTParameter{ Name = "LipSuckLowerLeft", Importance = 8, MinimumSize = 2 },
            new FTParameter{ Name = "LipSuckUpperLeft", Importance = 8, MinimumSize = 2 },
            new FTParameter{ Name = "LipSuckUpperRight", Importance = 8, MinimumSize = 2 },
            new FTParameter{ Name = "LipSuckLowerRight", Importance = 8, MinimumSize = 2 },

            new FTParameter{ Name = "MouthUpperInnerUpRight", Importance = 10, MinimumSize = 4 },
            new FTParameter{ Name = "MouthUpperDeepenRight", Importance = 10, MinimumSize = 4 },

            new FTParameter{ Name = "MouthUpperInnerUpLeft", Importance = 10, MinimumSize = 4 },
            new FTParameter{ Name = "MouthUpperDeepenLeft", Importance = 10, MinimumSize = 4 },

            new FTParameter{ Name = "MouthLowerDownRight", Importance = 10, MinimumSize = 4 },
            new FTParameter{ Name = "MouthLowerDownLeft", Importance = 10, MinimumSize = 4 },
            new FTParameter{ Name = "MouthUpperRight", Importance = 8, MinimumSize = 3 },   
            new FTParameter{ Name = "MouthUpperLeft", Importance = 8, MinimumSize = 3 },    
            new FTParameter{ Name = "MouthLowerRight", Importance = 8, MinimumSize = 3 },
            new FTParameter{ Name = "MouthLowerLeft", Importance = 8, MinimumSize = 3 },
            new FTParameter{ Name = "TongueOut", Importance = 8, MinimumSize = 2 },
            new FTParameter{ Name = "MouthRaiserUpper", Importance = 7, MinimumSize = 2 },
            new FTParameter{ Name = "MouthRaiserLower", Importance = 7, MinimumSize = 2 },

            // 'Secondary' face parameters
            
            new FTParameter{ Name = "MouthDimpleLeft", Importance = 5, MinimumSize = 3 },
            new FTParameter{ Name = "MouthDimpleRight", Importance = 5, MinimumSize = 3 },
            new FTParameter{ Name = "NoseSneerRight", Importance = 9, MinimumSize = 3 },
            new FTParameter{ Name = "NoseSneerLeft", Importance = 9, MinimumSize = 3 },
            new FTParameter{ Name = "MouthPressLeft", Importance = 4, MinimumSize = 2 },
            new FTParameter{ Name = "MouthPressRight", Importance = 4, MinimumSize = 2 },
            new FTParameter{ Name = "MouthTightenerLeft", Importance = 4, MinimumSize = 2 },
            new FTParameter{ Name = "MouthTightenerRight", Importance = 4, MinimumSize = 2 },
            new FTParameter{ Name = "MouthStretchLeft", Importance = 6, MinimumSize = 2 },
            new FTParameter{ Name = "MouthStretchRight", Importance = 6, MinimumSize = 2 },
            new FTParameter{ Name = "TongueUp", Importance = 5, MinimumSize = 2 },
            new FTParameter{ Name = "TongueDown", Importance = 5, MinimumSize = 2 },
            new FTParameter{ Name = "TongueLeft", Importance = 5, MinimumSize = 2 },
            new FTParameter{ Name = "TongueRight", Importance = 5, MinimumSize = 2 },
            new FTParameter{ Name = "TongueRoll", Importance = 2, MinimumSize = 2 },
            new FTParameter{ Name = "TongueBendDown", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "TongueCurlUp", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "TongueSquish", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "TongueFlat", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "TongueTwistLeft", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "TongueTwistRight", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "SoftPalateClose", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "ThroatSwallow", Importance = 0, MinimumSize = 2 },
            new FTParameter{ Name = "NeckFlexLeft", Importance = 0, MinimumSize = 4 },
            new FTParameter{ Name = "NeckFlexRight", Importance = 0, MinimumSize = 4 },
            new FTParameter{ Name = "LipSuckCornerRight", Importance = 0, MinimumSize = 4 },
            new FTParameter{ Name = "LipSuckCornerLeft", Importance = 0, MinimumSize = 4 },
        };

        public static readonly List<CombinedFTParameter> UnifiedCombinedEyeExpressions = new List<CombinedFTParameter>
        {
            new CombinedFTParameter
            {
                Name = "EyesY", Quality = 10, MinimumSize = 4, Importance = 11, 
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.PositiveNegative,
                        Name = "EyeLeftY"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.PositiveNegative,
                        Name = "EyeRightY"},
                }
            },
            new CombinedFTParameter
            {
                Name = "EyesX", Quality = 0, MinimumSize = 4, Importance = 11,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.PositiveNegative,
                        Name = "EyeLeftX"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.PositiveNegative,
                        Name = "EyeRightX"},
                }
            },
            new CombinedFTParameter
            {
                Name = "EyeLidRight", Quality = 10, MinimumSize = 4, Importance = 11,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "EyeOpenRight",  minRange = 0, maxRange = 0.8f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "EyeWideRight", minRange = 0.8f, maxRange = 1f},
                }
            },
            new CombinedFTParameter
            {
                Name = "EyeLidLeft", Quality = 10, MinimumSize = 4, Importance = 11,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "EyeOpenLeft",  minRange = 0, maxRange = 0.8f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "EyeWideLeft", minRange = 0.8f, maxRange = 1f},
                }
            },
            new CombinedFTParameter
            {
                Name = "EyeLid", Quality = 2, MinimumSize = 4, Importance = 11,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "EyeOpenLeft",  minRange = 0, maxRange = 0.8f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "EyeOpenRight", minRange = 0, maxRange = 0.8f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "EyeWideLeft", minRange = 0.8f, maxRange = 1f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "EyeWideRight", minRange = 0.8f, maxRange = 1f},
                }
            },
            new CombinedFTParameter
            {
                Name = "EyeSquint", Quality = 4, MinimumSize = 2, Importance = 8,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "EyeSquintLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "EyeSquintRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "EyeDilation", Quality = 9, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "PupilDiameterLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "PupilDiameterRight"},
                }
            },
        };
        public static readonly List<CombinedFTParameter> UnifiedCombinedLipExpressions = new List<CombinedFTParameter>
        {
            new CombinedFTParameter
            {
                Name = "LipSuckUpper", Quality = 8, MinimumSize = 2, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipSuckUpperLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipSuckUpperRight",  minRange = 0, maxRange = 1.0f},
                }
            },
            new CombinedFTParameter
            {
                Name = "LipSuckLower", Quality = 8, MinimumSize = 2, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipSuckLowerLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipSuckLowerRight",  minRange = 0, maxRange = 1.0f},
                }
            },
            new CombinedFTParameter
            {
                Name = "LipSuck", Quality = 1, MinimumSize = 2, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipSuckUpperLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipSuckUpperRight",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipSuckLowerLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipSuckLowerRight",  minRange = 0, maxRange = 1.0f},
                }
            },
            new CombinedFTParameter
            {
                Name = "LipFunnelUpper", Quality = 8, MinimumSize = 2, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipFunnelUpperLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipFunnelUpperRight",  minRange = 0, maxRange = 1.0f},
                }
            },
            new CombinedFTParameter
            {
                Name = "LipFunnelLower", Quality = 8, MinimumSize = 2, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipFunnelLowerLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipFunnelLowerRight",  minRange = 0, maxRange = 1.0f},
                }
            },
            new CombinedFTParameter
            {
                Name = "LipFunnel", Quality = 2, MinimumSize = 2, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipFunnelUpperLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipFunnelUpperRight",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipFunnelLowerLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipFunnelLowerRight",  minRange = 0, maxRange = 1.0f},
                }
            },
            new CombinedFTParameter
            {
                Name = "LipPuckerUpper", Quality = 8, MinimumSize = 2, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipPuckerUpperLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipPuckerUpperRight",  minRange = 0, maxRange = 1.0f},
                }
            },
            new CombinedFTParameter
            {
                Name = "LipPuckerLower", Quality = 8, MinimumSize = 2, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipPuckerLowerLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipPuckerLowerRight",  minRange = 0, maxRange = 1.0f},
                }
            },
            new CombinedFTParameter
            {
                Name = "LipPucker", Quality = 7, MinimumSize = 2, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipPuckerUpperLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipPuckerUpperRight",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipPuckerLowerLeft",  minRange = 0, maxRange = 1.0f},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "LipPuckerLowerRight",  minRange = 0, maxRange = 1.0f},
                }
            },
        };
        public static readonly List<CombinedFTParameter> UnifiedCombinedJawExpressions = new List<CombinedFTParameter>
        {
            new CombinedFTParameter
            {
                Name = "JawZ", Quality = 10, MinimumSize = 2, Importance = 4,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "JawForwards"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "JawBackwards"},
                }
            },
            new CombinedFTParameter
            {
                Name = "JawX", Quality = 10, MinimumSize = 2, Importance = 4,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "JawRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "JawLeft"},
                }
            },
        };
        public static readonly List<CombinedFTParameter> UnifiedCombinedCheekExpressions = new List<CombinedFTParameter> 
        {
            new CombinedFTParameter
            {
                Name = "CheekSquint", Quality = 4, MinimumSize = 2, Importance = 6,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "CheekSquintLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "CheekSquintRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "CheekLeftPuffSuck", Quality = 10, MinimumSize = 2, Importance = 6,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "CheekPuffLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "CheekSuckLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "CheekRightPuffSuck", Quality = 10, MinimumSize = 2, Importance = 6,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "CheekPuffRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "CheekSuckRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "CheekPuffSuck", Quality = 10, MinimumSize = 2, Importance = 6,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "CheekPuffRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "CheekPuffLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "CheekSuckRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "CheekSuckLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "CheekSuck", Quality = 6, MinimumSize = 2, Importance = 6,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "CheekSuckRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "CheekSuckLeft"},
                }
            },
        };
        public static readonly List<CombinedFTParameter> UnifiedCombinedSmileSadExpressions = new List<CombinedFTParameter>
        {
            new CombinedFTParameter
            {
                Name = "MouthCornersYLeft", Quality = 10, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthFrownLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthCornersYRight", Quality = 10, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthFrownRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthSmileRight", Quality = 8, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerPullRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantRight"}
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthSmileLeft", Quality = 8, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerPullLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantLeft"}
                }
            },
            new CombinedFTParameter
            {
                Name = "SmileSadLeft", Quality = 6, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerPullLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthFrownLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthStretchLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "SmileSadRight", Quality = 6, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerPullRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthFrownRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthStretchRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "SmileFrownLeft", Quality = 9, MinimumSize = 4, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerPullLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthFrownLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "SmileFrownRight", Quality = 9, MinimumSize = 4, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerPullRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthFrownRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "SmileFrown", Quality = 4, MinimumSize = 4, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerPullRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerPullLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthFrownRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthFrownLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "SmileSad", Quality = 2, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerPullRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerPullLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthCornerSlantLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthFrownRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthStretchRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthFrownLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthStretchLeft"},
                }
            },
        };
        public static readonly List<CombinedFTParameter> UnifiedCombinedMouthDirection = new List<CombinedFTParameter>
        {
            new CombinedFTParameter
            {
                Name = "MouthUpperX", Quality = 10, MinimumSize = 2, Importance = 8,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthUpperLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthLowerX", Quality = 10, MinimumSize = 2, Importance = 8,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthLowerRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthLowerLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthX", Quality = 6, MinimumSize = 2, Importance = 8,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthLowerRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthLowerLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "MouthUpperLeft"},
                }
            },
        };
        public static readonly List<CombinedFTParameter> UnifiedCombinedMouthExpressions = new List<CombinedFTParameter>
        {
            new CombinedFTParameter
            {
                Name = "NoseSneer", Quality = 2, MinimumSize = 2, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "NoseSneerRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "NoseSneerLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthRaiser", Quality = 4, MinimumSize = 2, Importance = 7,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthRaiserUpper"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthRaiserLower"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthOpen", Quality = 2, MinimumSize = 2, Importance = 7,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperDeepenRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperInnerUpRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperDeepenLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperInnerUpLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthLowerDownLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthLowerDownLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthUpperUpRight", Quality = 8, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperDeepenRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperInnerUpRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthUpperUpLeft", Quality = 8, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperDeepenLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperInnerUpLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthUpperUp", Quality = 4, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperDeepenRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperInnerUpRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperDeepenLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthUpperInnerUpLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthLowerDown", Quality = 4, MinimumSize = 4, Importance = 10,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthLowerDownLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthLowerDownRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthDimple", Quality = 4, MinimumSize = 2, Importance = 8,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthDimpleLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthDimpleRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthStretch", Quality = 4, MinimumSize = 2, Importance = 8,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthStretchLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthStretchRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthTightener", Quality = 4, MinimumSize = 2, Importance = 8,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthTightenerLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthTightenerRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthPress", Quality = 4, MinimumSize = 2, Importance = 8,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthPressLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthPressRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "MouthPressTighten", Quality = 1, MinimumSize = 2, Importance = 8,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthTightenerLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthTightenerRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthPressLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "MouthPressRight"},
                }
            },
        };
        public static readonly List<CombinedFTParameter> UnifiedCombinedBrowExpressions = new List<CombinedFTParameter>
        {
            new CombinedFTParameter
            {
                Name = "BrowDownRight", Quality = 9, MinimumSize = 2, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowLowererRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowPinchRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "BrowDownLeft", Quality = 9, MinimumSize = 2, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowLowererLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowPinchLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "BrowsDown", Quality = 8, MinimumSize = 2, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowLowererRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowLowererLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowPinchRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowPinchLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "BrowsInnerUp", Quality = 8, MinimumSize = 2, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowInnerUpRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowInnerUpLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "BrowsOuterUp", Quality = 8, MinimumSize = 2, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowOuterUpRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowOuterUpLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "BrowExpressionRight", Quality = 5, MinimumSize = 3, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowOuterUpRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowInnerUpRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "BrowLowererRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "BrowPinchRight"},
                }
            },
            new CombinedFTParameter
            {
                Name = "BrowExpressionLeft", Quality = 5, MinimumSize = 3, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowOuterUpLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowInnerUpLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "BrowLowererLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "BrowPinchLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "BrowExpression", Quality = 3, MinimumSize = 3, Importance = 9,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowOuterUpRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowInnerUpRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowOuterUpLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "BrowInnerUpLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "BrowLowererRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "BrowPinchRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "BrowLowererLeft"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "BrowPinchLeft"},
                }
            },
        };
        public static readonly List<CombinedFTParameter> UnifiedCombinedTongueExpressions = new List<CombinedFTParameter>
        {
            new CombinedFTParameter
            {
                Name = "TongueX", Quality = 10, MinimumSize = 3, Importance = 5,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "TongueRight"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "TongueLeft"},
                }
            },
            new CombinedFTParameter
            {
                Name = "TongueY", Quality = 10, MinimumSize = 3, Importance = 5,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "TongueUp"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "TongueDown"},
                }
            },
            new CombinedFTParameter
            {
                Name = "TongueArchY", Quality = 9, MinimumSize = 2, Importance = 0,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "TongueBendDown"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "TongueCurlUp"},
                }
            },
            new CombinedFTParameter
            {
                Name = "TongueShape", Quality = 9, MinimumSize = 2, Importance = 0,
                Parameters = new List<FTCombinedParameterPart>
                {
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Positive,
                        Name = "TongueSquish"},
                    new FTCombinedParameterPart {
                        CombinedDriver = BlendTreeDriveType.Negative,
                        Name = "TongueFlat"},
                }
            },
        };

        public static readonly List<CombinedFTParameter> AllUnifiedCombinedExpressions =
            UnifiedCombinedEyeExpressions
            .Concat(UnifiedCombinedJawExpressions)
            .Concat(UnifiedCombinedSmileSadExpressions)
            .Concat(UnifiedCombinedMouthExpressions)
            .Concat(UnifiedCombinedLipExpressions)
            .Concat(UnifiedCombinedCheekExpressions)
            .Concat(UnifiedCombinedMouthDirection)
            .Concat(UnifiedCombinedBrowExpressions)
            .Concat(UnifiedCombinedTongueExpressions)
            .ToList();
    }
}