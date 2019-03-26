using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class AshRain : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ash Rain");
			Tooltip.SetDefault(@"Shoots fireball which explodes on hit or after some time");
        }

        public override void SetDefaults()
        {
            item.damage = 120;                        
            item.magic = true;            
            item.width = 24;
            item.height = 28;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;    
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 3;
            item.mana = 5;          
            item.UseSound = SoundID.Item21;      
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FireMagic"); 
            item.shootSpeed = 11f; 
        }
    }
}
