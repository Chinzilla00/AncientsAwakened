using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ModLoader;
using static AAMod.AAMod;

namespace AAMod.Misc
{
    public static class CacheLoader
    {
        private static Dictionary<string, Texture2D> cache;

        public static void Cache()
        {
            cache = new Dictionary<string, Texture2D>
            {
                ["Backgrounds/InfernoSky"] = instance.GetTexture("Backgrounds/InfernoSky"),
                ["Backgrounds/InfernoBG"] = instance.GetTexture("Backgrounds/InfernoBG"),
                ["Backgrounds/MireBG"] = instance.GetTexture("Backgrounds/MireBG"),
                ["Backgrounds/MireFG1"] = instance.GetTexture("Backgrounds/MireFG1"),
                ["Backgrounds/MireFG2"] = instance.GetTexture("Backgrounds/MireFG2"),
                ["Terraria/Sun"] = ModContent.GetTexture("Terraria/Sun"),
                ["Terraria/Logo"] = ModContent.GetTexture("Terraria/Logo"),
                ["Terraria/Logo2"] = ModContent.GetTexture("Terraria/Logo2"),
                ["UI/LogoVoid"] = instance.GetTexture("UI/LogoVoid"),
                ["Backgrounds/Sun"] = instance.GetTexture("Backgrounds/Sun"),
                ["UI/LogoInferno"] = instance.GetTexture("UI/LogoInferno"),
                ["UI/LogoMire"] = instance.GetTexture("UI/LogoMire"),
                ["Backgrounds/YamataStars"] = instance.GetTexture("Backgrounds/YamataStars")
            };
        }

        public static Texture2D GetCachedTexture(string path)
        {
            cache.TryGetValue(path, out Texture2D output);
            return output;
        }

        public static void ClearCache()
        {
            cache.Clear();
        }
    }
}
