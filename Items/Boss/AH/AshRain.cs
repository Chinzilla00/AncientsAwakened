using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class AshRain : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ash Rain");
			Tooltip.SetDefault(@"Shoots fireball which explodes on hit or after some time");
        }

        public override void SetDefaults()
        {
            item.damage = 150;                        
            item.magic = true;            
            item.width = 24;
            item.height = 28;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;    
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = 9;
            AARarity = 12;
            item.mana = 5;          
            item.UseSound = SoundID.Item20;      
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FireMagic"); 
            item.shootSpeed = 11f; 
        }
    }
}
