using PulsarModLoader;

namespace BetterScrapDrops
{
    public class Mod : PulsarMod
    {
        public override string Version => "1.1.0";

        public override string Author => "Dragon";

        public override string Name => "Better Scrap Drops";

        public override string LongDescription => "Causes components dropped from destroyed ships to provide higher level scrap when picked up. Level is based on components the ship had.";

        public override string HarmonyIdentifier()
        {
            return $"{Author}.{Name}";
        }
    }
}
