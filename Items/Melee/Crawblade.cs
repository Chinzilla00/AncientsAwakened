using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class Crawblade : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 32;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 56;              //Sword width
            item.height = 65;             //Sword height
            item.useTime = 19;          //how fast 
            item.useAnimation = 19;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 4;      //Sword knockback
            item.value = 20;        
            item.rare = 5;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crawblade");
            Tooltip.SetDefault("");
        }
    }
}
