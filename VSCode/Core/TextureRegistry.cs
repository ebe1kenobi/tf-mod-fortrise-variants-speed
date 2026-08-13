using FortRise;

namespace TFModFortRiseVariantSpeed;

public class TextureRegistry : IRegisterable
{
  /// <summary>La botte ailee de la variante, 14x14 comme celles du jeu.</summary>
  public static ISubtextureEntry Speed { get; private set; } = null!;

  public static void Register(IModContent content, IModRegistry registry)
  {
    Speed = registry.Subtextures.RegisterTexture(
        content.Root.GetRelativePath("Content/Atlas/speed.png")
    );
  }
}
