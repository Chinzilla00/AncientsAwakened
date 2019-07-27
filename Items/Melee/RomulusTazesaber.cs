using Terraria.ID;

namespace AAMod.Items.Melee   //where is located
{
    public class RomulusTazesaber : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 100;            
            item.melee = true;            
            item.width = 58;              
            item.height = 58;             
            item.useTime = 17;          
            item.useAnimation = 17;     
            item.useStyle = 1;        
            item.knockBack = 0;      
            item.value = 10000;        
            item.rare = 7;
            item.UseSound = SoundID.Item15;       
            item.autoReuse = true;   
            item.useTurn = true; 
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Romulus' Tazesaber");
          Tooltip.SetDefault("A fulgarian Tazesaber stolen from a respected hero.");
        }
    }
}
