using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class DoomRay : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nova Focus");
            Tooltip.SetDefault("Fires an insanely powerful death laser");
        }

        public override void SetDefaults()
        {

            item.useStyle = 5;
            item.useAnimation = 7;
            item.useTime = 7;
            item.mana = 10;
            item.shootSpeed = 16f;
            item.knockBack = 0f;
            item.width = 122;
            item.reuseDelay = 5;
            item.height = 32;
            item.damage = 250;
            item.UseSound = SoundID.Item13;
            item.channel = true;
            item.shoot = mod.ProjectileType("DoomRay");
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.noMelee = true;
            item.magic = true;
            item.autoReuse = true;
            item.rare = 9; AARarity = 13;
        }

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-45, -3);
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
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * .5f + 2f
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
        
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ApocalyptitePlate", 5);
			recipe.AddIngredient(null, "UnstableSingularity", 5);
			recipe.AddIngredient(ItemID.ChargedBlasterCannon);
	        recipe.AddTile(null, "ACS");
	        recipe.SetResult(this);
	        recipe.AddRecipe();
		}
	}
}
