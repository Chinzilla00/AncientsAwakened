using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class GuardianNight : BaseAAItem
    {
        
        public override void SetDefaults()
        {

            item.damage = 174;            
            item.melee = true;            
            item.width = 78;              
            item.height = 78;             
            item.useTime = 26;          
            item.useAnimation = 26;     
            item.useStyle = 1;        
            item.knockBack = 4;      
            item.value = 20;        
            item.rare = 7;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guardian of the Depths");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
			recipe.AddIngredient(null, "DeepAbyssium", 10);
			recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
