using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class YtriumStaff : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 44;
            item.height = 44;
            item.maxStack = 1;

            item.value = BaseMod.BaseUtility.CalcValue(0, 5, 0, 0);
            item.rare = 3;
			item.damage = 30;                        
            item.magic = true;
			item.useTime = 23;
            item.useAnimation = 23;
            item.useStyle = 5; 
            item.noMelee = true;
            item.knockBack = 4;
			item.mana = 8;
            item.UseSound = SoundID.Item21;
            item.autoReuse = true;
            item.shoot = 122;
			item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yttrium Staff");
            Item.staff[item.type] = true;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "YtriumBar", 24);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
