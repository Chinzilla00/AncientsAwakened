using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class DragonStaff : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 50;                        
            item.magic = true;                     
            item.width = 60;
            item.height = 60;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;     
            item.noMelee = true;
            item.knockBack = 6;        
            item.value = 10000;
            item.rare = ItemRarityID.Pink;
            item.mana = 5;             
            item.UseSound = SoundID.Item21;            
            item.autoReuse = true;
            item.shoot = mod.ProjectileType ("DragonP");  
            item.shootSpeed = 13f;     
        }   

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Dragon Staff");
      Tooltip.SetDefault("Shoots dragon scales.");
            Item.staff[item.type] = true;
    }

		public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DragonSpirit", 20);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
