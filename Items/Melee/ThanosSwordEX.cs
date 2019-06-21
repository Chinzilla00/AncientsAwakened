using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ThanosSwordEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thanoscopter Blades");
            Tooltip.SetDefault(@"'I am...inevitable'
Mad Titan's Dualblade EX");
        }

        public override void SetDefaults()
        {
            item.noUseGraphic = true;
            item.damage = 300;
            item.melee = true;
            item.noMelee = true;
            item.width = 88;
            item.height = 100;
            item.useTime = 10;
            item.useAnimation = 10;
            item.channel = true;
            item.useStyle = 100;
            item.knockBack = 6f;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.rare = 11;
            item.shoot = mod.ProjectileType("ThanosSwordEX");
            item.noUseGraphic = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.useStyle = 100;
                item.shoot = mod.ProjectileType("ThanosSwordEX");
                item.shootSpeed = 10f;
            }
            else
            {
                item.useStyle = 1;
                item.shoot = mod.ProjectileType("ThanosSwordEXT");
                item.shootSpeed = 14f;
            }
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && (Main.projectile[i].type == mod.ProjectileType("ThanosSwordEX") || Main.projectile[i].type == mod.ProjectileType("ThanosSwordEXT")))
                {
                    return false;
                }
            }
            return base.CanUseItem(player);
        }

        public override bool UseItemFrame(Player player)     //this defines what frame the player use when this weapon is used
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ThanosSword", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
