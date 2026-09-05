using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public enum Level
    {
        Goma,
        Guri,
        Rangi,
        Kiri,
        Yangi
    }

    public Level levelToFinish;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        switch (levelToFinish)
        {
            case Level.Goma:
                sceneLoader.FinishGoma();
                break;

            case Level.Guri:
                sceneLoader.FinishGuri();
                break;

            case Level.Rangi:
                sceneLoader.FinishRangi();
                break;

            case Level.Kiri:
                sceneLoader.FinishKiri();
                break;

            case Level.Yangi:
                sceneLoader.FinishYangi();
                break;
        }
    }
}