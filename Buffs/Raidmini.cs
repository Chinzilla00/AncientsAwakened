using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Raidmini : ModBuff
	{
		public override void SetDefaults()
		{
			// DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
			DisplayName.SetDefault("Raidmini");
			Description.SetDefault("Smol bot");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 1800000;
            player.GetModPlayer<AAPlayer>(mod).Raidmini = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("Raidmini")] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("Raidmini"), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}