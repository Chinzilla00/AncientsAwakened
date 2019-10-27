using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Athena
{
    public class GaleForce : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 200;                        
            item.magic = true;                     
            item.width = 24;
            item.height = 28;
            item.useStyle = 5;        
            item.noMelee = true;
            item.knockBack = 6;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.mana = 8;             
            item.UseSound = SoundID.Item21;            
            item.autoReuse = true;
            item.useTime = 28;
            item.useAnimation = 28;
            item.shoot = mod.ProjectileType("HurricaneSpawn");
            item.shootSpeed = 9f;    
        }   

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Gale Force");
          Tooltip.SetDefault("");
        }
    }
}
