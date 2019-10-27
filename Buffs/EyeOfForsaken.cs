using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class EyeOfForsaken : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Eye of the Forsaken");
			Description.SetDefault(@"Eye of the Forsaken is protecting you
Damage and speed are increased");
			Main.debuff[Type] = false;
			canBeCleared = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.meleeDamage += 0.25f;
			player.rangedDamage += 0.25f;
			player.magicDamage += 0.25f;
			player.minionDamage += 0.25f;
			player.thrownDamage += 0.25f;
			player.moveSpeed += 0.35f;
			if (player.ownedProjectileCounts[mod.ProjectileType("EyeOfForsaken")] <= 0)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y-90, 0f, 0f, mod.ProjectileType("EyeOfForsaken"), 150, 0, player.whoAmI);
			}
		}
	}
}
