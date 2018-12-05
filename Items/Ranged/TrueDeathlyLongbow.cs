using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using System;

namespace AAMod.Items.Ranged
{
    public class TrueDeathlyLongbow : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Deathly Longbow");
            Tooltip.SetDefault("Replaces Arrows with Reaper Arrows");

        }

        public override void SetDefaults()
		{
			item.damage = 75;
			item.ranged = true;
			item.width = 46;
			item.height = 86;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 0;
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Arrow;
            
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
            return new Vector2(-3, 0);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.statLife += (damage / 8);
            player.HealEffect(damage / 8);
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
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("ReaperArrow"), damage, knockBack, player.whoAmI, 0f, 0f);
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DeathlyLongbow", 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 20);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
