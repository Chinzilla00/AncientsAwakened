using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.Localization;

namespace AAMod.Buffs
{
	public class EyeOfJudgement : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Eye of Judgement");
			Description.SetDefault(@"Eye of Judgement is protecting you
Damage and speed are increased");
			Main.debuff[Type] = false;
			canBeCleared = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.meleeDamage += 0.2f;
			player.rangedDamage += 0.2f;
			player.magicDamage += 0.2f;
			player.minionDamage += 0.2f;
			player.thrownDamage += 0.2f;
			player.moveSpeed += 0.25f;
			if (player.ownedProjectileCounts[mod.ProjectileType("EyeOfJudgement")] <= 0)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y-90, 0f, 0f, mod.ProjectileType("EyeOfJudgement"), 100, 0, player.whoAmI);
			}
		}
	}
}
