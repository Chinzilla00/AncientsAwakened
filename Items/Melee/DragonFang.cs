using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Melee
{
    public class DragonFang : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Fang");
            Tooltip.SetDefault("Right click to slash at your foes with the grace of a Valkyrie");
        }

        public override void SetDefaults()
        {
            item.damage = 110;
            item.width = 48;
            item.height = 46;
            item.useTime = 4;
            item.useAnimation = 4;
            item.knockBack = 3;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = ItemRarityID.Lime;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("ValkyrieSlash");
            item.melee = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.noMelee = false;
                item.noUseGraphic = false;
                item.channel = false;
                item.useAnimation = 15;
                item.useTime = 15;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.autoReuse = true;
                item.channel = false;
                item.shoot = ModContent.ProjectileType<Projectiles.AsgardianIce>();
                item.shootSpeed = 10;
            }
            else
            {
                item.noMelee = true;
                item.noUseGraphic = true;
                item.channel = true;
                item.useAnimation = 25;
                item.useTime = 5;
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.autoReuse = false;
                item.channel = true;
                item.shoot = ModContent.ProjectileType<Projectiles.ValkyrieSlash>();
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IceLongsword");
            recipe.AddIngredient(ItemID.Arkhalis);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
