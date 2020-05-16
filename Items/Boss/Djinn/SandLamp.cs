using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Djinn
{
    public class SandLamp : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Lamp");
        }

        public override void SetDefaults()
        {

            item.damage = 24;                        
            item.magic = true;            
            item.width = 24;
            item.height = 28;
            item.useTime = 15;
            item.useAnimation = 18;
            item.useStyle = ItemUseStyleID.HoldingOut;    
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.Orange;
            item.mana = 7;          
            item.UseSound = SoundID.Item21;      
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SandSpray"); 
            item.shootSpeed = 9f; 
        }
        
    }
}
