using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class StarStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Staff");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 175;
            item.magic = true;
            item.mana = 25;
            item.width = 64;
            item.height = 64;
            item.useTime = 5;
            item.useAnimation = 16;
            item.reuseDelay = 16;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 100000;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Star1");
            item.shootSpeed = 9f;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Stardust", 5);
            recipe.AddIngredient(null, "RadiumBar", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}