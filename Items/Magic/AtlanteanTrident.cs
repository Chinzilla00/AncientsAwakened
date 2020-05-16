using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
	public class AtlanteanTrident : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atlantean Trident");
			Tooltip.SetDefault("Fires off a tri-shot of water bolts");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.magic = true;
			item.mana = 8;
			item.width = 68;
			item.height = 68;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 500000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item21;
			item.autoReuse = true;
			item.shoot = ProjectileID.WaterBolt;
			item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, Main.myPlayer);
            }
            return false;
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("OceanTrident"));
			recipe.AddIngredient(mod.ItemType("BlazePike"));
			recipe.AddIngredient(mod.ItemType("SandLamp"));
			recipe.AddIngredient(mod.ItemType("NeutronStaff"));
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
			
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("OceanTrident"));
			recipe.AddIngredient(mod.ItemType("SludgeShot"));
			recipe.AddIngredient(mod.ItemType("Sickle"));
			recipe.AddIngredient(mod.ItemType("NeutronStaff"));
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}