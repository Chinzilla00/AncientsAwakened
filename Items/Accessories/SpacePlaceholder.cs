using Terraria;
using Terraria.ModLoader;
using System;
using ReLogic.Utilities;
using System.Collections.Generic;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Accessories
{
    public class SpacePlaceholder : ModNPC
    {
		public bool isAwakened = false;
		
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Space Portal");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
			npc.life = 1;
            npc.damage = 0;
            npc.defense = 0;
            npc.width = 32;
            npc.height = 32;
            npc.npcSlots = 0;
            npc.noTileCollide = true;
            npc.noGravity = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.dontTakeDamage = true;
            npc.immortal = true;
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
