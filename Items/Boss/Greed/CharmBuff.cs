using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed
{
    public class CharmBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Desire");
			Description.SetDefault(@"MORE COINS, MORE POWER!!!");
		}

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
            player.GetModPlayer<AAPlayer>(mod).Greed1 = true;
            player.allDamage += player.GetModPlayer<AAPlayer>(mod).GreedyDamage / 100f;
        }
	}
}