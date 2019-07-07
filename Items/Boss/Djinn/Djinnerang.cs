using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Djinn
{
    public class Djinnerang : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Djinnerang");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
		{

            item.damage = 30;            
            item.melee = true;
            item.width = 30;
            item.height = 30;
			item.useTime = 12;
			item.useAnimation = 12;
            item.noUseGraphic = true;
            item.useStyle = 1;
			item.knockBack = 0;
			item.value = 8;
			item.rare = 6;
			item.shootSpeed = 7f;
			item.shoot = mod.ProjectileType ("Djinnerang");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.value = 50000;
        }

    

        public override bool CanUseItem(Player player) 
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
