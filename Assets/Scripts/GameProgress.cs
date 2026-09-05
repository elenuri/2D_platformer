using UnityEngine;

public static class GameProgress
{
    // 0 = First visit (unlock Goma)
    // 1 = Goma completed
    // 2 = Guri completed
    // 3 = Rangi completed
    // 4 = Kiri completed
    // 5 = Game finished
    public static int mapState = 0;
}