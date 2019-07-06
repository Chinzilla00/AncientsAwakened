using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class TheFox : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 77;            
            item.melee = true;            
            item.width = 58;              
            item.height = 58;             
            item.useTime = 20;          
            item.useAnimation = 20;     
            item.useStyle = 1;        
            item.knockBack = 5;      
            item.value = 10000;        
            item.rare = 5;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = true; 
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("The Fox");
      Tooltip.SetDefault("What does it say");
    }
    }
}
