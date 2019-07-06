using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Melee   //where is located
{
    public class DragonBlade : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Blade");
            Tooltip.SetDefault("Shoots tiny swords!");
        }
        public override void SetDefaults()
        {

            item.damage = 54;            
            item.melee = true;            
            item.width = 60;              
            item.height = 60;             
            
            item.useTime = 30;          
            item.useAnimation = 30;     
            item.useStyle = 1;        
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = false;
			item.shoot = mod.ProjectileType("DragonSP");
			item.shootSpeed = 14f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "DragonSpirit", 30);   
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
