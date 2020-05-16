using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Serpent
{
    public class BlizzardBuster : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blizzard Breaker");
        }
        public override void SetDefaults()
		{
            item.damage = 26;            
            item.melee = true;
            item.noMelee = true;
            item.width = 30;
            item.height = 30;
			item.useTime = 26;
			item.useAnimation = 26;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 0;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.Orange;
			item.shootSpeed = 10f;
			item.shoot = mod.ProjectileType ("BB");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
