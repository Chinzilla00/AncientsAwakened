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
            Tooltip.SetDefault(@"Inflicts Moonraze");
		}

		public override void SetDefaults()
		{
            item.CloneDefaults(ItemID.Arkhalis);
            item.damage = 200;
            item.width = 70; 
            item.height = 80;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.useTime = 5;
            item.knockBack = 4f;
            item.autoReuse = false;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.shoot = mod.ProjectileType("Surasshu");
            item.shootSpeed = 15f;
            item.rare = 11;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Moonraze"), 600);
        }
    }
}