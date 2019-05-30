using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
	public class TiedemiesBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Tiedemies");
			Description.SetDefault("\"He will light your way\"");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<AAPlayer>(mod).TiedHead = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("TiedemiesPet")] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("TiedemiesPet"), 0, 0f, player.whoAmI, 0f, 0f);
			}
            if (!player.GetModPlayer<AAPlayer>(mod).Tied)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
	}
}