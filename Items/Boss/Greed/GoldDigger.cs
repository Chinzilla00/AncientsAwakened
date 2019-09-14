using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed
{
    public class GoldDigger : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 40;                        
            item.magic = true;                     
            item.width = 46;
            item.height = 46;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;     
            item.noMelee = true;
            item.knockBack = 1;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 8;
            item.mana = 5;             
            item.UseSound = SoundID.Item21;            
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Dig");
            item.shootSpeed = 13f;     
        }   

    public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Digger");
            Tooltip.SetDefault(@"Fires a projectile that, upon collision with a tile, creates a fountain of coins
Fountains will spit less coins the more fountains are active");
            Item.staff[item.type] = true;
        }
    }
}
