using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance { private set; get; }


    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        ResourceService resourceService = GetComponent<ResourceService>();
        resourceService.InitService();

        AudioService audioService = GetComponent<AudioService>();
        audioService.InitService();

        DataService dataService = GetComponent<DataService>();
        dataService.InitService();
      
        PoolService poolService = GetComponent<PoolService>();
        poolService.InitService();

        StartSystem startSystem = GetComponent<StartSystem>();
        startSystem.InitSystem();
        
        GameSystem gameSystem = GetComponent<GameSystem>();
        gameSystem.InitSystem();

        startSystem.InitStartScene(); 
    }
}