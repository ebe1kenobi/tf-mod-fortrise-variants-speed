using FortRise;
using Monocle;
using TowerFall;

namespace TFModFortRiseVariantSpeed;

// Optional way to use textures
public class TextureRegistry : IRegisterable
{
    // Variants
    public static ISubtextureEntry Speedx1 { get; private set; } = null!;
    public static ISubtextureEntry Speedx2 { get; private set; } = null!;
    public static ISubtextureEntry Speedx3 { get; private set; } = null!;
    public static ISubtextureEntry Speedx4 { get; private set; } = null!;
    public static ISubtextureEntry Speedx5 { get; private set; } = null!;

    public static void Register(IModContent content, IModRegistry registry)
    {
    Speedx1 = registry.Subtextures.RegisterTexture(
                content.Root.GetRelativePath("Content/Atlas/speedx1.png")
            );
    Speedx2 = registry.Subtextures.RegisterTexture(
                content.Root.GetRelativePath("Content/Atlas/speedx2.png")
            );
    Speedx3 = registry.Subtextures.RegisterTexture(
                content.Root.GetRelativePath("Content/Atlas/speedx3.png")
            );

    Speedx4 = registry.Subtextures.RegisterTexture(
                content.Root.GetRelativePath("Content/Atlas/speedx4.png")
            );

    Speedx5 = registry.Subtextures.RegisterTexture(
                content.Root.GetRelativePath("Content/Atlas/speedx5.png")
            );
  }
}