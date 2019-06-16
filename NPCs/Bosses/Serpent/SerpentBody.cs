using Microsoft.Xna.Framework;
using Terraria;

namespace AAMod.NPCs.Bosses.Serpent
{
    public class SerpentBody : SerpentHead
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subzero Serpent");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<SerpentHead>()))
            {
                return false;
            }
            return true;
        }
    }
}