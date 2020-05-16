using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;

namespace AAMod.Items.Boss.Shen   //where is located
{
    public class MeteorStrike : BaseAAItem
    {

        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Meteor Strike");
            Tooltip.SetDefault(@"Fires a barrage of meteors at your foes
Hitting enemies causes a smaller, but more damaging explosion
Hitting a tile causes a larger, but less damaging projectile
Inflicts Discordian Inferno");

        }


        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("Meteor");
            item.damage = 400;            
            item.magic = true;            //if it's magic
            item.width = 32;              
            item.height = 36;             
            item.useTime = 16;          
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.HoldingOut;      
            item.knockBack = .5f;
            item.value = Item.sellPrice(1, 50, 0, 0);
            item.mana = 10;
            item.UseSound = new LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound);
            item.autoReuse = true;   
            item.useTurn = true;
            item.shootSpeed = 16f;
            AARarity = 14;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
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

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = Main.rand.Next(3);

            switch (type)
            {
                case 0:
                    type = mod.ProjectileType("Meteor");
                    break;
                case 1:
                    type = mod.ProjectileType("MeteorRed");
                    break;
                default:
                    type = mod.ProjectileType("MeteorBlue");
                    break;
            }


            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
            return false;
        }

        public void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("DiscordInferno"), 600);
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SunStorm", 1);
            recipe.AddIngredient(null, "Toxibomb", 1);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
