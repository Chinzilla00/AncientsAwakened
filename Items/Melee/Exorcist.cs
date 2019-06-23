using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class Exorcist : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 108;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 56;              //Sword width
            item.height = 60;             //Sword height
            item.useTime = 10;          //how fast 
            item.useAnimation = 10;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 5;      //Sword knockback
            item.value = 20000;        
            item.rare = 8;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exorcist");
            Tooltip.SetDefault("");
        }
    }
}
