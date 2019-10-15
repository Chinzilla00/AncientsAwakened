using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class ManaOverload : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mana Overload");
            Description.SetDefault("Double magic attack speed");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }
        
    }
    public class ManaOverloadEffect : ModPlayer
    {
        public override void PostItemCheck()
        {
            if (player.HasBuff(mod.BuffType("ManaOverload")) && player.HeldItem.magic)
            {
                if (player.itemAnimation > 0)
                {
                    player.itemAnimation--;
                }
                else
                {
                    player.itemAnimation = 0;
                }
                if (player.itemTime > 0)
                {
                    player.itemTime--;
                }
                else
                {
                    player.itemTime = 0;
                }
            }
        }
    }
}
