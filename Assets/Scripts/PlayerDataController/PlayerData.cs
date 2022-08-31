[System.Serializable]
public class PlayerData 
{
    public int RewiredID; // assigned after loading ,possibly redundant
    public string name = "foo";
    int health = 100;
    int armor = 100;

    public PlayerData()
    {

    }

    public PlayerData(string PlayerName)
    {
        name = PlayerName;
    }
}