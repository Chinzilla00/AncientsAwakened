using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Wings
{

    [AutoloadEquip(EquipType.Wings)]
    public class MagmancerWings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Magmancer Wings");
            Tooltip.SetDefault("Allows flight and slow fall");
        }

		public override void SetDefaults()
		{
			item.width = 42;
			item.height = 42;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = ItemRarityID.LightPurple;
			item.accessory = true;
            item.alpha = 100;
		}
        
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 120;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 1.7f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 7f;
			acceleration *= 1.5f;
		}

        public override bool WingUpdate(Player player, bool inUse)
        {
            int WingTicks;
            if (inUse)
            {
                WingTicks = 6;
            }
            else
            {
                WingTicks = 8;
            }

            if (player.velocity.Y != 0)
            {
                player.wingFrameCounter++;
                if (player.wingFrameCounter > WingTicks)
                {
                    player.wingFrame++;
                    player.wingFrameCounter = 0;
                    if (player.wingFrame >= 3)
                    {
                        player.wingFrame = 0;
                    }
                }
            }
            else
            {
                player.wingFrame = 4;
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LavaBucket, 5);
            recipe.AddIngredient(ItemID.SoulofFlight, 20);
            recipe.AddIngredient(null, "SoulOfSmite", 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
