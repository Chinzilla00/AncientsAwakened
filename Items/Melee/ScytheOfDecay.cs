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
            item.value = Item.sellPrice(1, 0, 0, 0); 
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
    }
}
