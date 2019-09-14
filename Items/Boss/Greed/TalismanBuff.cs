using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed
{
    public class TalismanBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ruthless Desire");
			Description.SetDefault("MONEY MONEY MONEY!!!");
		}

		public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
            player.GetModPlayer<AAPlayer>(mod).Greed2 = true;
            player.allDamage += player.GetModPlayer<AAPlayer>(mod).GreedyDamage / 100f;
        }
	}
}