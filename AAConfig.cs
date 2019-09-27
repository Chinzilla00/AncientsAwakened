using Terraria.ModLoader.Config;

namespace AAMod
{
    public class AAConfigClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static AAConfigClient Instance; // See ExampleConfigServer.Instance for info.

        [Label("Disable AA Town NPCs")]
        [Tooltip("Disables this mod's town npcs from spawning, for those who'd prefer to have other npcs spawn quicker. Note: This does not affect Anubis due to him being key to progression.")]
        public bool NoAATownNPC;
    }
}
