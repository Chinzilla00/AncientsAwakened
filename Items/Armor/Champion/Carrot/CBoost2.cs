using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Champion.Carrot
{
    public class CBoost2 : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Champion Boost");
            Description.SetDefault("Increased stats");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)

        {
            AAPlayer mplayer = player.GetModPlayer<AAPlayer>();
            player.manaRegenBonus += 30;
            player.allDamage += 0.18f * mplayer.CarrotBuff;
            player.lifeRegen += 12 * mplayer.CarrotBuff;

            if (player.buffTime[buffIndex] == 2)
            {
                mplayer.CarrotBuff--;
                player.buffType[buffIndex] = mod.BuffType("CBoost1");
                player.buffTime[buffIndex] = 480;
            }
        }
    }
}