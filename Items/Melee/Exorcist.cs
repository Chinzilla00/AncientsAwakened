using Terraria.ID;

namespace AAMod.Items.Melee   //where is located
{
    public class Exorcist : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 108;            
            item.melee = true;            
            item.width = 56;              
            item.height = 60;             
            item.useTime = 10;          
            item.useAnimation = 10;     
            item.useStyle = 1;        
            item.knockBack = 5;      
            item.value = 20000;        
            item.rare = 8;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = true; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exorcist");
            Tooltip.SetDefault("");
        }
    }
}
