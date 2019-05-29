using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee     //We need player to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class ScytheOfDecay : BaseAAItem
    {

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scythe of Evil");
            Tooltip.SetDefault(@"The scythe of the lord of death himself
Inflicts Ichor and Cursed Inferno
Death Sickle EX");
        }

        public override void SetDefaults()
        {

            item.damage = 400;  
            item.melee = true; 
            item.width = 80;    
            item.height = 72; 

            item.useTime = 6; 
            item.useAnimation = 6;
            item.channel = true;
            item.useStyle = 100;  
            item.knockBack = 2f; 
            item.value = Item.buyPrice(1, 0, 0, 0); 
            item.rare = 9;
            item.expert = true;
            item.shoot = mod.ProjectileType("DecayScythe"); 
            item.noUseGraphic = true; 

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; 
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_NONE;
            glowmaskDrawColor = Color.White;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DeathSickle);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
 
        public override bool UseItemFrame(Player player)  
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vector = new Vector2(player.position.X + (float)player.width * 0.5f, player.position.Y + (float)player.height * 0.5f);
            float num22 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
            float num23 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
            if (player.gravDir == -1f)
            {
                num23 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector.Y;
            }
            float num24 = (float)Math.Sqrt((double)(num22 * num22 + num23 * num23));
            if ((float.IsNaN(num22) && float.IsNaN(num23)) || (num22 == 0f && num23 == 0f))
            {
                num22 = (float)player.direction;
                num23 = 0f;
                num24 = 10;
            }
            else
            {
                num24 = 10 / num24;
            }
            num22 *= num24;
            num23 *= num24;
            Projectile.NewProjectile(vector.X, vector.Y, num22, num23, mod.ProjectileType<Projectiles.DecayScytheProj>(), damage, knockBack, player.whoAmI, 0f, 0f);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
