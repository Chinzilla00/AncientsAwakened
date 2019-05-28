using Terraria.ID;
using Microsoft.Xna.Framework;
namespace AAMod.Items.Melee  //where is located
{
    public class CthulhusBlade : BaseAAItem
    {
        
        public override void SetDefaults()
        {

            item.damage = 23;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 48;              //Sword width
            item.height = 52;             //Sword height
            item.useTime = 22;          //how fast 
            item.useAnimation = 22;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 7;      //Sword knockback
            item.value = 19000;        
            item.rare = 3;
            item.UseSound = SoundID.Item1;                  //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cthulhu's Blade");
            Tooltip.SetDefault("");
        }
    }
}
