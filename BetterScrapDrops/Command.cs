using PulsarModLoader;
using PulsarModLoader.Chat.Commands.CommandRouter;
using PulsarModLoader.Utilities;

namespace BetterScrapperTurrets
{
    internal class Command : ChatCommand
    {
        public static SaveValue<int> ScrapLevelDivider = new SaveValue<int>("ScrapLevelDivider", 20);

        public override string[] CommandAliases()
        {
            return new string[] { "setscrapleveldivider", "ssld" };
        }

        public override string Description()
        {
            return "Sets combat level divider for scrap from scrapper turrets. Level of scrapper turret scrap = ShipCombatLevel/ScrapLevelDivider. Combat level 40 with a divider of 20 would add 2 levels to dropped scrap.";
        }

        public override void Execute(string arguments)
        {
            if (int.TryParse(arguments, out int value))
            {
                ScrapLevelDivider.Value = value;
                Messaging.Notification("Value has been set to " + ScrapLevelDivider.Value.ToString());
            }
            else
            {
                Messaging.Notification("Need a number. Current value: " + ScrapLevelDivider.Value.ToString());
            }
        }
    }
}
