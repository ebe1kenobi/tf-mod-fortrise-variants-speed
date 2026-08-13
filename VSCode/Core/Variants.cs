using FortRise;

namespace TFModFortRiseVariantSpeed
{
  /// <summary>
  /// UNE variante, et un reglage a cote.
  ///
  /// Il y en avait cinq - Speedx1 a Speedx5 - qui remplissaient cinq cases de
  /// l'ecran des variantes pour dire la meme chose a cinq vitesses. Une seule case
  /// suffit : la vitesse se regle dans une fenetre qui s'ouvre sur la variante meme
  /// (voir SpeedPopup), comme le jeu ouvre celle des joueurs sur ses variantes par
  /// joueur.
  /// </summary>
  public class Variants : IRegisterable
  {
    public static IVariantEntry Speed = null!;

    /// <summary>
    /// Le libelle, tel que le jeu le porte sur la variante : FortRise le met en
    /// majuscules en construisant le Variant. C'est par lui qu'on reconnait NOTRE
    /// case dans l'ecran des variantes, ou l'on ne voit que des objets du jeu.
    /// </summary>
    public const string TITLE = "SPEED";

    public static void Register(IModContent content, IModRegistry registry)
    {
      Speed = registry.Variants.RegisterVariant("Speed", new()
      {
        // Header commun a tous mes mods : sans lui FortRise retombe sur le nom du
        // mod et chacun cree sa propre colonne dans l'ecran des variantes.
        Header = "EBE1 MODS",
        Title = TITLE,

        // Par joueur : les bottes du jeu se portent, elles ne s'appliquent pas au
        // monde. C'est aussi ce qui permet de donner l'avantage a un seul joueur.
        //
        // Attention : le jeu prend alors la touche Alt pour sa fenetre des joueurs.
        // La vitesse s'ouvre donc a Alt2 (voir MyVariantToggle).
        Flags = CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom,
        Icon = TextureRegistry.Speed
      });
    }
  }
}
