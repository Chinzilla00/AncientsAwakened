using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class ChaosBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Chaotic Fury");
            Description.SetDefault("Your magic abilities are increased substantially");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.magicDamage += 0.20f;
			player.magicCrit += 15;
			player.manaCost -= 0.20f;
			player.statDefense += 12;
        }
    }
}