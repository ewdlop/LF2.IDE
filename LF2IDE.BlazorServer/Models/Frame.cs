namespace LF2IDE.BlazorServer.Models;

public class Frame
{
    public int Index { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Pic { get; set; }
    public int State { get; set; }
    public int Wait { get; set; }
    public int Next { get; set; }
    public int Dvx { get; set; }
    public int Dvy { get; set; }
    public int Dvz { get; set; }
    public int CenterX { get; set; }
    public int CenterY { get; set; }
    public int HitA { get; set; }
    public int HitD { get; set; }
    public int HitJ { get; set; }
    public string? Sound { get; set; }

    // Points
    public Point? BPoint { get; set; }
    public WPoint? WPoint { get; set; }
    public OPoint? OPoint { get; set; }
    public CPoint? CPoint { get; set; }

    // Interaction areas
    public List<Bdy> Bdys { get; set; } = new();
    public List<Itr> Itrs { get; set; } = new();

    public string RawText { get; set; } = string.Empty;
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class WPoint : Point
{
    public int Kind { get; set; }
    public int WeaponAct { get; set; }
    public int Attacking { get; set; }
    public int Cover { get; set; }
    public int Dvx { get; set; }
    public int Dvy { get; set; }
    public int Dvz { get; set; }
}

public class OPoint : Point
{
    public int Kind { get; set; }
    public int Action { get; set; }
    public int Dvx { get; set; }
    public int Dvy { get; set; }
    public int Oid { get; set; }
    public int Facing { get; set; }
}

public class CPoint : Point
{
    public int Kind { get; set; }
    public int? FrontHurtAct { get; set; }
    public int? BackHurtAct { get; set; }
    public int? Injury { get; set; }
    public int? VAction { get; set; }
    public int? AAction { get; set; }
    public int? TAction { get; set; }
    public int? ThrowVx { get; set; }
    public int? ThrowVy { get; set; }
    public int? ThrowVz { get; set; }
    public int? ThrowInjury { get; set; }
    public int? Hurtable { get; set; }
    public int? Decrease { get; set; }
    public int? DirControl { get; set; }
    public CPointCoverMode? Cover { get; set; }
}

public enum CPointCoverMode
{
    None = 0,
    SameBehind = 10,
    SameFront = 11,
    OppositeBehind = 20,
    OppositeFront = 21
}

public class Bdy
{
    public int Kind { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int W { get; set; }
    public int H { get; set; }
}

public class Itr
{
    public int Kind { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int W { get; set; }
    public int H { get; set; }
    public int? Dvx { get; set; }
    public int? Dvy { get; set; }
    public int? Fall { get; set; }
    public int? Arest { get; set; }
    public int? Vrest { get; set; }
    public int? BDefend { get; set; }
    public int? Injury { get; set; }
    public int? Effect { get; set; }
    public int? CatchingAct { get; set; }
    public int? CaughtAct { get; set; }
}

