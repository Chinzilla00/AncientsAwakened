using Terraria.ID;

namespace AAMod.Items.Boss.Serpent
{
    public class SnowflakeShuriken : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowflake Shuriken");
        }
        public override void SetDefaults()
		{
            item.damage = 20;
            item.maxStack = 999;
            item.ranged = true;
            item.width = 10;
            item.height = 10;
			item.useTime = 20;
			item.useAnimation = 20;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 0;
			item.value = 100;
			item.rare = ItemRarityID.Orange;
			item.shootSpeed = 12f;
			item.shoot = mod.ProjectileType ("SS");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.consumable = true;
        }
    }
}
