using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata   //where is located
{
    public class HydraStabber : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Abyssal Shiv");
            Tooltip.SetDefault(@"Extremely fast and dangerous
IInflicts Moonraze");
            
        }

        
        public override void SetDefaults()
        {
            item.damage = 470;            
            item.melee = true;            
            item.width = 42;              
            item.height = 52;             
            item.useTime = 10;          
            item.useAnimation = 30;
            item.reuseDelay = 32;
            item.useStyle = ItemUseStyleID.Stabbing;        
            item.knockBack = 2f;      
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.UseSound = SoundID.Item103;      
            item.autoReuse = true;   
            item.useTurn = false;
            item.shoot = ModContent.ProjectileType<Projectiles.Yamata.AbyssLash>();
            item.shootSpeed = 10;
            item.rare = ItemRarityID.Cyan; AARarity = 13;
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

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.YamataDust>(), 0f, 0f, 46, default, 1.381579f)];
                dust.noGravity = true;
            }
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

        public void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Moonraze>(), 600);
        }
        
        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "TrueCopperShortsword");
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
