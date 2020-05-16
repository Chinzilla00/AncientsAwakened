using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class Gong : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 35;
            item.height = 54;
            item.maxStack = 1;
            item.value = 10000;
            item.rare = ItemRarityID.Orange;
			item.damage = 20;                        
            item.magic = true;
			item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;        
            item.noMelee = true;
            item.knockBack = 4;
			item.mana = 8;             
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/GONG");
            item.autoReuse = true;
            item.shoot = ProjectileID.TopazBolt;
			item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gong");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("AAMod:Gold", 15);
            recipe.AddIngredient(ItemID.WhiteString);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
