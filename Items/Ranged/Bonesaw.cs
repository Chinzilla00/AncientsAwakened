using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Bonesaw : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bonesaw");
			Tooltip.SetDefault("Turns bullets into ichor bullets!"
				+ "\n16% chance not to consume ammo.");
		}

		public override void SetDefaults()
		{
			item.damage = 23;
			item.ranged = true;
			item.width = 48;
			item.height = 18;
			item.useAnimation = 10;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 3, 75, 0);
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item41;
			item.autoReuse = true;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			item.crit = 3;
		}
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Ichor, 25);
			recipe.AddIngredient(ItemID.SoulofNight, 15);
			recipe.AddIngredient(ItemID.CrimtaneBar, 12);
			recipe.AddIngredient(ItemID.TheUndertaker);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = ProjectileID.IchorBullet;
            return true;
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

        // What if I wanted this gun to have a 2% chance not to consume ammo?

        public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .16f;
		}
	}
}
