using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Melee   //where is located
{
    public class Dragonkite : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonkite");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.damage = 150;            
            item.melee = true;            
            item.width = 176;              
            item.height = 176;             
            item.useTime = 45;          
            item.useAnimation = 45;     
            item.useStyle = 1;        
            item.knockBack = 4;      
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item20;       
            item.autoReuse = true;   
            item.useTurn = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "RadiantIncinerite", 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 15); 
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
