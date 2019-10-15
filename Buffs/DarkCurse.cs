using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace AAMod.Buffs
{
    public class DarkCurse : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Dark Curse");
            Description.SetDefault("You deal significanlty less damage!");
            Main.debuff[Type] = true;
            longerExpertDebuff = false;
        }
    }
    public class DarkCurseEffect : GlobalNPC
    {
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(mod.BuffType("DarkCurse")))
            {
                drawColor.R = (byte)(drawColor.R * .2f);
                drawColor.G = (byte)(drawColor.G * .2f);
                drawColor.B = (byte)(drawColor.B * .2f);
            }

        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (npc.HasBuff(mod.BuffType("DarkCurse")))
            {
                damage = (int)(damage * .5f);
            }
        }
    }
}
