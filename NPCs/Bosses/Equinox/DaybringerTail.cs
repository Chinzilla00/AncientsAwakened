using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Equinox
{
    public class DaybringerTail : DaybringerHead
    {
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

        /* public override bool CheckActive()
         {
             if (NPC.AnyNPCs(mod.NPCType<DaybringerHead>()))
             {
                 return false;
             }
             return true;
         }*/
    }
}