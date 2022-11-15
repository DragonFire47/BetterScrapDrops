using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static PulsarModLoader.Patches.HarmonyHelpers;

namespace BetterScrapDrops
{
    [HarmonyPatch(typeof(PLSpaceScrap), "OnCollect")]
    internal class Patch
    {
        //returns level for scrap
        static int PatchMethod(int componentHash)
        {
            if(componentHash == -1)
            {
                return 0;
            }
            else
            {
                return PLShipComponent.CreateShipComponentFromHash(componentHash).Level;
            }
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Stloc_3)
            };
            List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(PLSpaceScrap), "SpecificComponent_CompHash")),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patch), "PatchMethod")),
                new CodeInstruction(OpCodes.Stloc_3)
            };
            return PatchBySequence(instructions, targetSequence, injectedSequence, PatchMode.REPLACE);
        }
    }
}
