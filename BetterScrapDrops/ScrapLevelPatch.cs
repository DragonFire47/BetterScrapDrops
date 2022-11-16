using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using static PulsarModLoader.Patches.HarmonyHelpers;

namespace BetterScrapperTurrets
{
    [HarmonyPatch(typeof(PLServer), "CreateSpaceScrapAtLocation")]
    internal class ScrapLevelPatch
    {
        static int PatchMethod(PLShipInfoBase ship)
        {
            return (int)ship.GetCombatLevel() / Command.ScrapLevelDivider.Value;
        }
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldloc_2),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Stfld),
            };

            List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldloc_2),
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ScrapLevelPatch), "PatchMethod")),
                new CodeInstruction(OpCodes.Stfld, AccessTools.Field(typeof(PLSpaceScrap), "SpecificComponent_CompHash")),
            };
            return PatchBySequence(instructions, targetSequence, injectedSequence, PatchMode.BEFORE, CheckMode.NEVER);
        }
    }
}
