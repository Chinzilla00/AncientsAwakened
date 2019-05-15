using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata
{
    public class Darksprayer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darksprayer");
            Tooltip.SetDefault(@"'Spouts of dark, leaves it's mark'
Inflicts Moonrazed");           
        }

        public override void SetDefaults()
        {
            item.damage = 200;
            item.ranged = true;
            item.width = 44;
            item.height = 34;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.useAmmo = AmmoID.Rocket;
            item.knockBack = 8f;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.UseSound = SoundID.Item38;      //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.shootSpeed = 20f;
            item.shoot = mod.ProjectileType("Moonblow");
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);

        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            /*Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
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
            );*/
		}

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Moonblow"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Yamata;;
                }
            }
        }

        /*public void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Moonraze"), 600);
        }*/
	
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(ItemID.SnowmanCannon);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
