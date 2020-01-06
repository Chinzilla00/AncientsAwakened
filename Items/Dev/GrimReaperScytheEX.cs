using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class GrimReaperScytheEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Shredder");
            Tooltip.SetDefault(@"Left click to swing and release homing scythe
Right click to do dashing hit
You are immune during the dash and deal 15x damage in true melee
Dashing ability has 5 seconds CD
'Well, how many Grim Reapers have you met before, mate?'
-Gregg
Scythe of the Grim Reaper EX");
		}

		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.useStyle = 1;
			item.useAnimation = 30;
			item.useTime = 30;
			item.knockBack = 6f;
			item.width = 24;
			item.height = 28;
			item.damage = 225;
			item.crit = 14;
			item.scale = 1.15f;
			item.UseSound = SoundID.Item71;
			item.rare = 11;
			item.shoot = mod.ProjectileType("GrimReaperScytheEX");
			item.shootSpeed = 16f;
			item.value = 1000000;
			item.melee = true;
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			int side = player.direction;
			if (player.altFunctionUse != 2)
			{
				item.shoot = mod.ProjectileType("GrimReaperScytheEX");
				return true;
			}
			if (player.altFunctionUse == 2 && !player.HasBuff(mod.BuffType("ReaperCD")))
			{
				player.AddBuff(mod.BuffType("ReaperImmune2"), 60);
				player.AddBuff(mod.BuffType("ReaperCD"), 300);
				item.shoot = mod.ProjectileType("ReaperHitbox");
				player.velocity.X = 26f * side;
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == mod.ProjectileType("GrimReaperScytheEX"))
			{
				float num121 = 0.99f;
				int num122 = 3;
				Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
				float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
				float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
				Vector2 vector14 = new Vector2(speedX, speedY);
				vector14.Normalize();
				vector14 *= 40f;
				bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
				for (int num123 = 0; num123 < num122; num123++)
				{
					float num124 = num123 - (num122 - 1f) / 2f;
					Vector2 vector15 = vector14.RotatedBy(num121 * num124, default);
					if (!flag11)
					{
						vector15 -= vector14;
					}
					if (type == mod.ProjectileType("GrimReaperScytheEX") && player.HasBuff(mod.BuffType("ReaperImmune2")))
					{
						Projectile.NewProjectile(vector2.X + vector15.X, vector2.Y + vector15.Y, num82, num83, type, damage/15, knockBack, player.whoAmI);
					}
					else
					{
						Projectile.NewProjectile(vector2.X + vector15.X, vector2.Y + vector15.Y, num82, num83, type, damage, knockBack, player.whoAmI);
					}
				}
			}
			if (type == mod.ProjectileType("ReaperHitbox"))
			{
				return true;
			}
			return false;
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GrimReaperScythe", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
	}
}
