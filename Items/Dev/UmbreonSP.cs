using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class UmbreonSP : ModItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blade of Night");
			Tooltip.SetDefault("A dark sword from a dark creature.");
		}
		
		public override void SetDefaults()
		{
			item.damage = 200;
			item.melee = true;
			item.width = 84;
			item.height = 84;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 5;
			item.value = Item.buyPrice(0, 1, 50, 0);
			item.rare = 2;
			item.UseSound = SoundID.Item71;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("UmbreonSPProjectile");
			item.shootSpeed = 20f;
			
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
