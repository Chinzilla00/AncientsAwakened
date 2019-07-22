using Terraria.ID;

namespace AAMod.Items.Melee   //where is located
{
    public class Shadowban : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Shadowban");
            Tooltip.SetDefault("Despite the name, this hammer's ban is not very unnoticable");
        }

        public override void SetDefaults()
        {

            item.damage = 30;            
            item.melee = true;            
            item.width = 60;              
            item.height = 60;             
            item.useTime = 20;          
            item.useAnimation = 20;     
            item.useStyle = 1;        
            item.knockBack = 25;      
            item.value = 100000;        
            item.rare = 5;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = false;
            item.hammer = 80;
        }

    
    }
}
