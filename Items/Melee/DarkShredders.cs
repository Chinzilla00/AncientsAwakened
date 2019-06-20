using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class DarkShredders : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 115;
            item.melee = true;
            item.width = 80;
            item.height = 80;
            item.useTime = 6;
            item.useAnimation = 6;
            item.channel = true;
            item.useStyle = 100;
            item.knockBack = 3f;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 11;      
            item.shoot = mod.ProjectileType("DarkShredders");
            item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkMatter", 12);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reality Shredders");
            Tooltip.SetDefault("Blades made out of Dark matter. Inflicts the Electified debuff");
        }

 
        public override bool UseItemFrame(Player player)
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
            return true;
        }
    }
}
