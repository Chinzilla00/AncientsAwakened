using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Toad
{
    public class ToadTongue : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Toad Tongue");
            Tooltip.SetDefault(@"Pulls enemies towards you when it retracts");
        }

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 44;
            item.value = Item.sellPrice(0, 0, 70, 0);
            item.rare = 4;
            item.noMelee = true;
            item.useStyle = 5;
            item.useAnimation = 40;
            item.useTime = 40;
            item.knockBack = 8f;
            item.damage = 40;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("ToadTongue");
            item.shootSpeed = 14;
            item.UseSound = SoundID.Item1;
            item.melee = true;
        }
    }
}