using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Flasks
{
    public class OrderBottle : BaseAAItem
	{
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 26;
            item.maxStack = 999;
            item.consumable = true;
            item.useTime = 28;
            item.useAnimation = 28;
            item.shoot = ModContent.ProjectileType<Projectiles.OrderSolution>();
            item.shootSpeed = 1f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.noUseGraphic = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Order Flask");
            Tooltip.SetDefault(@"Brings order to the Chaos Biomes");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.shoot = mod.ProjectileType("OrderBottle");
                item.shootSpeed = 9f;
            }
            else
            {
                item.shoot = ModContent.ProjectileType<Projectiles.OrderSolution>(); ;
                item.shootSpeed = 2f;
            }
            return base.CanUseItem(player);
        }
    }
}
