using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class CCRune : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Rune");
			Description.SetDefault("Summons runes to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			bool flag = player.ownedProjectileCounts[mod.ProjectileType("BunnyRune")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("DiscordRune")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("EnergyRune")] > 0;
			bool flag2 = player.ownedProjectileCounts[mod.ProjectileType("TerraRune")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("ChaosRune")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("VoidRune")] > 0;
			if (flag)
			{
				modPlayer.WeakCCRune = true;
			}
			if (flag2)
			{
				modPlayer.CCRune = true;
			}
			float slotscanuse = player.maxMinions - player.slotsMinions;
			if (modPlayer.CCBook)
			{
				if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.BunnyRune>()] < 1 && slotscanuse > 1f)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.BunnyRune>(), (int)(1 * player.minionDamage), 0, player.whoAmI, 0f, 0f);
				}
				if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.DiscordRune>()] < 1 && slotscanuse > 2f)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.DiscordRune>(), (int)(50 * player.minionDamage), 4f, player.whoAmI, 0f, 0f);
				}
				if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.EnergyRune>()] < 1 && slotscanuse > 3f)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.EnergyRune>(), (int)(100 * player.minionDamage), 2f, player.whoAmI, 0f, 0f);
				}
			}
			if (modPlayer.CCBookEX)
			{
				if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.TerraRune>()] < 1 && slotscanuse > 1f)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.TerraRune>(), (int)(1 * player.minionDamage), 0, player.whoAmI, 0f, 0f);
				}
				if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.ChaosRune>()] < 1 && slotscanuse > 2f)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.ChaosRune>(), (int)(400 * player.minionDamage), 4f, player.whoAmI, 0f, 0f);
				}
				if (player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.VoidRune>()] < 1 && slotscanuse > 3f)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.VoidRune>(), (int)(800 * player.minionDamage), 2f, player.whoAmI, 0f, 0f);
				}
			}
			if (!modPlayer.WeakCCRune && !modPlayer.CCRune && !modPlayer.CCBook && !modPlayer.CCBookEX)
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