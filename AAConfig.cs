using Terraria.ModLoader.Config;

namespace AAMod
{
    public class AAConfigClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static AAConfigClient Instance; // See ExampleConfigServer.Instance for info.

        [Label("$Mods.AAMod.Common.AATownNPC")]
        [Tooltip("$Mods.AAMod.Common.AATownNPCInfo")]
        public bool NoAATownNPC;

        [Label("$Mods.AAMod.Common.DisableBossDialogue")]
        [Tooltip("$Mods.AAMod.Common.DisableBossDialogueInfo")]
        public bool NoBossDialogue;
    }
}
