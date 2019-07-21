using Terraria.ID;
using Microsoft.Xna.Framework;
namespace AAMod.Items.Melee  //where is located
{
    public class CthulhusBlade : BaseAAItem
    {
        
        public override void SetDefaults()
        {

            item.damage = 23;            
            item.melee = true;            
            item.width = 48;              
            item.height = 52;             
            item.useTime = 22;          
            item.useAnimation = 22;     
            item.useStyle = 1;        
            item.knockBack = 7;      
            item.value = 19000;        
            item.rare = 3;
            item.UseSound = SoundID.Item1;                  
            item.autoReuse = true;   
            item.useTurn = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cthulhu's Blade");
            Tooltip.SetDefault("");
        }
    }
}
