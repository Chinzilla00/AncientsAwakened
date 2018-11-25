using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
    public class DarkmatterVisor : ModItem
    {
        
        public override void SetStaticDefaults()
        { 
            
            DisplayName.SetDefault("Darkmatter Visor");
            Tooltip.SetDefault(@"10% increased Ranged damage
Dark, yet still barely visible");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 14;
            item.value = 300000;
            item.rare = 11;
            item.defense = 26;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.10f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DarkmatterBreastplate") && legs.type == mod.ItemType("DarkmatterGreaves");
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"25% decreased ammo consumption
20% increased ranged critical chance
Your ranged ammunition electrocute enemies";

            player.ammoCost75 = true;
            player.rangedCrit += 20;
            player.GetModPlayer<AAPlayer>(mod).darkmatterSetRa = true;
            player.armorEffectDrawShadowLokis = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkMatter", 25);
            recipe.AddIngredient(null, "DarkEnergy", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}