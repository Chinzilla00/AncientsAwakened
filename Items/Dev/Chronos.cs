using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Dev
{
    public class Chronos : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.useTime = 25;
            item.CloneDefaults(ItemID.Terrarian);

            item.damage = 350;
            item.value = 1000000;
            item.rare = 11;
            item.knockBack = 1;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 18;
            item.useTime = 18;
            item.shoot = mod.ProjectileType("Chronos");
            item.expert = true; item.expertOnly = true;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= CalcDamageMultiplierFromTimeOfDay(item.damage);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 1000);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chronos");
            Tooltip.SetDefault("Time Teller EX\n" +
                "Damage changes based on time of day\n" +
                "Damage is greatest at Midday and Midnight\n" +
                "'Time is big ball of wibbly-wobbly timey-wimey yo-yos.'\n" +
                "-Dallin");
        }

        public override void UpdateInventory(Player player)
        {
            if (player.accWatch < 3)
                player.accWatch = 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TimeTeller");
            recipe.AddIngredient(null, "EXSoul");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public float CalcDamageMultiplierFromTimeOfDay(int baseDamage)
        {
            int minDamage = baseDamage; //this is the damage you set in SetDefaults.
            int maxDamage = 500; //this is the damage you get at midday/midnight.

            float maxMultiplier = maxDamage / (float)minDamage;
            float time = (int)Main.time;
            float calcTimeMax = 0f;
            if (Main.dayTime)
                calcTimeMax = 54000f; //max time in a day
            else
                calcTimeMax = 32400f; //max time in a night

            return BaseUtility.MultiLerp(time / calcTimeMax, 1f, maxMultiplier, 1f);
        }
    }
}
