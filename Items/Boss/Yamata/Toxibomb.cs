using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata        //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class Toxibomb : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 200;                      
            item.magic = true;  
            item.width = 32;     
            item.height = 28;    
            item.useTime = 26; 
            item.useAnimation = 26; 
            item.useStyle = 5;        
            item.noMelee = true;   
            item.knockBack = 1; 
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.mana = 9;
            item.UseSound = SoundID.Item1; 
            item.autoReuse = true; 
            item.shoot = mod.ProjectileType("Toxibomb");  
            item.shootSpeed = 20f;    
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Bomb");
			Tooltip.SetDefault("Fires off explosive spirit bombs");
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(45, 46, 70);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "BogBomb", 1);
            recipe.AddTile(null, "BinaryReassembler");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
