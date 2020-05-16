using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Serpent
{
    public class SerpentSpike : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Serpent Spike");
        }

        public override void SetDefaults()
        {
            item.damage = 30; 
            item.melee = true;
            item.width = 132;
            item.height = 132;
            item.scale = 1.1f;
            item.maxStack = 1;
            item.useTime = 25; 
            item.useAnimation = 25;
            item.knockBack = 2f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(0, 5, 0, 0); 
            item.rare = ItemRarityID.Orange;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("SerpentSpike");  
            item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }
    }
}
