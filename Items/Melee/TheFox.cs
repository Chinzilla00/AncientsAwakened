using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class TheFox : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 77;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 58;              //Sword width
            item.height = 58;             //Sword height
            item.useTime = 20;          //how fast 
            item.useAnimation = 20;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 5;      //Sword knockback
            item.value = 10000;        
            item.rare = 5;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true; 
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("The Fox");
      Tooltip.SetDefault("What does it say");
    }
    }
}
