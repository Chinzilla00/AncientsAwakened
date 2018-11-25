using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Laser_Rifle : ModItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laser Carbine");
			Tooltip.SetDefault("Uses energy cells as ammo");
		}

		public override void SetDefaults()
		{
			item.damage = 53;
			item.ranged = true;
			item.width = 46;
			item.height = 22;
			item.useAnimation = 14;
			item.useTime = 14;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 4, 72, 0);
			item.rare = 5;
			item.UseSound = SoundID.Item12;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 22f;
			item.useAmmo = mod.ItemType("Energy_Cell");
			
			item.crit = 5;
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

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 2);
		}
	}
}
