using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Akuma
{
	public class DaybreakArrow : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daybreak Arrow");
			Tooltip.SetDefault(@"Scorches its target with the heat of the blazing sun
Inflicts Daybroken
Non-consumable");
		}

		public override void SetDefaults()
		{
			item.damage = 23;
			item.ranged = true;
			item.width = 14;
			item.height = 40;
			item.consumable = false;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 7f;
			item.value = Item.sellPrice(0, 30, 0, 0); ;
			item.rare = 6;
			item.shoot = mod.ProjectileType("DaybreakArrow");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 3f;                  //The speed of the projectile
			item.ammo = AmmoID.Arrow;
            item.rare = 10;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MoonlordArrow, 999);
            recipe.AddIngredient(null, "DaybreakIncinerite", 1);
            recipe.AddIngredient(null, "CrucibleScale", 1);
            recipe.AddTile(null, "ACS");
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}
