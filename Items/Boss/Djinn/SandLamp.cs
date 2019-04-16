using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Djinn
{
    public class SandLamp : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Lamp");
        }

        public override void SetDefaults()
        {

            item.damage = 26;                        
            item.magic = true;            
            item.width = 24;
            item.height = 28;
            item.useTime = 10;
            item.useAnimation = 18;
            item.useStyle = 5;    
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 3;
            item.mana = 5;          
            item.UseSound = SoundID.Item21;      
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SandSpray"); 
            item.shootSpeed = 6f; 
        }
        
    }
}
