using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class EtheralEX : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light");
			Tooltip.SetDefault(@"Etheral EX");
        }

	    public override void SetDefaults()
	    {
	        item.damage = 300;
	        item.magic = true;
	        item.mana = 20;
	        item.width = 60;
	        item.height = 26;
	        item.useTime = 10;
	        item.useAnimation = 10;
	        item.reuseDelay = 5;
	        item.useStyle = ItemUseStyleID.HoldingOut;
	        item.UseSound = SoundID.Item13;
	        item.noMelee = true;
            item.noUseGraphic = true;
			item.channel = true;
	        item.knockBack = 0f;
	        item.value = Item.sellPrice(0, 30, 0, 0);
            item.channel = true;
            item.shoot = mod.ProjectileType("EtheralEX");
            item.shootSpeed = 30f;           
            item.expert = true; item.expertOnly = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_NONE; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.overrideColor = new Color(159, 207, 190);
	            }
	        }
	    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Etheral");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}