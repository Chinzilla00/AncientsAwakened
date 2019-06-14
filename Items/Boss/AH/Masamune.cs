using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class Masamune : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Masamune");
            Tooltip.SetDefault(@"Left click to quickly slash at your foes with the blade");
		}

		public override void SetDefaults()
		{
            item.damage = 200;
            item.width = 70; 
            item.height = 80;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.useAnimation = 25;
            item.useTime = 15;
            item.useStyle = 5;
            item.useTime = 5;
            item.knockBack = 4f;
            item.autoReuse = false;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.shoot = mod.ProjectileType("Surasshu");
            item.shootSpeed = 15f;
            item.rare = 11;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                item.melee = true;
                item.noMelee = true;
                item.noUseGraphic = true;
                item.channel = true;
                item.useAnimation = 25;
                item.useTime = 5;
                item.useStyle = 5;
                item.autoReuse = false;
                item.shoot = mod.ProjectileType("Surasshu");
                item.shootSpeed = 15f;
            }
            else
            {
                item.melee = true;
                item.noMelee = false;
                item.noUseGraphic = false;
                item.channel = false;
                item.useAnimation = 15;
                item.useTime = 15;
                item.useStyle = 1;
                item.autoReuse = true;
                item.shoot = mod.ProjectileType("MasamuneSlash");
                item.shootSpeed = 12f;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Moonraze"), 600);
        }
    }
}