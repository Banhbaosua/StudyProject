using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpListInDictionary : MonoBehaviour
{
    public class Characters
    {
        public string Name;
        public string Gender;
        public int Height;
        public int Age;
        public string Nationality;
        public Characters(string newName, string newGender, int newHeight, int newAge)
        {
            Name = newName;
            Gender = newGender;
            Height = newHeight;
            Age = newAge;
        }
    }

    List<Characters> HeroCharacters = new List<Characters>();
    List<Characters> VillainCharacters = new List<Characters>();
    List<Characters> AntiHeroCharacters = new List<Characters>();
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, List<Characters>> UnityHero = new Dictionary<string, List<Characters>>();

        Characters character;
        //Hero
        character = new Characters("Iron Man","Male",185,48);
        HeroCharacters.Add(character);
        character = new Characters("Captain America","Male",187,80);
        HeroCharacters.Add(character);
        character = new Characters("Thor","Male",206,1500);
        HeroCharacters.Add(character);
        UnityHero.Add("Hero",HeroCharacters);

        //Villain
        character = new Characters("Thanos","Male",200,1000);
        VillainCharacters.Add(character);
        character = new Characters("Ultron","Machine",231,1);
        VillainCharacters.Add(character);
        character = new Characters("Hela","Female",175,1500);
        VillainCharacters.Add(character);
        UnityHero.Add("Villain", VillainCharacters);

        //AntiHero
        character = new Characters("Deadpool","Male",188,45);
        AntiHeroCharacters.Add(character);
        character = new Characters("Punisher","Male",185,41);
        AntiHeroCharacters.Add(character);
        character = new Characters("Eddie Brock","Male",191,35);
        AntiHeroCharacters.Add(character);
        UnityHero.Add("Anti Hero",AntiHeroCharacters);

        foreach (KeyValuePair<string, List<Characters>> kvp in UnityHero )
        {
            Debug.Log("----"+kvp.Key);
            foreach (Characters C in kvp.Value)
            {
                Debug.Log(C.Name + " - " +C.Gender+ " - " + C.Height+ " - " + C.Age);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
