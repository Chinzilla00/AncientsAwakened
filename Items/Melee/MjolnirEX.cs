using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class MjolnirEX : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stormbreaker");
			Tooltip.SetDefault("Forged to slay the mad titan himself"
            + "\nMelee strikes cause lightning to fall from the sky"
            +"\nRight click to throw"
			+"\nMjolnir EX");
        }

		public override void SetDefaults()
		{
			item.noMelee = true;
			item.useStyle = 1;
			item.shootSpeed = 16f;
			item.damage = 300;
			item.knockBack = 9f;
			item.width = 14;
			item.height = 28;
			item.UseSound = SoundID.Item1;
			item.useAnimation = 15;
			item.useTime = 15;
			item.noUseGraphic = true;
			item.rare = 9;
			item.value = Item.sellPrice(0, 25, 0, 0);
			item.melee = true;
			item.shoot = mod.ProjectileType("MjolnirEX");
			item.autoReuse = true;
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Electric);
			}
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}	
		
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse != 2)
			{
				item.noMelee = false;
				item.noUseGraphic = false;
				item.shoot = 0;
				return true;
			}
			if (player.altFunctionUse == 2 && player.ownedProjectileCounts[item.shoot] < 1)
			{
				item.noMelee = true;
				item.noUseGraphic = true;
				item.shoot = mod.ProjectileType("MjolnirEX");
				return true;
			}
			return false;
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Thunderstrike"));
            target.AddBuff(mod.BuffType("Electrified"), 300);
			Vector2 vector12 = new Vector2(target.Center.X, target.Center.Y);
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num75 = 20f;
			float num119 = vector12.Y;
			if (num119 > player.Center.Y - 200f)
			{
				num119 = player.Center.Y - 200f;
			}
			
            vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
			vector2.Y -= 100 * 1;
			Vector2 vector13 = vector12 - vector2;
			if (vector13.Y < 0f)
			{
				vector13.Y *= -1f;
			}
			if (vector13.Y < 20f)
			{
				vector13.Y = 20f;
			}
			vector13.Normalize();
			vector13 *= num75;
			float num82 = vector13.X;
			float num83 = vector13.Y;
			float speedX5 = num82;
			float speedY5 = num83 + Main.rand.Next(-5, 5) * 0.02f;
			int L = Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY5, 466, damage, knockback, player.whoAmI, vector13.ToRotation());
			Main.projectile[L].penetrate = -1;
			Main.projectile[L].hostile = false;
			Main.projectile[L].friendly = true;
			Main.projectile[L].melee = true;
			Main.projectile[L].usesLocalNPCImmunity = true;
			Main.projectile[L].localNPCHitCooldown = -1;
		}
		
		public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Mjolnir"));
			recipe.AddIngredient(null, "EXSoul");
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}
