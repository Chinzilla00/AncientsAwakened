using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class Shadowban : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Shadowban");
            Tooltip.SetDefault("Despite the name, this hammer's ban is not very unnoticable");
        }

        public override void SetDefaults()
        {

            item.damage = 45;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 60;              //Sword width
            item.height = 60;             //Sword height
            item.useTime = 40;          //how fast 
            item.useAnimation = 40;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 25;      //Sword knockback
            item.value = 100000;        
            item.rare = 5;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = false;   //if it's capable of autoswing.
            item.useTurn = false; 
        }

    
    }
}
