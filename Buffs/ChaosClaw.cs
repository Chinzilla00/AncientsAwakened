using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class ChaosClaw : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Discordian Claw");
			Description.SetDefault("Summons a discordian claw to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType("AbyssClaw")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("BlazeClaw")] > 0)
            {
				modPlayer.ChaosClaw = true;
			}
			if (!modPlayer.ChaosClaw)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}