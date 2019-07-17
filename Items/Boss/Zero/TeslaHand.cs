using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class TeslaHand : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broken Zero Weapon");
            Tooltip.SetDefault("Will not stop spewing tesla bolts");
        }

        public override void SetDefaults()
        {
            item.ranged = true;
            item.useTime = 4;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2.5f;
            item.autoReuse = true;
            item.reuseDelay = 15;
            item.useAnimation = 12;
            item.shootSpeed = 16f;
            item.width = 36;
            item.height = 42;
            item.damage = 240;
            item.UseSound = SoundID.Item116;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.shoot = mod.ProjectileType("Teslashock");
            item.rare = 9; AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
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

        // How can I make the shots appear out of the muzzle exactly?
        // Also, when I do this, how do I prevent shooting through tiles?
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ApocalyptitePlate", 5);
			recipe.AddIngredient(null, "UnstableSingularity", 5);
	        recipe.AddTile(null, "ACS");
	        recipe.SetResult(this);
	        recipe.AddRecipe();
		}
	}
}
