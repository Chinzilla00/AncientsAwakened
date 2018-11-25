using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Ranged.Ammo
{
    public class Energy_Cell : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 5;
			item.width = 8;
			item.height = 16;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.rare = 5;
			item.consumable = true;
			item.shoot = mod.ProjectileType("Energy_Cell_Pro");
			item.ammo = item.type;
			
		}
		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energy Cell");
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
    }
}
