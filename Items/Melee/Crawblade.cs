using Terraria;
using Terraria.ID;

namespace AAMod.Items.Melee   //where is located
{
    public class Crawblade : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 32;            
            item.melee = true;            
            item.width = 56;              
            item.height = 65;             
            item.useTime = 19;          
            item.useAnimation = 19;     
            item.useStyle = 1;        
            item.knockBack = 4;       
            item.rare = 5;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crawblade");
            Tooltip.SetDefault("");
        }
    }
}
