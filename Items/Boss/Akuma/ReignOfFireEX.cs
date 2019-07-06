using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma   //where is located
{
    public class ReignOfFireEX : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Draconian Fury");
            Tooltip.SetDefault(@"Rains fire and fury upon your foes
Inflicts Daybroken
Reign of Fire EX");
        }

        
        public override void SetDefaults()
        {
            item.damage = 450;            
            item.melee = true;            
            item.width = 86;              
            item.height = 86;             
            item.useTime = 25;          
            item.useAnimation = 25;     
            item.useStyle = 1;        
            item.knockBack = 6.5f;      
            item.value = Item.sellPrice(3, 0, 0, 0);
			item.UseSound = SoundID.Item20;
            item.autoReuse = true;   
            item.useTurn = true;
            item.expert = true;
            item.rare = 9;
            AARarity = 13;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/ReignOfFire_Glow");
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
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType<Dusts.AkumaDust>(), 0f, 0f, 46, new Color(255, 75, 0), 1.381579f)];
                dust.noGravity = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Main.PlaySound(2, target.Center, 124);
            Vector2 vector12 = new Vector2(target.Center.X, target.Center.Y);
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num75 = 20f;
			float num119 = vector12.Y;
			if (num119 > player.Center.Y - 200f)
			{
				num119 = player.Center.Y - 200f;
			}
            for (int num120 = 0; num120 < 3; num120++)
            {
                vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
                vector2.Y -= 100 * num120;
                Vector2 vector13 = vector12 - vector2;
                if (vector13.Y < 0f)
                {
                    vector13.Y *= -1f;
                }
                if (vector13.Y < 20f)
                {
                    vector13.Y = 20f;
                }
                vector13.Normalize();
                vector13 *= num75;
                float num82 = vector13.X;
                float num83 = vector13.Y;
                float speedX5 = num82;
                float speedY6 = num83 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, mod.ProjectileType("FireProjEX"), damage, knockBack, Main.myPlayer);
            }
            target.AddBuff(BuffID.Daybreak, 600);
        }
        
        
        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ReignOfFire");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
