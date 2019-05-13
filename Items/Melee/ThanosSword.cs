using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee     //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class ThanosSword : BaseAAItem
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
            item.damage = 170;  
            item.melee = true;
            item.noMelee = true;
            item.width = 88;    
            item.height = 100; 
            item.useTime = 6; 
            item.useAnimation = 6;
            item.channel = true;
            item.useStyle = 100;  
            item.knockBack = 0f; 
            item.value = Item.buyPrice(1, 0, 0, 0); 
            item.rare = 11;
            item.shoot = mod.ProjectileType("ThanosSword"); 
            item.noUseGraphic = true;
        }
        
        public override bool UseItemFrame(Player player)
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                item.useStyle = 100;
                item.shoot = mod.ProjectileType("ThanosSword");
            }
            else
            {
                item.useStyle = 5;
                item.shoot = mod.ProjectileType("ThanosSwordT");
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiumBar", 10);
            recipe.AddIngredient(null, "DarkMatter", 10);
            recipe.AddIngredient(null, "DarkShredders", 1);
            recipe.AddIngredient(null, "BreakingDawn", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
