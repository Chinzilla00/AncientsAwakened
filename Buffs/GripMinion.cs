using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class GripMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Grips of Chaos");
			Description.SetDefault("Grabby Hands");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("DragonClaw")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("HydraClaw")] > 0)
            {
				modPlayer.GripMinion = true;
			}
			if (!modPlayer.ChairMinion)
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